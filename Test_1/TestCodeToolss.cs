using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;
using System.Linq;
using AQ.CodeTools;
using System.Collections.Generic;
using AQ.CodeTools.Enums;
using System.Text.RegularExpressions;
using System.IO;

namespace AQ.Test
{
    public class TestCodeToolss
    {
        [Fact]
        public void TestStart()
        {
            Assert.Equal(0, 0);
        }



        [Fact]
        public void TestCodeGenerate()
        {
            //var templatePath = @"F:\C#\Netcore\17kk\AQ.System\AQ.CodeTools\CodeTemplates";
            //var outputPath = @"F:\C#\Netcore\17kk\AQ.System";
            var templatePath = @"E:\netcore\17KK\AQ.System\AQ.CodeTools\CodeTemplates";
            var outputPath = @"E:\netcore\17KK\AQ.System";
            var services = new ServiceCollection();
            services.Configure<CodeGenerateOptionModel>(options =>
            {
                //options.DbConnString = "Data Source = .;Initial Catalog = AQsys;User Id = sa;Password = 123456;";
                options.DbConnString = @"Data Source = WANGFAN-PC\SQLEXPRESS;Initial Catalog = AQsys;User Id = sa;Password = 123456;";
                options.DbType = "MSSQL";
                options.ModelsConfig = new TemplateConfig()
                {
                    Namespace = "AQ.Models",
                    TemplatePath = $@"{templatePath}\ModelTemplate.txt",
                    OutputPath = $@"{outputPath}\AQ.Models\Auto\"
                };
                options.ViewModelsConfig = new TemplateConfig()
                {
                    Namespace = "AQ.ViewModels",
                    TemplatePath = $@"{templatePath}\ViewModelTemplate.txt",
                    OutputPath = $@"{outputPath}\AQ.ViewModels\Auto\"
                };
                options.IRepositoryConfig = new TemplateConfig()
                {
                    Namespace = "AQ.IRepository",
                    TemplatePath = $@"{templatePath}\IRepositoryTemplate.txt",
                    OutputPath = $@"{outputPath}\AQ.IRepository\Auto\"
                };
                options.RepositoryConfig = new TemplateConfig()
                {
                    Namespace = "AQ.Repository.SqlServer",
                    TemplatePath = $@"{templatePath}\RepositoryTemplate.txt",
                    OutputPath = $@"{outputPath}\AQ.Repository.SqlServer\Auto\"
                };
                options.IServiceConfig = new TemplateConfig()
                {
                    Namespace = "AQ.IServices",
                    TemplatePath = $@"{templatePath}\IServiceTemplate.txt",
                    OutputPath = $@"{outputPath}\AQ.IServices\Auto\"
                };
                options.ServiceConfig = new TemplateConfig()
                {
                    Namespace = "AQ.Services",
                    TemplatePath = $@"{templatePath}\ServiceTemplate.txt",
                    OutputPath = $@"{outputPath}\AQ.Services\Auto\"
                };
            });
            services.AddTransient<GenerateModelCode>();
            services.AddTransient<GenerateViewModelCode>();
            services.AddTransient<GenerateIRepositoryCode>();
            services.AddTransient<GenerateRepositoryCode>();
            services.AddTransient<GenerateIServiceCode>();
            services.AddTransient<GenerateServiceCode>();
            services.AddTransient<CodeGenerator>();
            var serviceProvider = services.BuildServiceProvider();
            var codeGenerator = serviceProvider.GetRequiredService<CodeGenerator>();
            codeGenerator.GenerateCode(new List<string>() { "SysUser" }, EnumCodeTemplate.All, true);
            //codeGenerator.GenerateCode(EnumCodeTemplate.IRepository, true);
            //codeGenerator.GenerateCode(EnumCodeTemplate.Model, true);
            //codeGenerator.GenerateCode(EnumCodeTemplate.ViewModel, true);

            Assert.Equal(0, 0);
        }
    }
}
