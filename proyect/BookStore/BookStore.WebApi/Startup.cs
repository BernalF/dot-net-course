using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Entities;
using BookStore.WebApi.Repositories;
using BookStore.WebApi.Utils;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;

namespace BookStore.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<BooksRepository>();
            services.AddScoped<CategoriesRepository>();

            var connectionString = Environment.GetEnvironmentVariable("BookStoreDatabase") ??
                                   Configuration.GetConnectionString("BookStoreDatabase");

            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.AddDbContext<BookStoreDBContext>(options => options.UseSqlServer(connectionString));

            services.AddTransient<BookStoreDBContext>();

            services.AddCors(policy =>
            {
                policy.AddPolicy("CORS",
                    builder => { builder.WithOrigins("http://localhost:5002", "https://localhost:5003"); });
            });

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
                        ValidAudience =
                            Configuration[Security.JwtTokenBuilder.CONFIGURATION_AUTHENTICATION_AUDIENCE_KEY],
                        IssuerSigningKey = Security.JwtTokenBuilder.GetSecurityKey(
                            Configuration[Security.JwtTokenBuilder.CONFIGURATION_AUTHENTICATION_SHARED_SECRET_KEY])
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
                c.SwaggerDoc("v1", new Info {Title = "BookStore API", Version = "v1"});

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "We use Basic Bearer tokens",
                    In = "header",
                    Type = "apiKey",
                    Name = "Authorization"
                });

                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[0]}
                });
            });

            services.AddOData();

            services.AddMvc(opts =>
            {
                foreach (var formatter in opts.OutputFormatters
                    .OfType<ODataOutputFormatter>()
                    .Where(it => !it.SupportedMediaTypes.Any()))
                {
                    formatter.SupportedMediaTypes.Add("application/prs.mock-odata");
                }

                foreach (var formatter in opts.InputFormatters
                    .OfType<ODataInputFormatter>()
                    .Where(it => !it.SupportedMediaTypes.Any()))
                {
                    formatter.SupportedMediaTypes.Add("application/prs.mock-odata");
                }
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseErrorHandlerMiddleware();

            app.UseCors("CORS");

            app.UseAuthentication();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookStore API V1");
                c.RoutePrefix = "";
            });

            app.UseHttpsRedirection();

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
        }
    }
}