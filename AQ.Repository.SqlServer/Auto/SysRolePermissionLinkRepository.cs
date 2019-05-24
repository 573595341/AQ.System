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
using System.Collections.Generic;

/*[end custom code head]*/

namespace AQ.Repository.SqlServer
{
    public class SysRolePermissionLinkRepository : BaseRepository<SysRolePermissionLink, Int64>, ISysRolePermissionLinkRepository
    {

        /*[begin custom code body]*/
        #region 自定义代码区域,重新生成代码不会覆盖
        private readonly ILogger<SysRolePermissionLinkRepository> _logger;
        public SysRolePermissionLinkRepository(IOptionsSnapshot<DbOption> option, ILogger<SysRolePermissionLinkRepository> logger) : base(option.Value)
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
        public int DeleteLogical(Int64[] keys)
        {
            var sql = $"update SysRolePermissionLink set IsDelete = 1,ModifyTime = getdate() where Id in @Keys";
            return dbConnection.Execute(sql, new { Keys = keys });
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public async Task<int> DeleteLogicalAsync(Int64[] keys)
        {
            var sql = $"update SysRolePermissionLink set IsDelete = 1,ModifyTime = getdate() where Id in @Keys";
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
            var sql = $"update SysRolePermissionLink set Status = @Status,ModifyTime = getdate() where Id in @Keys";
            return dbConnection.Execute(sql, new { Status = status, Keys = keys });
        }

        /// <summary>
        /// 更改状态
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="keys">主键</param>
        /// <returns></returns>
        public async Task<int> UpdateStatusAsync(int status, Int64[] keys)
        {
            var sql = $"update SysRolePermissionLink set Status = @Status,ModifyTime = getdate() where Id in @Keys";
            return await dbConnection.ExecuteAsync(sql, new { Status = status, Keys = keys });
        }

        /*[begin custom code bottom]*/
        #region 自定义代码区域,重新生成代码不会覆盖

        /// <summary>
        /// 更新菜单权限
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <param name="data">权限信息</param>
        /// <returns></returns>
        public bool UpdateMenu(string roleId, List<SysRolePermissionLink> data)
        {
            dbConnection.Open();
            var tran = dbConnection.BeginTransaction();
            try
            {
                dbConnection.Execute(DeleteMenuSql().ToString(), new { PerType = new List<string>() { "Menu", "Module" }, RoleId = roleId }, tran);
                dbConnection.Execute(AddSql().ToString(), data, tran);
                tran.Commit();
                return true;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw ex;
            }
            finally
            {
                tran.Dispose();
                dbConnection.Close();
            }
        }

        /// <summary>
        /// 更新菜单权限
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <param name="data">权限信息</param>
        /// <returns></returns>
        public async Task<bool> UpdateMenuAsync(string roleId, List<SysRolePermissionLink> data)
        {
            var tran = dbConnection.BeginTransaction();
            try
            {
                await dbConnection.ExecuteAsync(DeleteMenuSql().ToString(), new { PerType = new List<string>() { "Menu", "Module" }, RoleId = roleId }, tran);
                await dbConnection.ExecuteAsync(AddSql().ToString(), data, tran);
                tran.Commit();
                return true;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw ex;
            }
            finally
            {
                tran.Dispose();
            }
        }

        private StringBuilder DeleteMenuSql()
        {
            #region sql
            StringBuilder sql = new StringBuilder();
            sql.Append(@"
delete from SysRolePermissionLink 
where Id in (
	select a.id
	from SysRolePermissionLink a
	inner join SysPermission b on a.PerId = b.Id
	where b.PerType in @PerType and RoleId = @RoleId
)
");
            #endregion
            return sql;
        }

        private StringBuilder AddSql()
        {
            #region sql
            StringBuilder sql = new StringBuilder();
            sql.Append(@"insert into SysRolePermissionLink(RoleId,PerId,Operation) values(@RoleId,@PerId,@Operation)");
            #endregion
            return sql;
        }

        #endregion
        /*[end custom code bottom]*/
    }
}
