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
    public class SysPermissionRepository : BaseRepository<SysPermission, String>, ISysPermissionRepository
    {

        /*[begin custom code body]*/
        #region 自定义代码区域,重新生成代码不会覆盖
        private readonly ILogger<SysPermissionRepository> _logger;
        public SysPermissionRepository(IOptionsSnapshot<DbOption> option, ILogger<SysPermissionRepository> logger) : base(option.Value)
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
            var sql = $"update SysPermission set IsDelete = 1,ModifyTime = getdate() where Id in @Keys";
            return dbConnection.Execute(sql, new { Keys = keys });
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public async Task<int> DeleteLogicalAsync(String[] keys)
        {
            var sql = $"update SysPermission set IsDelete = 1,ModifyTime = getdate() where Id in @Keys";
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
            var sql = $"update SysPermission set Status = @Status,ModifyTime = getdate() where Id in @Keys";
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
            var sql = $"update SysPermission set Status = @Status,ModifyTime = getdate() where Id in @Keys";
            return await dbConnection.ExecuteAsync(sql, new { Status = status, Keys = keys });
        }

        /*[begin custom code bottom]*/
        #region 自定义代码区域,重新生成代码不会覆盖

        /// <summary>
        /// 获取菜单权限信息
        /// </summary>
        /// <param name="moduleId">模块id</param>
        /// <param name="roleId">角色id</param>
        /// <returns></returns>
        public List<MenuPermission> GetMenuData(string moduleId, string roleId)
        {
            UpdatePermissionByMenu();
            var data = dbConnection.Query<MenuPermission>(GetMenuDataSql().ToString(), new { PerType = "Menu", ModuleId = moduleId, RoleId = roleId });
            return data != null ? data.ToList() : new List<MenuPermission>();
        }

        /// <summary>
        /// 获取菜单权限信息
        /// </summary>
        /// <param name="moduleId">模块id</param>
        /// <param name="roleId">角色id</param>
        /// <returns></returns>
        public async Task<List<MenuPermission>> GetMenuDataAsync(string moduleId, string roleId)
        {
            UpdatePermissionByMenu();
            var dataTask = await dbConnection.QueryAsync<MenuPermission>(GetMenuDataSql().ToString(), new { PerType = "Menu", ModuleId = moduleId, RoleId = roleId });
            return dataTask != null ? dataTask.ToList() : new List<MenuPermission>();
        }

        /// <summary>
        /// 获取模块权限信息
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <returns></returns>
        public List<ModulePermission> GetModuleData(string roleId)
        {
            var result = new BaseResult<List<ModulePermission>>() { Data = new List<ModulePermission>() };
            UpdatePermissionByModule();
            var data = dbConnection.Query<ModulePermission>(GetModuleDataSql().ToString(), new { PerType = "Module", RoleId = roleId });
            return data != null ? data.ToList() : new List<ModulePermission>();
        }

        /// <summary>
        /// 获取模块权限信息
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <returns></returns>
        public async Task<List<ModulePermission>> GetModuleDataAsync(string roleId)
        {
            var result = new BaseResult<List<ModulePermission>>() { Data = new List<ModulePermission>() };
            UpdatePermissionByModule();
            var dataTask = await dbConnection.QueryAsync<ModulePermission>(GetModuleDataSql().ToString(), new { PerType = "Module", RoleId = roleId });
            return dataTask != null ? dataTask.ToList() : new List<ModulePermission>();
        }

        /// <summary>
        /// 更新菜单权限资源
        /// </summary>
        /// <returns></returns>
        private bool UpdatePermissionByMenu()
        {
            #region sql
            StringBuilder sql = new StringBuilder();
            sql.Append(@"
--添加新增菜单
INSERT INTO SysPermission(PerType,ResourceId) select 'Menu' PerType,Id ResourceId FROM SysMenu a 
WHERE a.IsDelete = 0 and not exists(select 1 from SysPermission b where a.Id = b.ResourceId)
--删除无效菜单
DELETE FROM SysPermission 
WHERE PerType = 'Menu' and not exists (select 1 from SysMenu b where SysPermission.ResourceId = b.Id and b.IsDelete = 0)
");
            #endregion
            var r = dbConnection.Execute(sql.ToString());
            return r >= 0;
        }

        /// <summary>
        /// 更新模块权限资源
        /// </summary>
        /// <returns></returns>
        private bool UpdatePermissionByModule()
        {
            #region sql
            StringBuilder sql = new StringBuilder();
            sql.Append(@"
--添加新增菜单
INSERT INTO SysPermission(PerType,ResourceId) select 'Module' PerType,Id ResourceId FROM SysModule a 
WHERE a.IsDelete = 0 and not exists(select 1 from SysPermission b where a.Id = b.ResourceId)
--删除无效菜单
DELETE FROM SysPermission 
WHERE PerType = 'Module' and not exists (select 1 from SysModule b where SysPermission.ResourceId = b.Id and b.IsDelete = 0)
");
            #endregion
            var r = dbConnection.Execute(sql.ToString());
            return r >= 0;
        }

        private StringBuilder GetMenuDataSql()
        {
            #region sql
            StringBuilder sql = new StringBuilder();
            sql.Append(@"
select 
a.Id, b.Id [SId], b.Name, b.ParentId,ISNULL(c.Operation,0) value
from SysPermission a
inner join SysMenu b on b.Id = a.ResourceId and b.IsDelete = 0
left join SysRolePermissionLink c on c.PerId = a.Id and c.RoleId=@RoleId
where a.PerType=@PerType and b.ModuleId = @ModuleId
order by b.Sort
");
            #endregion
            return sql;
        }

        private StringBuilder GetModuleDataSql()
        {
            #region sql
            StringBuilder sql = new StringBuilder();
            sql.Append(@"
select 
a.Id, b.Id [SId], b.Name, ISNULL(c.Operation,0) value
from SysPermission a
inner join SysModule b on b.Id = a.ResourceId and b.IsDelete = 0
left join SysRolePermissionLink c on c.PerId = a.Id and c.RoleId=@RoleId
where a.PerType=@PerType
order by b.Sort
");
            #endregion
            return sql;
        }

        #endregion
        /*[end custom code bottom]*/
    }
}
