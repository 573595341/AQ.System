using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace AQ.CodeTools
{
    public abstract class BaseGenerateCode
    {
        /// <summary>分隔符，默认为windows下的\\分隔符</summary>
        protected readonly string delimiter = "\\";
        /// <summary>默认输出路径</summary>
        protected string defaultOutputPath;
        /// <summary>生成代码配置项</summary>
        protected CodeGenerateOptionModel generateOptions;
        /// <summary>模板缓存内容</summary>
        protected string templateContent = string.Empty;

        #region Private
        /// <summary>头部自定义代码起始标记</summary>
        private readonly string customHeadBegin = "/*[begin custom code head]*/";
        /// <summary>头部自定义代码结束标记</summary>
        private readonly string customHeadEnd = "/*[end custom code head]*/";
        /// <summary>主体自定义代码起始标记</summary>
        private readonly string customBodyBegin = "/*[begin custom code body]*/";
        /// <summary>主体自定义代码结束标记</summary>
        private readonly string customBodyEnd = "/*[end custom code body]*/";
        /// <summary>底部自定义代码起始标记</summary>
        private readonly string customBottomBegin = "/*[begin custom code bottom]*/";
        /// <summary>底部自定义代码结束标记</summary>
        private readonly string customBottomEnd = "/*[end custom code bottom]*/";
        #endregion

        public BaseGenerateCode(CodeGenerateOptionModel options)
        {
            generateOptions = options;
            defaultOutputPath = string.Format("{0}AutoCodes{1}", AppDomain.CurrentDomain.BaseDirectory, delimiter);
        }

        /// <summary>
        /// 读取代码模板内容
        /// </summary>
        /// <param name="templateName"></param>
        /// <returns></returns>
        protected virtual void ReadTemplate(string templateName)
        {
            if (!string.IsNullOrEmpty(templateContent))
            {
                return;
            }
            if (File.Exists(templateName))
            {
                using (var streamReader = new StreamReader(templateName))
                {
                    templateContent = streamReader.ReadToEnd();
                }
            }
            else
            {
                var assembly = Assembly.GetExecutingAssembly();
                using (var stream = assembly.GetManifestResourceStream(string.Format("{0}.CodeTemplates.{1}", assembly.GetName().Name, templateName)))
                {
                    if (stream != null)
                    {
                        using (var streamReader = new StreamReader(stream))
                        {
                            templateContent = streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="content"></param>
        protected virtual void WriteAndSave(string fileName, string content)
        {
            //实例化一个文件流--->与写入文件相关联
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                //实例化一个StreamWriter-->与fs相关联
                using (var sw = new StreamWriter(fs))
                {
                    //开始写入
                    sw.Write(content);
                    //清空缓冲区
                    sw.Flush();
                    //关闭流
                    sw.Close();
                    fs.Close();
                }
            }
        }

        /// <summary>
        /// 处理自定义代码
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="newContent"></param>
        /// <returns></returns>
        protected virtual string ReplaceCustomCode(string filePath, string newContent)
        {
            var oldContent = ReadFile(filePath);
            var headCode = MatchContent(oldContent, customHeadBegin, customHeadEnd);
            var bodyCode = MatchContent(oldContent, customBodyBegin, customBodyEnd);
            var bottomCode = MatchContent(oldContent, customBottomBegin, customBottomEnd);
            newContent = Regex.Replace(newContent, GetRegStr(customHeadBegin, customHeadEnd), headCode);
            newContent = Regex.Replace(newContent, GetRegStr(customBodyBegin, customBodyEnd), bodyCode);
            newContent = Regex.Replace(newContent, GetRegStr(customBottomBegin, customBottomEnd), bottomCode);
            return newContent;
        }

        /// <summary>
        /// 处理模板信息
        /// </summary>
        /// <param name="table">数据表信息</param>
        /// <param name="isExsitedCovered">是否覆盖已存在文件</param>
        /// <returns></returns>
        public abstract void ProcessTemplate(DbTableModel table, bool isExsitedCovered = false);

        #region private

        /// <summary>
        /// 读取文件内容
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private string ReadFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return string.Empty;
            }
            if (File.Exists(filePath))
            {
                using (var streamReader = new StreamReader(filePath))
                {
                    return streamReader.ReadToEnd();
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 匹配指定其实和结束字符串之间的内容
        /// </summary>
        /// <param name="content"></param>
        /// <param name="beginStr"></param>
        /// <param name="endStr"></param>
        /// <returns></returns>
        private string MatchContent(string content, string beginStr, string endStr)
        {
            if (string.IsNullOrEmpty(content))
            {
                return string.Empty;
            }
            var reg = GetRegStr(beginStr, endStr);
            var matchHead = Regex.Match(content, reg);
            return matchHead.Value;
        }

        /// <summary>
        /// 获取正则表达式
        /// </summary>
        /// <param name="beginStr">起始标识字符串</param>
        /// <param name="endStr">结束标识字符串</param>
        /// <returns></returns>
        private string GetRegStr(string beginStr, string endStr)
        {
            beginStr = beginStr.Replace("*", "\\*").Replace("[", "\\[").Replace("]", "\\]"); ;
            endStr = endStr.Replace("*", "\\*").Replace("[", "\\[").Replace("]", "\\]"); ;
            //var regHead = $"{ beginStr}[.\\s\\S]*?{ endStr}";
            return $"(?<=({ beginStr}))[.\\s\\S]*?(?=({ endStr}))";
        }
        #endregion
    }
}
