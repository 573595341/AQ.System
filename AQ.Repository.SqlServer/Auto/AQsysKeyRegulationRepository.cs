using AQ.IRepository;
using AQ.Models;
using AQ.Core;
using AQ.Core.Repository;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Dapper;
/*[begin custom code head]*/
//自定义命名空间区域
using System.Text;
using Microsoft.Extensions.Logging;
/*[end custom code head]*/

namespace AQ.Repository.SqlServer
{
    public class AQsysKeyRegulationRepository : BaseRepository<AQsysKeyRegulation, Int32?>, IAQsysKeyRegulationRepository
    {

        /*[begin custom code body]*/
        //自定义代码区域,重新生成代码不会覆盖
        private ILogger<AQsysKeyRegulationRepository> _logger;
        public AQsysKeyRegulationRepository(IOptionsSnapshot<DbOption> option, ILogger<AQsysKeyRegulationRepository> logger) : base(option.Value)
        {
            //dbOption = option.Value;
            //if (dbOption == null)
            //{
            //    throw new ArgumentNullException(nameof(option));
            //}
            //dbConnection = DbConnectionFactory.CreateConnection(option.Value.DbType, option.Value.DbConnString);
            _logger = logger;
        }
        /*[end custom code body]*/



        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public int DeleteLogical(Int32?[] keys)
        {
            var sql = $"update AQsysKeyRegulation set IsDelete = 1 where Id in @Keys";
            return dbConnection.Execute(sql, new { Keys = keys });
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public async Task<int> DeleteLogicalAsync(Int32?[] keys)
        {
            var sql = $"update AQsysKeyRegulation set IsDelete = 1 where Id in @Keys";
            return await dbConnection.ExecuteAsync(sql, new { Keys = keys });
        }

        

        /*[begin custom code bottom]*/
        //自定义代码区域,重新生成代码不会覆盖
        /// <summary>
        /// 生成主键
        /// </summary>
        /// <param name="keyName">key名称</param>
        /// <param name="tabName">表名称</param>
        /// <returns></returns>
        public string GenerateKey(string keyName, string tabName)
        {
            if (string.IsNullOrEmpty(keyName) || string.IsNullOrEmpty(tabName))
            {
                _logger.LogError($"生成表{tabName}中的主键({keyName})失败; ERROR:参数为空");
                return string.Empty;
            }
            var sql = $"exec dbo.P_GenerateKey @KeyName,@TabName";
            try
            {
                var keyModel = dbConnection.QueryFirstOrDefault<AQsysKeyRegulation>(sql
                    , new AQsysKeyRegulation() { KeyName = keyName, TabName = tabName });
                if (keyModel == null)
                {
                    _logger.LogError($"生成表{tabName}中的主键({keyName})失败; 尚未配置主键信息");
                    return string.Empty;
                }
                StringBuilder keyStr = new StringBuilder(keyModel.Format ?? string.Empty);
                if (!string.IsNullOrEmpty(keyModel.FormatDate))
                {
                    keyStr.Replace("FormatDate", keyModel.NowDate.Value.ToString(keyModel.FormatDate));
                }
                keyStr.Append(keyModel.Number.ToString().PadLeft(keyModel.NumberLength.Value, '0'));
                return keyStr.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"生成表{tabName}中的主键({keyName})失败; ERROR:{ex.Message}");
            }
            return string.Empty;
        }
        /*[end custom code bottom]*/
    }
}
