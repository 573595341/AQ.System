using System;
using System.Collections.Generic;
/*[begin custom code head]*/
//自定义命名空间区域
using AQ.ViewModels;
using AQ.ModelExtension;
using System.Threading.Tasks;

/*[end custom code head]*/

namespace AQ.IServices
{
    public interface ISysPermissionService
    {
        /*[begin custom code body]*/
        #region 自定义代码区域,重新生成代码不会覆盖
        #endregion
        /*[end custom code body]*/

        /*[begin custom code bottom]*/
        #region 自定义代码区域,重新生成代码不会覆盖

        /// <summary>
        /// 获取菜单权限信息
        /// </summary>
        /// <param name="moduleId">模块id</param>
        /// <param name="roleId">角色id</param>
        /// <returns></returns>
        BaseResult<List<MenuPermissionViewModel>> GetMenuData(string moduleId, string roleId);

        /// <summary>
        /// 获取菜单权限信息
        /// </summary>
        /// <param name="moduleId">模块id</param>
        /// <param name="roleId">角色id</param>
        /// <returns></returns>
        Task<BaseResult<List<MenuPermissionViewModel>>> GetMenuDataAsync(string moduleId, string roleId);

        /// <summary>
        /// 获取模块权限信息
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <returns></returns>
        BaseResult<List<ModulePermissionViewModel>> GetModuleData(string roleId);

        /// <summary>
        /// 获取模块权限信息
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <returns></returns>
        Task<BaseResult<List<ModulePermissionViewModel>>> GetModuleDataAsync(string roleId);

        #endregion
        /*[end custom code bottom]*/
    }
}
