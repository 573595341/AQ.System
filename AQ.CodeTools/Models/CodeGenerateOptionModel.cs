using System;
using System.Collections.Generic;
using System.Text;
using AQ.Core;

namespace AQ.CodeTools
{
    public class CodeGenerateOptionModel : DbOption
    {
        /// <summary>Models配置</summary>
        public TemplateConfig ModelsConfig { get; set; }
        public TemplateConfig ViewModelsConfig { get; set; }
        public TemplateConfig IRepositoryConfig { get; set; }
        public TemplateConfig RepositoryConfig { get; set; }
        public TemplateConfig ServiceConfig { get; set; }
        public TemplateConfig IServiceConfig { get; set; }
        public TemplateConfig ControllersConfig { get; set; }

        public CodeGenerateOptionModel()
        {
            DbType = "MSSQL";
        }
    }

    /// <summary>
    /// 模板配置
    /// </summary>
    public class TemplateConfig
    {
        /// <summary>模板路径 </summary>
        public string TemplatePath { get; set; }
        /// <summary>代码输出路径(默认在当前程序运行路径下的AutoCodes文件夹下) </summary>
        public string OutputPath { get; set; }
        /// <summary>命名空间 </summary>
        public string Namespace { get; set; }
    }
}
