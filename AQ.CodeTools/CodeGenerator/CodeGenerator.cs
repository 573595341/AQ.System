using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Text;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using AQ.Core;
using AQ.CodeTools.Enums;
using System.Threading.Tasks;

namespace AQ.CodeTools
{
    /// <summary>
    /// 代码生成
    /// </summary>
    public class CodeGenerator
    {
        /// <summary>
        /// 配置选项
        /// </summary>
        private CodeGenerateOptionModel _generateOptions;

        #region 模板处理对象
        /// <summary>Model</summary>
        private GenerateModelCode _generateModel;
        /// <summary>ViewModel</summary>
        private GenerateViewModelCode _generateViewModel;
        /// <summary>IRepository</summary>
        private GenerateIRepositoryCode _generateIRepository;
        /// <summary>Repository</summary>
        private GenerateRepositoryCode _generateRepository;
        /// <summary>IService</summary>
        private GenerateIServiceCode _generateIService;
        /// <summary>Service</summary>
        private GenerateServiceCode _generateService;
        #endregion

        public CodeGenerator(IOptions<CodeGenerateOptionModel> options
            , GenerateModelCode generateModel
            , GenerateViewModelCode generateViewModel
            , GenerateIRepositoryCode generateIRepository
            , GenerateRepositoryCode generateRepository
            , GenerateIServiceCode generateIService
            , GenerateServiceCode generateService)
        {
            _generateOptions = options.Value;
            if (string.IsNullOrWhiteSpace(_generateOptions.DbConnString))
            {
                throw new ArgumentNullException("CodeGenerateOptionModel对象DbConnString为空");
            }
            _generateModel = generateModel;
            _generateViewModel = generateViewModel;
            _generateIRepository = generateIRepository;
            _generateRepository = generateRepository;
            _generateIService = generateIService;
            _generateService = generateService;
        }

        /// <summary>
        /// 生成模板代码
        /// </summary>
        /// <param name="codeTemp">生成代码模板类型</param>
        /// <param name="isExsitedCovered">是否覆盖已存在的文件</param>
        public void GenerateCode(EnumCodeTemplate codeTemp, bool isExsitedCovered = false)
        {
            try
            {
                List<DbTableModel> dbTables = null;
                using (var conn = DbConnectionFactory.CreateConnection(_generateOptions.DbType, _generateOptions.DbConnString))
                {
                    var dbType = DbConnectionFactory.GetDbType(_generateOptions.DbType);
                    dbTables = conn.GetDbTables(dbType);
                }
                if (dbTables != null && dbTables.Count() > 0)
                {
                    dbTables.ForEach(tab =>
                    {
                        //生成Entity模型
                        GenerateCodeRoute(tab, codeTemp, isExsitedCovered);
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// 生成模板代码
        /// </summary>
        /// <param name="tableNames"></param>
        /// <param name="codeTemp">生成代码模板类型</param>
        /// <param name="isExsitedCovered">是否覆盖已存在的文件</param>
        public void GenerateCode(List<string> tableNames, EnumCodeTemplate codeTemp, bool isExsitedCovered = false)
        {
            try
            {
                List<DbTableModel> dbTables = new List<DbTableModel>();
                using (var conn = DbConnectionFactory.CreateConnection(_generateOptions.DbType, _generateOptions.DbConnString))
                {
                    var dbType = DbConnectionFactory.GetDbType(_generateOptions.DbType);
                    var tables = conn.GetTables(dbType);
                    foreach (var tab in tableNames)
                    {
                        var dbTab = tables.FirstOrDefault<DbTableModel>(t => t.TableName.ToLower().Equals(tab.ToLower()));
                        if (dbTab != null)
                        {
                            dbTab.Columns = conn.GetDbTableColumns(dbType, dbTab.TableName);
                            dbTables.Add(dbTab);
                        }
                    }
                }
                if (dbTables.Count > 0)
                {
                    dbTables.ForEach(tab =>
                    {
                        //生成Entity模型
                        GenerateCodeRoute(tab, codeTemp, isExsitedCovered);
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 根据不同模板生成不同代码
        /// </summary>
        /// <param name="table"></param>
        /// <param name="codeTemp">生成代码模板类型</param>
        /// <param name="isExsitedCovered">是否覆盖已存在的文件</param>
        private void GenerateCodeRoute(DbTableModel table, EnumCodeTemplate codeTemp, bool isExsitedCovered = false)
        {
            switch (codeTemp)
            {
                case EnumCodeTemplate.Model:
                    _generateModel.ProcessTemplate(table, isExsitedCovered);
                    break;
                case EnumCodeTemplate.ViewModel:
                    _generateViewModel.ProcessTemplate(table, isExsitedCovered);
                    break;
                case EnumCodeTemplate.IRepository:
                    _generateIRepository.ProcessTemplate(table, isExsitedCovered);
                    break;
                case EnumCodeTemplate.Repository:
                    _generateRepository.ProcessTemplate(table, isExsitedCovered);
                    break;
                case EnumCodeTemplate.IService:
                    _generateIService.ProcessTemplate(table, isExsitedCovered);
                    break;
                case EnumCodeTemplate.Service:
                    _generateService.ProcessTemplate(table, isExsitedCovered);
                    break;
                case EnumCodeTemplate.All:
                    foreach (EnumCodeTemplate item in Enum.GetValues(typeof(EnumCodeTemplate)))
                    {
                        if (item != EnumCodeTemplate.All)
                        {
                            GenerateCodeRoute(table, item, isExsitedCovered);
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
