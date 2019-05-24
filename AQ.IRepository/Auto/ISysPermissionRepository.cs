using System;
using System.Threading.Tasks;
/*[begin custom code head]*/
//自定义命名空间区域
using AQ.Models;
using AQ.Core.Repository;
using AQ.ModelExtension;
using System.Collections.Generic;

/*[end custom code head]*/

namespace AQ.IRepository
{
    public interface ISysPermissionRepository : IBaseRepository<SysPermission, String>
    {
        /*[begin custom code body]*/
        #region 自定义代码区域,重新生成代码不会覆盖
        #endregion
        /*[end custom code body]*/

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        int DeleteLogical(String[] keys);

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        Task<int> DeleteLogicalAsync(String[] keys);

        /// <summary>
        /// 更改状态
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="keys">主键</param>
        /// <returns></returns>
        int UpdateStatus(int status, String[] keys);

        /// <summary>
        /// 更改状态
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="keys">主键</param>
        /// <returns></returns>
        Task<int> UpdateStatusAsync(int status, String[] keys);

        /*[begin custom code bottom]*/
        #region 自定义代码区域,重新生成代码不会覆盖

        /// <summary>
        /// 获取菜单权限信息
        /// </summary>
        /// <param name="moduleId">模块id</param>
        /// <param name="roleId">角色id</param>
        /// <returns></returns>
        List<MenuPermission> GetMenuData(string moduleId, string roleId);

        /// <summary>
        /// 获取菜单权限信息
        /// </summary>
        /// <param name="moduleId">模块id</param>
        /// <param name="roleId">角色id</param>
        /// <returns></returns>
        Task<List<MenuPermission>> GetMenuDataAsync(string moduleId, string roleId);

        /// <summary>
        /// 获取模块权限信息
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <returns></returns>
        List<ModulePermission> GetModuleData(string roleId);

        /// <summary>
        /// 获取模块权限信息
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <returns></returns>
        Task<List<ModulePermission>> GetModuleDataAsync(string roleId);

        #endregion
        /*[end custom code bottom]*/
    }
}
