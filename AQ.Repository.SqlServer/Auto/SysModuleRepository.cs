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
using AQ.ModelExtension;
using System.Linq;
/*[end custom code head]*/

namespace AQ.Repository.SqlServer
{
    public class SysModuleRepository : BaseRepository<SysModule, String>, ISysModuleRepository
    {

		/*[begin custom code body]*/
        #region 自定义代码区域,重新生成代码不会覆盖
        private ILogger<SysModuleRepository> logger;
        public SysModuleRepository(IOptionsSnapshot<DbOption> option, ILogger<SysModuleRepository> log) : base(option.Value)
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
        public int DeleteLogical(String[] keys)
        {
            var sql = $"update SysModule set IsDelete = 1,ModifyTime = getdate() where Id in @Keys";
            return dbConnection.Execute(sql, new { Keys = keys });
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public async Task<int> DeleteLogicalAsync(String[] keys)
        {
            var sql = $"update SysModule set IsDelete = 1,ModifyTime = getdate() where Id in @Keys";
            return await dbConnection.ExecuteAsync(sql, new { Keys = keys });
        }

		/// <summary>
        /// 更改状态
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="keys">主键</param>
        /// <returns></returns>
        public int UpdateStatus(int status, String[] keys)
        {
            var sql = $"update SysModule set Status = @Status,ModifyTime = getdate() where Id in @Keys";
            return dbConnection.Execute(sql, new { Status = status, Key = keys });
        }

        /// <summary>
        /// 更改状态
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="keys">主键</param>
        /// <returns></returns>
        public async Task<int> UpdateStatusAsync(int status, String[] keys)
        {
            var sql = $"update SysModule set Status = @Status,ModifyTime = getdate() where Id in @Keys";
            return await dbConnection.ExecuteAsync(sql, new { Status = status, Key = keys });
        }

		/*[begin custom code bottom]*/
        #region 自定义代码区域,重新生成代码不会覆盖

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public ListPagedResult<SysModule> GetListPaged(SysModuleCondition condition)
        {
            var result = new ListPagedResult<SysModule>();
            var sqlWhere = GetConditionSql(condition);
            result.TotalData = GetDataCount(condition, sqlWhere);
            result.GetPageCount();

            result.Data = dbConnection.Query<SysModule>(GetListSql(condition, sqlWhere), condition).ToList();
            return result;
        }

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public async Task<ListPagedResult<SysModule>> GetListPagedAsync(SysModuleCondition condition)
        {
            var result = new ListPagedResult<SysModule>();
            var sqlWhere = GetConditionSql(condition);
            result.TotalData = GetDataCount(condition, sqlWhere);
            result.GetPageCount();
            var data = await dbConnection.QueryAsync<SysModule>(GetListSql(condition, sqlWhere), condition);
            result.Data = data.ToList();
            return result;
        }

        /// <summary>
        /// 获取查询列表sql语句
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        private string GetListSql(SysModuleCondition condition, string sqlWhere)
        {
            #region sql
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"
select * from (
    select
    ROW_NUMBER() over(order by {3} {4}) as RowNum
    ,[Id]
    ,[Name]
    ,[Ico]
    ,[Sort]
    ,[Status]
    ,[CreateTime]
    ,[ModifyTime]
    ,[CreateUser]
    ,[ModifyUser]
    ,[IsDelete]
    from SysModule where IsDelete = 0 {0}
) as t where RowNum between {1} and {2}
", sqlWhere, condition.StartNum, condition.EndNum, string.IsNullOrEmpty(condition.SortName) ? "Sort" : condition.SortName, condition.IsSortByDesc ? "desc" : "asc");
            #endregion
            return sql.ToString();
        }

        /// <summary>
        /// 获取分页列表总条数
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        private int GetDataCount(SysModuleCondition condition, string sqlWhere)
        {
            #region sql
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@" select count(Id) from SysModule where IsDelete = 0 {0} ", sqlWhere);
            #endregion
            return dbConnection.ExecuteScalar<int>(sql.ToString(), condition);
        }

        /// <summary>
        /// 获取条件字符串
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        private string GetConditionSql(SysModuleCondition condition)
        {
            if (condition == null) return string.Empty;

            StringBuilder sqlWhere = new StringBuilder();
            if (condition.IsDelete != null)
            {
                sqlWhere.Append(" and IsDelete = @IsDelete ");
            }
            if (condition.CreateTime != null)
            {
                sqlWhere.Append(" and CreateTime = @CreateTime ");
            }
            if (condition.ModifyTime != null)
            {
                sqlWhere.Append(" and ModifyTime = @ModifyTime ");
            }
            if (!string.IsNullOrEmpty(condition.CreateUser))
            {
                sqlWhere.Append(" and CreateUser like @CreateUser ");
                condition.CreateUser = $"{condition.CreateUser}%";
            }
            if (!string.IsNullOrEmpty(condition.ModifyUser))
            {
                sqlWhere.Append(" and ModifyUser like @ModifyUser ");
                condition.ModifyUser = $"{condition.ModifyUser}%";
            }
            if (!string.IsNullOrEmpty(condition.Id))
            {
                sqlWhere.Append(" and Id = @Id ");
            }
            if (!string.IsNullOrEmpty(condition.Name))
            {
                sqlWhere.Append(" and Name like @Name ");
                condition.Name = $"{condition.Name}%";
            }
            return sqlWhere.ToString();
        }

        #endregion
        /*[end custom code bottom]*/
    }
}
