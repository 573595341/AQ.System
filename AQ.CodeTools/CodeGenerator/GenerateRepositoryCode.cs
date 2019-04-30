using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using Microsoft.Extensions.Options;

namespace AQ.CodeTools
{
    public class GenerateRepositoryCode : BaseGenerateCode
    {
        public GenerateRepositoryCode(IOptions<CodeGenerateOptionModel> options) : base(options.Value)
        {
        }

        public override void ProcessTemplate(DbTableModel table, bool isExsitedCovered = false)
        {
            //模板路径
            var templatePath = !string.IsNullOrWhiteSpace(generateOptions.RepositoryConfig.TemplatePath) ?
                generateOptions.RepositoryConfig.TemplatePath : "RepositoryTemplate.txt";
            //输出路径
            var outputPath = !string.IsNullOrWhiteSpace(generateOptions.RepositoryConfig.OutputPath) ?
                generateOptions.RepositoryConfig.OutputPath : string.Format("{0}Auto{1}", defaultOutputPath, delimiter);
            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }
            var fullPath = string.Format("{0}{1}Repository.cs", outputPath, table.TableName);
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
            var keyCol = table.Columns.FirstOrDefault(col => col.IsPrimaryKey);
            var keyName = keyCol?.ColName;
            var keyTypeName = keyCol?.CSharpType;
            return templateContent.Replace("{RepositoryNamespace}", generateOptions.RepositoryConfig.Namespace)
                .Replace("{ModelName}", table.TableName)
                .Replace("{KeyName}", keyName)
                .Replace("{KeyTypeName}", keyTypeName);
        }
    }
}
