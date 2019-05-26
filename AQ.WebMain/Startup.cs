using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using AutoMapper;
using NLog.Extensions.Logging;
using NLog.Web;
using NLog;
using AQ.WebMain.Filter;
using AQ.WebMain.Commons.Extensions;
using AQ.WebMain.Commons;
using AQ.IServices;
using AQ.ViewModels;
using AQ.Services;
using AQ.IRepository;
using AQ.Repository.SqlServer;
using AQ.Core;
using AQ.WebMain.Profiles;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.Razor;

namespace AQ.WebMain
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //services.Configure<RazorViewEngineOptions>(options =>
            //{
            //    options.ViewLocationFormats.Clear();
            //    options.ViewLocationFormats.Add("/Views/Admin/{1}/{0}.cshtml");
            //    options.ViewLocationFormats.Add("/Views/{1}/{0}.cshtml");
            //    options.ViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
            //    options.ViewLocationFormats.Add("/Views/Shared/{0}.cshtml");

            //    //options.AreaViewLocationFormats.Clear();
            //    //options.AreaViewLocationFormats.Add("/Areas/{2}/Views/Member/{1}/{0}.cshtml");
            //    //options.AreaViewLocationFormats.Add("/Areas/{2}/Views/{1}/{0}.cshtml");
            //    //options.AreaViewLocationFormats.Add("/Areas/{2}/Views/Shared/{0}.cshtml");
            //    //options.AreaViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
            //});

            services.Configure<DbOption>(Configuration.GetSection("DbOptions"));
            //services.Configure<DbOption>(options =>
            //{
            //    options.DbConnString = "Data Source = .;Initial Catalog = AQsys;User Id = sa;Password = 123456;";
            //    options.DbType = "MSSQL";
            //});
            services.AddAntiforgery(options =>
            {
                options.FormFieldName = "PageToken";
                options.HeaderName = "PAGETOKEN";
                options.SuppressXFrameOptionsHeader = false;
            });
            services.AddMvc(options =>
            {
                options.Filters.Add<GlobalExceptionFilter>();
                //options.Filters.Add<AuthorizationFilter>();
            })
            .AddJsonOptions(opt =>
            {
                opt.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                opt.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver(); //默认格式
                //opt.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver(); //首字母小写
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //Mapper.Initialize(cfg =>
            //{
            //    cfg.AddProfile<ServicesMapperProfiles>();
            //});
            services.AddAutoMapper(opt => opt.AddProfile<ServicesMapperProfiles>());
            services.AddScopedByAssemblyName("AQ.Repository.SqlServer");
            services.AddScopedByAssemblyName("AQ.Services");

            //services.AddRouting();

            //services.AddTransient<IArticleCategoryRepository, ArticleCategoryRepository>();
            //services.AddTransient<IArticleCategoryService, ArticleCategoryService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStatusCodePagesWithRedirects("/Error/{0}");
            //app.UseStatusCodePages( async context => await context.HttpContext.Response.WriteAsync("404"));

            //读取Nlog配置文件
            env.ConfigureNLog("nlog.config");
            loggerFactory.AddNLog();
            //loggerFactory.AddProvider(new NLogLoggerPrivoder());

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                //路由规则匹配是从上到下的，优先匹配的路由一定要写在最上面。因为路由匹配成功以后，他不会继续匹配下去。
                //routes.MapRoute(
                //  name: "admin", // 路由名称，这个只要保证在路由集合中唯一即可
                //  template: "Admin/{controller=Home}/{action=Index}/{id?}"); //路由规则,匹配以Admin开头的url

                //routes.MapAreaRoute(
                //    name: "admin",
                //    areaName: "Admin",
                //    template: "Admin/{controller}/{action}/{id?}");



                //routes.MapAreaRoute(
                //    name: "admin",
                //    areaName: "Admin",
                //    template: "{area:exists}/Member/{controller=Home}/{action=Index}/{id?}");

                routes.MapAreaRoute(
                    name: "admin",
                    areaName: "Admin",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                //routes.MapRoute(
                //    name: "default",
                //    template: "Admin/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
