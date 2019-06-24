using AQ.Core.Extensions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AQ.CodeTools
{
    /// <summary>
    /// model
    /// </summary>
    public class GenerateModelCode : BaseGenerateCode
    {
        /// <summary>忽略新增字段集合</summary>
        private List<string> ignoreInsert;
        /// <summary>忽略更新字段集合</summary>
        private List<string> ignoreUpdate;
        /// <summary>忽略查询字段集合</summary>
        private List<string> ignoreSelect;

        //private Ignore
        public GenerateModelCode(IOptions<CodeGenerateOptionModel> options) : base(options.Value)
        {
            ignoreSelect = new List<string>();
            ignoreInsert = new List<string>()
            {
                //"CreateTime","ModifyTime","IsDelete"
            };
            ignoreUpdate = new List<string>()
            {
                "CreateTime","CreateUser","IsDelete"
            };
        }

        /// <summary>
        /// 处理模板信息
        /// </summary>
        /// <param name="table">数据表信息</param>
        /// <param name="isExsitedCovered">是否覆盖已存在文件</param>
        /// <returns></returns>
        public override void ProcessTemplate(DbTableModel table, bool isExsitedCovered = false)
        {
            //模板路径
            var templatePath = !string.IsNullOrWhiteSpace(generateOptions.ModelsConfig.TemplatePath) ?
                generateOptions.ModelsConfig.TemplatePath : "ModelTemplate.txt";
            //输出路径
            var outputPath = !string.IsNullOrWhiteSpace(generateOptions.ModelsConfig.OutputPath) ?
                generateOptions.ModelsConfig.OutputPath : string.Format("{0}Auto{1}", defaultOutputPath, delimiter);
            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }
            var fullPath = string.Format("{0}{1}.cs", outputPath, table.TableName);
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
            return templateContent.Replace("{ModelsNamespace}", generateOptions.ModelsConfig.Namespace)
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
            if (column.IsPrimaryKey)
            {
                sb.AppendLine("\t\t[Required]");
                sb.AppendLine("\t\t[Key]");
                //sb.AppendLine($"\t\t[Column(\"{tableName}Id\")]");
                //if (column.IsIdentity)
                //{
                //    sb.AppendLine("\t\t[DatabaseGenerated(DatabaseGeneratedOption.Identity)]");
                //}
                //sb.AppendLine($"\t\tpublic override {column.CSharpType} Id " + "{get;set;}");
            }
            else
            {
                if (!column.IsNullable)
                {
                    sb.AppendLine("\t\t[Required]");
                }
                if (column.ColumnLength.HasValue && column.ColumnLength.Value > 0)
                {
                    sb.AppendLine($"\t\t[MaxLength({column.ColumnLength.Value})]");
                }
                //if (column.IsIdentity)
                //{
                //    sb.AppendLine("\t\t[DatabaseGenerated(DatabaseGeneratedOption.Identity)]");
                //}

                if (ignoreInsert.Exists(s => s == column.ColName))
                {
                    sb.AppendLine($"\t\t[IgnoreInsert]");
                }
                if (ignoreUpdate.Exists(s => s == column.ColName))
                {
                    sb.AppendLine($"\t\t[IgnoreUpdate]");
                }
                if (ignoreSelect.Exists(s => s == column.ColName))
                {
                    sb.AppendLine($"\t\t[IgnoreSelect]");
                }
            }
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
