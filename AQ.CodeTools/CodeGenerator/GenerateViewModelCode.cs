using AQ.Core.Extensions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AQ.CodeTools
{
    public class GenerateViewModelCode : BaseGenerateCode
    {
        public GenerateViewModelCode(IOptions<CodeGenerateOptionModel> options) : base(options.Value)
        {
        }

        public override void ProcessTemplate(DbTableModel table, bool isExsitedCovered = false)
        {
            //模板路径
            var templatePath = !string.IsNullOrWhiteSpace(generateOptions.ViewModelsConfig.TemplatePath) ?
                generateOptions.ViewModelsConfig.TemplatePath : "ViewModelTemplate.txt";
            //输出路径
            var outputPath = !string.IsNullOrWhiteSpace(generateOptions.ViewModelsConfig.OutputPath) ?
                generateOptions.ViewModelsConfig.OutputPath : string.Format("{0}Auto{1}", defaultOutputPath, delimiter);
            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }
            var fullPath = string.Format("{0}{1}ViewModel.cs", outputPath, table.TableName);
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
            var propContent = new StringBuilder();
            table.Columns.ForEach(column =>
            {
                var propStr = GenerateModelProperty(table.TableName, column);
                propContent.AppendLine(propStr);
            });
            return templateContent.Replace("{ModelsNamespace}", generateOptions.ViewModelsConfig.Namespace)
                .Replace("{Comment}", string.IsNullOrEmpty(table.TableComment.Trim()) ? table.TableName : table.TableComment)
                .Replace("{ModelName}", table.TableName)
                .Replace("{ModelProperties}", propContent.ToString());
        }

        /// <summary>
        /// 生成Entity属性
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        private string GenerateModelProperty(string tableName, DbTableColumnModel column)
        {
            var sb = new StringBuilder();
            sb.AppendLine(string.Format("\t\t/// <summary>{0}</summary>", string.IsNullOrEmpty(column.Comment.Trim()) ? column.ColName : column.Comment));

            #region MyRegion
            //if (column.IsPrimaryKey)
            //{
            //    sb.AppendLine("\t\t[Key]");
            //    sb.AppendLine($"\t\t[Column(\"{tableName}Id\")]");
            //    if (column.IsIdentity)
            //    {
            //        sb.AppendLine("\t\t[DatabaseGenerated(DatabaseGeneratedOption.Identity)]");
            //    }
            //    sb.AppendLine($"\t\tpublic override {column.CSharpType} Id " + "{get;set;}");
            //}
            //else
            //{
            //    if (!column.IsNullable)
            //    {
            //        sb.AppendLine("\t\t[Required]");
            //    }
            //    if (column.ColumnLength.HasValue && column.ColumnLength.Value > 0)
            //    {
            //        sb.AppendLine($"\t\t[MaxLength({column.ColumnLength.Value})]");
            //    }
            //    if (column.IsIdentity)
            //    {
            //        sb.AppendLine("\t\t[DatabaseGenerated(DatabaseGeneratedOption.Identity)]");
            //    }
            //} 
            #endregion

            var colType = column.CSharpType;
            if (colType.ToLower() != "string" && colType.ToLower() != "byte[]" && colType.ToLower() != "object" && column.IsNullable)
            {
                colType = string.Format("{0}?", colType);
            }
            sb.AppendLine(string.Format("\t\tpublic {0} {1} {{ get; set; }}", colType, column.ColName));
            return sb.ToString();
        }

    }
}
