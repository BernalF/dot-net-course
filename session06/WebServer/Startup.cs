using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using WebServer.Data;

using Microsoft.EntityFrameworkCore;
using AdventureWorks.Models;
using Microsoft.Extensions.Configuration;
using System;

using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Formatter;
using System.Net.Http.Headers;
using System.Linq;
using System.Collections.Generic;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;

namespace WebServer
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ProductRepository>();
            services.AddScoped<CategoryRepository>();

            //BP-01 Tip: Use EnvironmentVariables and Secrets
            var connectionString = Environment.GetEnvironmentVariable("AdventureWorksDatabase") ??
                        Configuration.GetConnectionString("AdventureWorksDatabase");

            services.AddDbContext<AdventureWorksContext>(
                options => options.UseSqlServer(
                    connectionString)
            );

            services.AddTransient<AdventureWorksContext>();

            services.AddCors(policy => {
                policy.AddPolicy("CORS", builder => {
                    builder.WithOrigins("https://localhost:5002", "http://localhost:5003");

//                  builder.AllowAnyOrigin()
//                          .AllowAnyMethod()
//                           .WithHeaders();
                });
            });

            //BP- Security
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = false,
                        ValidIssuer = Configuration[Security.JwtTokenBuilder.CONFIGURATION_AUTHENTICATION_ISSUER_KEY],
                        ValidAudience = Configuration[Security.JwtTokenBuilder.CONFIGURATION_AUTHENTICATION_AUDIENCE_KEY],
                        IssuerSigningKey = Security.JwtTokenBuilder.GetSecurityKey(Configuration[Security.JwtTokenBuilder.CONFIGURATION_AUTHENTICATION_SHARED_SECRET_KEY])
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context => Task.CompletedTask,
                        OnTokenValidated = context => Task.CompletedTask,
                        OnChallenge = context => Task.CompletedTask
                    };
                });


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
					Description = "We use Basic Bearer tokens",
                    In = "header",
                    Type = "apiKey",
                    Name = "Authorization"
                });

                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> {
                    { "Bearer", new string[0] }
                });
            });

            //BP-05: OData powered queries
            services.AddOData();

            services.AddMvc(op =>
            {
                //BP-05: OData powered queries
                foreach (var formatter in op.OutputFormatters
                        .OfType<ODataOutputFormatter>()
                        .Where(it => !it.SupportedMediaTypes.Any()))
                {
                    formatter.SupportedMediaTypes.Add("application/prs.mock-odata");
                }
                foreach (var formatter in op.InputFormatters
                    .OfType<ODataInputFormatter>()
                    .Where(it => !it.SupportedMediaTypes.Any()))
                {
                    formatter.SupportedMediaTypes.Add("application/prs.mock-odata");
                }

            });
                //BP-02 Tip: Content-resolution
                //.AddXmlSerializerFormatters();
                // Plus, 2.1 compatibility
                //.SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
            }
            //BP-04 Tip: Error handler Middleware
            app.UseErrorHandlerMiddleware();

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
            app.UseCors("CORS");

			//BP- Security
			app.UseAuthentication();

			app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = "";
            });

			app.UseMvc(routeBuilder =>
            {
                //BP-05: OData powered queries
                routeBuilder.EnableDependencyInjection();
                routeBuilder.Expand()
                            .Select()
                            .Filter()
                            .Count()
                            .OrderBy()
                            .SkipToken()
                            .MaxTop(null);
            });

            // https://localhost:5001/api/products?$select=Color,Name
            // https://localhost:5001/api/products?$select=Name,Size,Weight&$orderby=Names
            // https://localhost:5001/api/products?$select=Color,Name&$orderby=Name%20desc&$filter=contains(Name,'Women')
            //https://www.odata.org/documentation/
            // https://www.odata.org/getting-started/basic-tutorial/
        }
    }
}