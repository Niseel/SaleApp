using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using ApplicationCore.Mapping;
using ApplicationCore.Services;
using AutoMapper;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SaleApp.Interfaces;
using SaleApp.Services;

namespace SaleApp
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
            //services.Configure<RazorViewEngineOptions>(options =>
            //{
            //    options.AreaViewLocationFormats.Clear();
            //    options.AreaViewLocationFormats.Add("/Admin/{2}/Views/{1}/{0}.cshtml");
            //    options.AreaViewLocationFormats.Add("/Admin/{2}/Views/Shared/{0}.cshtml");
            //    options.AreaViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
            //});

            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IBrandIndexVmService, BrandIndexVmService>();


            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICategoryIndexVmService, CategoryIndexVmService>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductIndexVmService, ProductIndexVmService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserIndexVmService, UserIndexVmService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddAutoMapper(typeof(MappingProfile));

            services.AddMvc(option => option.EnableEndpointRouting = false);


            services.AddControllersWithViews();
            services.AddDbContextPool<SaleContext>(option =>
                option.UseSqlServer(Configuration.GetConnectionString("SaleDBConnection"), 
                b => b.MigrationsAssembly("SaleApp")));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "MyArea",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}"
                );
                //routes.MapAreaRoute(
                //    name: "MyAreaAdmin",
                //    areaName: "Admin",
                //    template: "Admin/{controller=Home}/{action=Index}/{id?}"
                //);
                //routes.MapAreaRoute(
                //    name: "MyAreaClient",
                //    areaName: "Client",
                //    template: "Client/{controller=Home}/{action=Index}/{id?}");
            });

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=Home}/{action=Index}/{id?}");
            //    endpoints.MapControllerRoute(
            //        name: "areas",
            //        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            //});

        }
    }
}
