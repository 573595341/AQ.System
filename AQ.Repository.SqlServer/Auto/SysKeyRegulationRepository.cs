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
    public class SysKeyRegulationRepository : BaseRepository<SysKeyRegulation, Int64>, ISysKeyRegulationRepository
    {

		/*[begin custom code body]*/
        #region 自定义代码区域,重新生成代码不会覆盖
        private ILogger<SysKeyRegulationRepository> logger;
        public SysKeyRegulationRepository(IOptionsSnapshot<DbOption> option, ILogger<SysKeyRegulationRepository> log) : base(option.Value)
        {
            logger = log;
        }
        #endregion
        /*[end custom code body]*/
        
		/// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public int DeleteLogical(Int64[] keys)
        {
            var sql = $"update SysKeyRegulation set IsDelete = 1,ModifyTime = getdate() where Id in @Keys";
            return dbConnection.Execute(sql, new { Keys = keys });
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public async Task<int> DeleteLogicalAsync(Int64[] keys)
        {
            var sql = $"update SysKeyRegulation set IsDelete = 1,ModifyTime = getdate() where Id in @Keys";
            return await dbConnection.ExecuteAsync(sql, new { Keys = keys });
        }

		/// <summary>
        /// 更改状态
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="keys">主键</param>
        /// <returns></returns>
        public int UpdateStatus(int status, Int64[] keys)
        {
            var sql = $"update SysKeyRegulation set Status = @Status,ModifyTime = getdate() where Id in @Keys";
            return dbConnection.Execute(sql, new { Status = status, Key = keys });
        }

        /// <summary>
        /// 更改状态
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="keys">主键</param>
        /// <returns></returns>
        public async Task<int> UpdateStatusAsync(int status, Int64[] keys)
        {
            var sql = $"update SysKeyRegulation set Status = @Status,ModifyTime = getdate() where Id in @Keys";
            return await dbConnection.ExecuteAsync(sql, new { Status = status, Key = keys });
        }

		/*[begin custom code bottom]*/
        #region 自定义代码区域,重新生成代码不会覆盖

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
                logger.LogError($"生成表{tabName}中的主键({keyName})失败; ERROR:参数为空");
                return string.Empty;
            }
            var sql = $"exec dbo.P_GenerateKey @KeyName,@TabName";
            try
            {
                var keyModel = dbConnection.QueryFirstOrDefault<SysKeyRegulation>(sql
                    , new SysKeyRegulation() { KeyName = keyName, TabName = tabName });
                if (keyModel == null)
                {
                    logger.LogError($"生成表{tabName}中的主键({keyName})失败; 尚未配置主键信息");
                    return string.Empty;
                }
                StringBuilder keyStr = new StringBuilder(keyModel.Format ?? string.Empty);
                if (!string.IsNullOrEmpty(keyModel.FormatDate))
                {
                    keyStr.Replace("FormatDate", keyModel.NowDate.Value.ToString(keyModel.FormatDate));
                }
                keyStr.Append(keyModel.Number.ToString().PadLeft(keyModel.NumberLength, '0'));
                return keyStr.ToString();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"生成表{tabName}中的主键({keyName})失败; ERROR:{ex.Message}");
            }
            return string.Empty;
        }

        #endregion
        /*[end custom code bottom]*/
    }
}
