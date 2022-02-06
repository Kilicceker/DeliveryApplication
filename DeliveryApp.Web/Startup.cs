using DeliveryApp.Web.HttpService;
using DeliveryApp.Web.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DeliveryApp.Web
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

            services.AddControllersWithViews().AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                opt.JsonSerializerOptions.PropertyNamingPolicy = null;
            });
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped(typeof(IApiService<>), typeof(ApiService<>));
            services.AddHttpClient<ProductService>(opt => {
                opt.BaseAddress = new Uri(Configuration["baseUrl"]);
            });
            services.AddHttpClient<BrandService>(opt => {
                opt.BaseAddress = new Uri(Configuration["baseUrl"]);
            });
            services.AddHttpClient<CommentService>(opt => {
                opt.BaseAddress = new Uri(Configuration["baseUrl"]);
            });
            services.AddHttpClient<CategoryService>(opt => {
                opt.BaseAddress = new Uri(Configuration["baseUrl"]);
            });
            services.AddHttpClient<BasketService>(opt => {
                opt.BaseAddress = new Uri(Configuration["baseUrl"]);
            });
            services.AddHttpClient<AuthService>(opt => {
                opt.BaseAddress = new Uri(Configuration["baseUrl"]);
            });
            services.AddHttpClient<AddressService>(opt => {
                opt.BaseAddress = new Uri(Configuration["baseUrl"]);
            });
            services.AddHttpClient<OrderService>(opt => {
                opt.BaseAddress = new Uri(Configuration["baseUrl"]);
            });
            services.AddHttpContextAccessor();

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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(name: "Admin", areaName: "Admin", pattern: "Admin/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
