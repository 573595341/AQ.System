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

        public void Add(string roleId, string perType)
        {


        }

        /// <summary>
        /// 获取菜单权限信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public BaseResult<List<MenuPermission>> GetMenuData(string roleId)
        {
            var result = new BaseResult<List<MenuPermission>>() { Data = new List<MenuPermission>() };
            result.Data = dbConnection.Query<MenuPermission>(GetMenuDataSql().ToString(), new { PerType = "Menu", RoleId = roleId }).ToList();
            result.ResultCode = CommonResults.Success.ResultCode;
            result.ResultMsg = CommonResults.Success.ResultMsg;
            return result;
        }

        /// <summary>
        /// 获取菜单权限信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<BaseResult<List<MenuPermission>>> GetMenuDataAsync(string roleId)
        {
            var result = new BaseResult<List<MenuPermission>>() { Data = new List<MenuPermission>() };
            var dataTask = await dbConnection.QueryAsync<MenuPermission>(GetMenuDataSql().ToString(), new { PerType = "Menu", RoleId = roleId });
            result.Data = dataTask.ToList();
            result.ResultCode = CommonResults.Success.ResultCode;
            result.ResultMsg = CommonResults.Success.ResultMsg;
            return result;
        }

        /// <summary>
        /// 获取模块权限信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public BaseResult<List<ModulePermission>> GetModuleData(string roleId)
        {
            var result = new BaseResult<List<ModulePermission>>() { Data = new List<ModulePermission>() };
            result.Data = dbConnection.Query<ModulePermission>(GetMenuDataSql().ToString(), new { PerType = "Module", RoleId = roleId }).ToList();
            result.ResultCode = CommonResults.Success.ResultCode;
            result.ResultMsg = CommonResults.Success.ResultMsg;
            return result;
        }

        /// <summary>
        /// 获取模块权限信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<BaseResult<List<ModulePermission>>> GetModuleDataAsync(string roleId)
        {
            var result = new BaseResult<List<ModulePermission>>() { Data = new List<ModulePermission>() };
            var dataTask = await dbConnection.QueryAsync<ModulePermission>(GetMenuDataSql().ToString(), new { PerType = "Module", RoleId = roleId });
            result.Data = dataTask.ToList();
            result.ResultCode = CommonResults.Success.ResultCode;
            result.ResultMsg = CommonResults.Success.ResultMsg;
            return result;
        }

        private StringBuilder GetMenuDataSql()
        {
            #region sql
            StringBuilder sql = new StringBuilder();
            sql.Append(@"
select 
b.Id, b.Name, b.ParentId, c.Operation value
into #temp
from SysPermission a
inner join SysMenu b on b.Id = a.ResourceId
inner join SysRolePermissionLink c on c.PerId = a.Id
where a.PerType=@PerType and c.RoleId=@RoleId;
select * from #temp
union 
select Id, Name, ParentId, 0 value 
from SysMenu
where not exists(select Id from #temp where SysMenu.Id=#temp.Id);
drop table #temp;
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
b.Id, b.Name, b.ParentId, c.Operation value
into #temp
from SysPermission a
inner join SysModule b on b.Id = a.ResourceId
inner join SysRolePermissionLink c on c.PerId = a.Id
where a.PerType=@PerType and c.RoleId=@RoleId;
select * from #temp
union 
select Id, Name, ParentId, 0 value 
from SysModule
where not exists(select Id from #temp where SysModule.Id=#temp.Id);
drop table #temp;
");
            #endregion
            return sql;
        }

        private StringBuilder AddSql()
        {
            #region sql
            StringBuilder sql = new StringBuilder();
            sql.Append(@"


");

            #endregion
            return sql;
        }

        #endregion
        /*[end custom code bottom]*/
    }
}
