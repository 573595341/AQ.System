using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;
using System.Linq;
using Microsoft.Extensions.Options;
using AQ.IServices;
using AQ.Services;
using AQ.IRepository;
using AQ.Repository.SqlServer;
using AutoMapper;
using AQ.Core;
using AQ.WebMain.Commons.Extensions;
using AQ.WebMain.Profiles;
using AQ.Models;
using AQ.ViewModels;

namespace AQ.Test
{
    public class UnitTest1
    {
        [Fact]
        public void TestMethod1()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            var services = BuildServiceForSqlServer();

            //var ioption = service.GetRequiredService<IOptions<CodeGenerateOption>>();
            //var s = ioption.Value;
            //var s2 = service.GetRequiredService<CodeGenerateOption>();
            //ConfigureFromConfigurationOptions<CodeGenerateOption> option = 
            //    new ConfigureFromConfigurationOptions<CodeGenerateOption>();


            //var service = services.GetService<IAuthorizeService>();
            //service.GetAuthModuleData();



            Assert.Equal(0, 0);
        }


        /// <summary>
        /// 构造依赖注入容器，然后传入参数
        /// </summary>
        /// <returns></returns>
        public IServiceProvider BuildServiceForSqlServer()
        {
            var services = new ServiceCollection();
            services.Configure<DbOption>(options =>
            {
                //options.DbConnString = "Data Source=.;Initial Catalog=AQsys;User ID=sa;Password=123456;Persist Security Info=True;Max Pool Size=50;Min Pool Size=0;Connection Lifetime=300;";//这个必须
                options.DbConnString = "Data Source = WANGFAN-PC\\SQLEXPRESS;Initial Catalog = AQsys;User Id = sa;Password = 123456;";//这个必须
                options.DbType = "MSSQL";//数据库类型是SqlServer,其他数据类型参照枚举DatabaseType//这个也必须
            });
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<ServicesMapperProfiles>();
            });
            services.AddAutoMapper();
            services.AddScopedByAssemblyName("AQ.Repository.SqlServer");
            services.AddScopedByAssemblyName("AQ.Services");

            //services.AddTransient<IAQsysKeyRegulationRepository, AQsysKeyRegulationRepository>();
            //services.AddTransient<IAQsysModuleRepository, AQsysModuleRepository>();
            //services.AddTransient<IAQsysModuleService, AQsysModuleService>();
            return services.BuildServiceProvider(); //构建服务提供程序
        }

        public IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(AppContext.BaseDirectory)
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .AddEnvironmentVariables();
            return builder.Build();
        }

        class CodeGenerateOption
        {
            public string ConnectionString { get; set; }
            public string DbType { get; set; }
            public string Author { get; set; }
            public string OutputPath { get; set; }
            public string ModelsNamespace { get; set; }
        }
    }
}
