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
    public interface ISysRolePermissionLinkService
    {
        /*[begin custom code body]*/
        #region 自定义代码区域,重新生成代码不会覆盖
        #endregion
        /*[end custom code body]*/

        /*[begin custom code bottom]*/
        #region 自定义代码区域,重新生成代码不会覆盖

        /// <summary>
        /// 更新菜单权限
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <param name="moduleData">模块数据</param>
        /// <param name="menuData">菜单数据</param>
        /// <returns></returns>
        BaseResult UpdateMenu(string roleId, ModulePermissionViewModel moduleData, List<MenuPermissionViewModel> menuData);

        /// <summary>
        /// 更新菜单权限
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <param name="moduleData">模块数据</param>
        /// <param name="menuData">菜单数据</param>
        /// <returns></returns>
        Task<BaseResult> UpdateMenuAsync(string roleId, ModulePermissionViewModel moduleData, List<MenuPermissionViewModel> menuData);

        #endregion
        /*[end custom code bottom]*/
    }
}
