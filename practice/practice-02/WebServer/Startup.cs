using AdventureWorks.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebServer.Data;

namespace WebServer
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ProductRepository>();

            services.AddScoped<CategoryRepository>();

            //services.AddDbContext<AdventureWorks.Models.AdventureWorksContext>(
            //   options => options.UseSqlServer(Configuration.GetConnectionString("AdventureWorksDatatabase")));

            services.AddDbContext<AdventureWorksContext>(
                options => options.UseSqlServer("data source=localhost;initial catalog=AdventureWorks;Integrated Security=True;MultipleActiveResultSets=True;App=EntityFramework"));

            services.AddTransient<AdventureWorksContext>();

            services.AddCors(policy =>
            {
                policy.AddPolicy("CORS", builder =>
                {
                    builder.WithOrigins("https://localhost:5002", "http://localhost:5003");
                    //builder.AllowAnyOrigin()
                    //       .AllowAnyMethod()
                    //       .WithHeaders();
                });
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title = "Inventory API", Version = "v1" });
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Inventory API V1");
                c.RoutePrefix = "api";
            });

            app.UseCors("CORS");

            app.UseMvc();

            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Home}/{action=Index}/{id?}");
            //});
        }
    }
}
