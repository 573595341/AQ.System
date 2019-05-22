using System;
using System.Threading.Tasks;
using System.Text;
using System.Linq;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
/*[begin custom code head]*/
//自定义命名空间区域
using Dapper;
using AQ.Core;
using AQ.Core.Repository;
using AQ.Models;
using AQ.IRepository;
using AQ.ModelExtension;

/*[end custom code head]*/

namespace AQ.Repository.SqlServer
{
    public class SysRoleRepository : BaseRepository<SysRole, String>, ISysRoleRepository
    {

        /*[begin custom code body]*/
        #region 自定义代码区域,重新生成代码不会覆盖
        private readonly ILogger<SysRoleRepository> _logger;
        public SysRoleRepository(IOptionsSnapshot<DbOption> option, ILogger<SysRoleRepository> logger) : base(option.Value)
        {
            _logger = logger;
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
            var sql = $"update SysRole set IsDelete = 1,ModifyTime = getdate() where Id in @Keys";
            return dbConnection.Execute(sql, new { Keys = keys });
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public async Task<int> DeleteLogicalAsync(String[] keys)
        {
            var sql = $"update SysRole set IsDelete = 1,ModifyTime = getdate() where Id in @Keys";
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
            var sql = $"update SysRole set Status = @Status,ModifyTime = getdate() where Id in @Keys";
            return dbConnection.Execute(sql, new { Status = status, Keys = keys });
        }

        /// <summary>
        /// 更改状态
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="keys">主键</param>
        /// <returns></returns>
        public async Task<int> UpdateStatusAsync(int status, String[] keys)
        {
            var sql = $"update SysRole set Status = @Status,ModifyTime = getdate() where Id in @Keys";
            return await dbConnection.ExecuteAsync(sql, new { Status = status, Keys = keys });
        }

        /*[begin custom code bottom]*/
        #region 自定义代码区域,重新生成代码不会覆盖

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public ListPagedResult<SysRole> GetListPaged(SysRoleCondition condition)
        {
            var result = new ListPagedResult<SysRole>();
            var sqlWhere = GetConditionSql(condition);
            result.TotalData = GetDataCount(condition, sqlWhere);
            result.GetPageCount();

            result.Data = dbConnection.Query<SysRole>(GetListSql(condition, sqlWhere).ToString(), condition).ToList();
            return result;
        }

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public async Task<ListPagedResult<SysRole>> GetListPagedAsync(SysRoleCondition condition)
        {
            var result = new ListPagedResult<SysRole>();
            var sqlWhere = GetConditionSql(condition);
            result.TotalData = await GetDataCountAsync(condition, sqlWhere);
            result.GetPageCount();
            var data = await dbConnection.QueryAsync<SysRole>(GetListSql(condition, sqlWhere).ToString(), condition);
            result.Data = data.ToList();
            return result;
        }


        /// <summary>
        /// 获取分页列表总条数
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        private int GetDataCount(SysRoleCondition condition, StringBuilder sqlWhere)
        {
            return dbConnection.ExecuteScalar<int>(GetListCountSql(condition, sqlWhere).ToString(), condition);
        }

        /// <summary>
        /// 获取分页列表总条数
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        private async Task<int> GetDataCountAsync(SysRoleCondition condition, StringBuilder sqlWhere)
        {
            return await dbConnection.ExecuteScalarAsync<int>(GetListCountSql(condition, sqlWhere).ToString(), condition);
        }

        /// <summary>
        /// 获取查询列表sql语句
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        private StringBuilder GetListSql(SysRoleCondition condition, StringBuilder sqlWhere)
        {
            #region sql
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"
select * from (
    select
    ROW_NUMBER() over(order by {3} {4}) as RowNum
    ,[Id]
    ,[Name]
    ,[Status]
    ,[CreateTime]
    ,[ModifyTime]
    ,[CreateUser]
    ,[ModifyUser]
    ,[IsDelete]
    from SysRole where IsDelete = 0 {0}
) as t where RowNum between {1} and {2}
", sqlWhere, condition.StartNum, condition.EndNum, string.IsNullOrEmpty(condition.SortName) ? "ModifyTime" : condition.SortName, condition.IsSortByDesc ? "desc" : "asc");
            #endregion
            return sql;
        }

        /// <summary>
        /// 获取列表总数
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        private StringBuilder GetListCountSql(SysRoleCondition condition, StringBuilder sqlWhere)
        {
            StringBuilder countSql = new StringBuilder();
            countSql.AppendFormat(@" select count(Id) from SysRole where IsDelete = 0 {0} ", sqlWhere);
            return countSql;
        }

        /// <summary>
        /// 获取条件字符串
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        private StringBuilder GetConditionSql(SysRoleCondition condition)
        {
            StringBuilder sqlWhere = new StringBuilder();
            if (condition == null) return sqlWhere;

            if (!string.IsNullOrEmpty(condition.Id))
            {
                sqlWhere.Append(" and Id = @Id ");
            }
            if (!string.IsNullOrEmpty(condition.Name))
            {
                sqlWhere.Append(" and Name like @Name ");
                condition.Name = $"{condition.Name}%";
            }
            if (condition.Status != null)
            {
                sqlWhere.Append(" and Status = @Status ");
            }
            return sqlWhere;
        }

        #endregion
        /*[end custom code bottom]*/
    }
}
