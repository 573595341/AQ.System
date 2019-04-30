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
using AQ.ModelExtension.ConditionModels;
using AQ.ModelExtension;
using System.Linq;
/*[end custom code head]*/

namespace AQ.Repository.SqlServer
{
    public class SysUserRepository : BaseRepository<SysUser, String>, ISysUserRepository
    {

        /*[begin custom code body]*/
        #region 自定义代码区域,重新生成代码不会覆盖
        private ILogger<SysUserRepository> logger;
        public SysUserRepository(IOptionsSnapshot<DbOption> option, ILogger<SysUserRepository> log) : base(option.Value)
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
            var sql = $"update SysUser set IsDelete = 1,ModifyTime = getdate() where Id in @Keys";
            return dbConnection.Execute(sql, new { Keys = keys });
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public async Task<int> DeleteLogicalAsync(String[] keys)
        {
            var sql = $"update SysUser set IsDelete = 1,ModifyTime = getdate() where Id in @Keys";
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
            var sql = $"update SysUser set Status = @Status,ModifyTime = getdate() where Id in @Keys";
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
            var sql = $"update SysUser set Status = @Status,ModifyTime = getdate() where Id in @Keys";
            return await dbConnection.ExecuteAsync(sql, new { Status = status, Key = keys });
        }

        /*[begin custom code bottom]*/
        #region 自定义代码区域,重新生成代码不会覆盖

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public ListPagedResult<SysUser> GetListPaged(SysUserCondition condition)
        {
            var result = new ListPagedResult<SysUser>();
            var sqlWhere = GetConditionSql(condition);
            result.TotalData = GetDataCount(condition, sqlWhere);
            result.GetPageCount();

            result.Data = dbConnection.Query<SysUser>(GetListSql(condition, sqlWhere), condition).ToList();
            return result;
        }

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public async Task<ListPagedResult<SysUser>> GetListPagedAsync(SysUserCondition condition)
        {
            var result = new ListPagedResult<SysUser>();
            var sqlWhere = GetConditionSql(condition);
            result.TotalData = await GetDataCountAsync(condition, sqlWhere);
            result.GetPageCount();
            var dataTask = await dbConnection.QueryAsync<SysUser>(GetListSql(condition, sqlWhere), condition);
            result.Data = dataTask.ToList();
            return result;
        }

        /// <summary>
        /// 获取查询列表sql语句
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        private string GetListSql(SysUserCondition condition, string sqlWhere)
        {
            #region sql
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"
select * from (
    select
    ROW_NUMBER() over(order by {3} {4}) as RowNum
    ,[Id]
    ,[Account]
    ,[Pwd]
    ,[NickName]
    ,[Mobile]
    ,[JobCode]
    ,[CName]
    ,[EName]
    ,[Alias]
    ,[Photo]
    ,[Sex]
    ,[Birthday]
    ,[IdCard]
    ,[BankCard]
    ,[PresentAddrress]
    ,[Status]
    ,[CreateTime]
    ,[ModifyTime]
    ,[CreateUser]
    ,[ModifyUser]
    ,[IsDelete]
    rownumber
    from SysUser where IsDelete = 0 {0}
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
        private int GetDataCount(SysUserCondition condition, string sqlWhere)
        {
            #region sql
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@" select count(Id) from SysUser where IsDelete = 0 {0} ", sqlWhere);
            #endregion
            return dbConnection.ExecuteScalar<int>(sql.ToString(), condition);
        }

        /// <summary>
        /// 获取分页列表总条数
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        private async Task<int> GetDataCountAsync(SysUserCondition condition, string sqlWhere)
        {
            #region sql
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@" select count(Id) from SysUser where IsDelete = 0 {0} ", sqlWhere);
            #endregion
            return await dbConnection.ExecuteScalarAsync<int>(sql.ToString(), condition);
        }

        /// <summary>
        /// 获取条件字符串
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        private string GetConditionSql(SysUserCondition condition)
        {
            if (condition == null) return string.Empty;

            StringBuilder sqlWhere = new StringBuilder();
            if (!string.IsNullOrEmpty(condition.Account))
            {
                sqlWhere.Append(" and Account = @Account ");
            }
            if (!string.IsNullOrEmpty(condition.CName))
            {
                sqlWhere.Append(" and CName like @CName ");
                condition.CName = $"{condition.CName}%";
            }
            if (!string.IsNullOrEmpty(condition.Id))
            {
                sqlWhere.Append(" and Id = @Id ");
            }
            if (!string.IsNullOrEmpty(condition.JobCode))
            {
                sqlWhere.Append(" and JobCode = @JobCode ");
            }
            if (!string.IsNullOrEmpty(condition.Mobile))
            {
                sqlWhere.Append(" and Mobile = @Mobile ");
            }
            if (!string.IsNullOrEmpty(condition.NickName))
            {
                sqlWhere.Append(" and NickName like @NickName ");
                condition.NickName = $"{condition.NickName}%";
            }
            if (condition.Sex != null)
            {
                sqlWhere.Append(" and Sex = @Sex ");
            }
            if (condition.Status != null)
            {
                sqlWhere.Append(" and Status = @Status ");
            }
            return sqlWhere.ToString();
        }

        #endregion
        /*[end custom code bottom]*/
    }
}
