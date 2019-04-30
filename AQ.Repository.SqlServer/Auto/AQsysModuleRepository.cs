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
using System.Collections.Generic;
using AQ.ModelExtension;
using System.Text;
using System.Linq;
using Microsoft.Extensions.Logging;
/*[end custom code head]*/

namespace AQ.Repository.SqlServer
{
    public class AQsysModuleRepository : BaseRepository<AQsysModule, String>, IAQsysModuleRepository
    {

        /*[begin custom code body]*/
        //自定义代码区域,重新生成代码不会覆盖
        private ILogger<AQsysModuleRepository> _logger;
        public AQsysModuleRepository(IOptionsSnapshot<DbOption> option, ILogger<AQsysModuleRepository> logger) : base(option.Value)
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
        public int DeleteLogical(String[] keys)
        {
            var sql = $"update AQsysModule set IsDelete = 1,LastModifyDate = getdate() where ModuleId in @Keys";
            return dbConnection.Execute(sql, new { Keys = keys });
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public async Task<int> DeleteLogicalAsync(String[] keys)
        {
            var sql = $"update AQsysModule set IsDelete = 1,LastModifyDate = getdate() where ModuleId in @Keys";
            return await dbConnection.ExecuteAsync(sql, new { Keys = keys });
        }

        /*[begin custom code bottom]*/
        #region 自定义代码区域,重新生成代码不会覆盖

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public ListPagedResult<AQsysModule> GetListPaged(AQsysModuleCondition condition)
        {
            var result = new ListPagedResult<AQsysModule>();
            var sqlWhere = GetConditionSql(condition);
            result.TotalData = GetDataCount(condition, sqlWhere);
            result.GetPageCount();
            #region sql
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"
select * from (
    select
    ROW_NUMBER() over(order by {3} {4}) as RowNum
    ,[ModuleId]
    ,[ModuleName]
    ,[ModuleIco]
    ,[Sort]
    ,[Status]
    ,[CreateDate]
    ,[LastModifyDate]
    ,[CreateUserAccount]
    ,[ModifyUserAccount]
    ,[IsDelete]
    rownumber
    from AQsysModule where IsDelete = 0 {0}
) as t where RowNum between {1} and {2}
", sqlWhere, condition.StartNum, condition.EndNum, string.IsNullOrEmpty(condition.SortName) ? "Sort" : condition.SortName, condition.IsSortByDesc ? "desc" : "asc");
            #endregion
            result.Data = dbConnection.Query<AQsysModule>(sql.ToString(), condition).ToList();
            return result;
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int UpdateStatus(string[] keys, int status)
        {
            var sql = $"update AQsysModule set Status = @Status,LastModifyDate = getdate() where ModuleId in @Keys";
            return dbConnection.Execute(sql, new { Status = status, Keys = keys });
        }

        /// <summary>
        /// 获取分页列表总条数
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        private int GetDataCount(AQsysModuleCondition condition, string sqlWhere)
        {
            #region sql
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@" select count(ModuleId) from AQsysModule where IsDelete = 0 {0} ", sqlWhere);
            #endregion
            return dbConnection.ExecuteScalar<int>(sql.ToString(), condition);
        }

        /// <summary>
        /// 获取条件字符串
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        private string GetConditionSql(AQsysModuleCondition condition)
        {
            if (condition == null) return string.Empty;

            StringBuilder sqlWhere = new StringBuilder();
            if (condition.IsDelete != null)
            {
                sqlWhere.Append(" and IsDelete = @IsDelete ");
            }
            if (condition.CreateDate != null)
            {
                sqlWhere.Append(" and CreateDate = @CreateDate ");
            }
            if (condition.LastModifyDate != null)
            {
                sqlWhere.Append(" and LastModifyDate = @LastModifyDate ");
            }
            if (!string.IsNullOrEmpty(condition.CreateUserAccount))
            {
                sqlWhere.Append(" and CreateUserAccount like @CreateUserAccount ");
                condition.CreateUserAccount = $"{condition.CreateUserAccount}%";
            }
            if (!string.IsNullOrEmpty(condition.ModifyUserAccount))
            {
                sqlWhere.Append(" and ModifyUserAccount like @ModifyUserAccount ");
                condition.ModifyUserAccount = $"{condition.ModifyUserAccount}%";
            }
            if (!string.IsNullOrEmpty(condition.ModuleId))
            {
                sqlWhere.Append(" and ModuleId = @ModuleId ");
            }
            if (!string.IsNullOrEmpty(condition.ModuleName))
            {
                sqlWhere.Append(" and ModuleName like @ModuleName ");
                condition.ModuleName = $"{condition.ModuleName}%";
            }
            return sqlWhere.ToString();
        }
        #endregion
        /*[end custom code bottom]*/
    }
}
