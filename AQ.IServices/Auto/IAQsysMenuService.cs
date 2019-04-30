using AQ.ModelExtension;
using AQ.ViewModels;
using System;
using System.Collections.Generic;
/*[begin custom code head]*/
//自定义命名空间区域
/*[end custom code head]*/

namespace AQ.IServices
{
    public interface IAQsysMenuService
    {
        /*[begin custom code body]*/
        //自定义代码区域,重新生成代码不会覆盖
        /*[end custom code body]*/

        /*[begin custom code bottom]*/
        //自定义代码区域,重新生成代码不会覆盖

        /// <summary>
        /// 获取所有系统模块数据信息
        /// </summary>
        /// <returns></returns>
        ListPagedResult<AQsysMenuViewModel> GetListAll();

        /// <summary>
        /// 获取所有系统模块数据信息
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        ListPagedResult<AQsysMenuViewModel> GetListPaged(AQsysMenuCondition condition);

        /// <summary>
        /// 获取系统模块详情
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        BaseResult<AQsysMenuViewModel> GetDetail(string moduleId);

        /// <summary>
        /// 添加系统模块信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        BaseResult Add(AQsysMenuViewModel model);

        /// <summary>
        /// 更新系统模块信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        BaseResult Update(AQsysMenuViewModel model);

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
        BaseResult<bool> ChangeStatus(string[] keys, int status);
        /*[end custom code bottom]*/
    }
}
