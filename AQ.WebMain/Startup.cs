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

            //读取Nlog配置文件
            env.ConfigureNLog("nlog.config");
            loggerFactory.AddNLog();
            //loggerFactory.AddProvider(new NLogLoggerPrivoder());

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
