using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AQ.CodeTools
{
    public class GenerateServiceCode : BaseGenerateCode
    {
        public GenerateServiceCode(IOptions<CodeGenerateOptionModel> options) : base(options.Value)
        {
        }

        public override void ProcessTemplate(DbTableModel table, bool isExsitedCovered = false)
        {
            //模板路径
            var templatePath = !string.IsNullOrWhiteSpace(generateOptions.ServiceConfig.TemplatePath) ?
                generateOptions.ServiceConfig.TemplatePath : "ServiceTemplate.txt";
            //输出路径
            var outputPath = !string.IsNullOrWhiteSpace(generateOptions.ServiceConfig.OutputPath) ?
                generateOptions.ServiceConfig.OutputPath : string.Format("{0}Auto{1}", defaultOutputPath, delimiter);
            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }
            var fullPath = string.Format("{0}{1}Service.cs", outputPath, table.TableName);
            var isCustomCode = false;//是否处理自定义代码
            if (File.Exists(fullPath))
            {
                if (!isExsitedCovered)
                {
                    return;
                }
                else
                {
                    isCustomCode = true;
                }
            }
            ReadTemplate(templatePath);
            var newContent = ReplaceContent(table);
            if (isCustomCode)
            {
                newContent = ReplaceCustomCode(fullPath, newContent);
            }
            WriteAndSave(fullPath, newContent);
        }

        /// <summary>
        /// 替换模板参数
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        private string ReplaceContent(DbTableModel table)
        {
            return templateContent.Replace("{ServiceNamespace}", generateOptions.ServiceConfig.Namespace)
                .Replace("{ModelName}", table.TableName);
        }


    }
}
