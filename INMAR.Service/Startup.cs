using INMAR.Service.DdContextConfiguration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace INMAR.Service
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private static string contentRootPath;
        public Startup(IConfiguration configuration,
            IWebHostEnvironment env)
        {
            _configuration = configuration;
            contentRootPath = env.ContentRootPath;

        }
        public void ConfigureServices(IServiceCollection services)
        {
            var databasePath = Path.Combine(contentRootPath, "DdContextConfiguration", "mydatabase.db");

            Console.WriteLine($"Database Path: {databasePath}");

            services.AddDbContext<ApplicationDBContext>(options => options.UseSqlite($"Data Source={databasePath}"));


            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });
            services.AddMvc().AddXmlSerializerFormatters();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "INMAR Service", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseRouting();

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDBContext>();

                if (env.IsDevelopment())
                {
                    dbContext.Database.Migrate();

                    SeedData.Initialize(dbContext);
                }
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });

            app.UseStaticFiles();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "INMAR Service");
            });
        }
    }
}
