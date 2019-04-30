using System;
using System.Collections.Generic;
/*[begin custom code head]*/
//自定义命名空间区域
using AQ.ViewModels;
using AQ.ModelExtension;

/*[end custom code head]*/

namespace AQ.IServices
{
    public interface ISysMenuService
    {
        /*[begin custom code body]*/
        #region 自定义代码区域,重新生成代码不会覆盖
        #endregion
        /*[end custom code body]*/

        /*[begin custom code bottom]*/
        #region 自定义代码区域,重新生成代码不会覆盖

        /// <summary>
        /// 获取所有系统菜单数据信息
        /// </summary>
        /// <returns></returns>
        ListPagedResult<SysMenuViewModel> GetListAll();

        /// <summary>
        /// 获取所有系统菜单数据信息
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        ListPagedResult<SysMenuViewModel> GetListPaged(SysMenuCondition condition);

        /// <summary>
        /// 获取系统菜单详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        BaseResult<SysMenuViewModel> GetDetail(string id);

        /// <summary>
        /// 添加系统菜单信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        BaseResult Add(SysMenuViewModel model);

        /// <summary>
        /// 更新系统菜单信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        BaseResult Update(SysMenuViewModel model);

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        BaseResult DeleteLogical(string[] keys);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        BaseResult Delete(string id);

        /// <summary>
        /// 更改状态
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        BaseResult ChangeStatus(string[] keys, int status);

        #endregion
        /*[end custom code bottom]*/
    }
}
