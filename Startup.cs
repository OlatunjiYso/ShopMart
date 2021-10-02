using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ShopMart.Models;
using ShopMart.Data;
using Microsoft.EntityFrameworkCore;
using ShopMart.Interface;
using ShopMart.Authorization;
using ShopMart.Services;
using ShopMart.Helpers;

namespace ShopMart
{
    public class Startup
    {
        private readonly IConfiguration Configuration;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


       
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<ShopMartContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            services.AddDbContext<ShopMartContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Transient);
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddCors();
            services.AddControllers();
            // configure strongly typed settings objects
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            // configure DI for application services
            services.AddScoped<IJwtUtils, JwtUtils>();
            //services.AddScoped<ICustomerService, CustomerService>();
            services.AddTransient<ICustomerService, CustomerService>();
        }



        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            app.UseAuthorization();
            app.UseMiddleware<JwtMiddleware>();
            app.UseEndpoints(x => x.MapControllers());
        }
    }
}
