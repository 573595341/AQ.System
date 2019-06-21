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
using AQ.EntityFrameworkCore;

/*[end custom code head]*/

namespace AQ.Repository.SqlServer
{
    public class SysUserRepositoryNew : RepositoryBase<SysUser, String>, ISysUserRepositoryNew
    {

        /*[begin custom code body]*/
        #region 自定义代码区域,重新生成代码不会覆盖
        private readonly ILogger<SysUserRepository> _logger;
        public SysUserRepositoryNew(DbContextBase dbContextBase, ILogger<SysUserRepository> log) : base(dbContextBase)
        {
            _logger = log;
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
            return DBConnection.Execute(sql, new { Keys = keys });
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public async Task<int> DeleteLogicalAsync(String[] keys)
        {
            var sql = $"update SysUser set IsDelete = 1,ModifyTime = getdate() where Id in @Keys";
            return await DBConnection.ExecuteAsync(sql, new { Keys = keys });
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
            return DBConnection.Execute(sql, new { Status = status, Keys = keys });
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
            return await DBConnection.ExecuteAsync(sql, new { Status = status, Keys = keys });
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
            //var user = Get(t => t.Id == "U0005");
            //DBContext.Attach(user);
            //var user = new SysUser() { Id = "U0005" };
            //user.CName = DateTime.Now.ToString("HHmmss");
            //user.CreateTime = DateTime.Now;
            //Update(user);
            //var r = SaveChanges();

            var result = new ListPagedResult<SysUser>();
            //var sqlWhere = GetConditionSql(condition);
            //result.TotalData = GetDataCount(condition, sqlWhere);
            //result.GetPageCount();
            //result.Data = DBConnection.Query<SysUser>(GetListSql(condition, sqlWhere).ToString(), condition).ToList();
            result.TotalData = GetWhere(GetAllList(), condition).Count();
            result.GetPageCount();
            result.Data = GetWhere(GetAllList(), condition)
                    .OrderIf(condition.SortName, d => condition.SortName, condition.IsSortByDesc)
                    .Skip(condition.StartNum).Take(condition.PageSize).ToList();
            //DBContext.Set<SysUser>().Where().OrderBy(d => d.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize);

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
            var dataTask = await DBConnection.QueryAsync<SysUser>(GetListSql(condition, sqlWhere).ToString(), condition);
            result.Data = dataTask.ToList();
            return result;
        }

        /// <summary>
        /// 获取分页列表总条数
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        private int GetDataCount(SysUserCondition condition, StringBuilder sqlWhere)
        {
            return DBConnection.ExecuteScalar<int>(GetListCountSql(condition, sqlWhere).ToString(), condition);
        }

        /// <summary>
        /// 获取分页列表总条数
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        private async Task<int> GetDataCountAsync(SysUserCondition condition, StringBuilder sqlWhere)
        {
            return await DBConnection.ExecuteScalarAsync<int>(GetListCountSql(condition, sqlWhere).ToString(), condition);
        }

        /// <summary>
        /// 获取查询列表sql语句
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        private StringBuilder GetListSql(SysUserCondition condition, StringBuilder sqlWhere)
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
", sqlWhere, condition.StartNum, condition.EndNum, string.IsNullOrEmpty(condition.SortName) ? "ModifyTime" : condition.SortName, condition.IsSortByDesc ? "desc" : "asc");
            #endregion
            return sql;
        }

        /// <summary>
        /// 获取查询列表sql语句
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        private StringBuilder GetListCountSql(SysUserCondition condition, StringBuilder sqlWhere)
        {
            StringBuilder countSql = new StringBuilder();
            countSql.AppendFormat(@" select count(Id) from SysUser where IsDelete = 0 {0} ", sqlWhere);
            return countSql;
        }

        /// <summary>
        /// 获取条件字符串
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        private StringBuilder GetConditionSql(SysUserCondition condition)
        {
            StringBuilder sqlWhere = new StringBuilder();
            if (condition == null) return sqlWhere;

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
            return sqlWhere;
        }

        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <param name="source"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        private IQueryable<SysUser> GetWhere(IQueryable<SysUser> source, SysUserCondition condition)
        {
            return source.WhereIf(!string.IsNullOrEmpty(condition.Id), t => t.Id == condition.Id)
                .WhereIf(!string.IsNullOrEmpty(condition.JobCode), t => t.JobCode == condition.JobCode)
                .WhereIf(!string.IsNullOrEmpty(condition.Mobile), t => t.Mobile == condition.Mobile)
                .WhereIf(!string.IsNullOrEmpty(condition.NickName), t => t.NickName.Contains(condition.NickName))
                .WhereIf(condition.Sex != null, t => t.Sex == condition.Sex)
                .WhereIf(!string.IsNullOrEmpty(condition.Account), t => t.Account == condition.Account)
                .WhereIf(!string.IsNullOrEmpty(condition.CName), t => t.NickName.Contains(condition.CName));
        }

        #endregion
        /*[end custom code bottom]*/
    }
}
