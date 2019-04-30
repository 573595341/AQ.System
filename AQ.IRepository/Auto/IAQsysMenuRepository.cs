using System;
using AQ.Models;
using AQ.Core.Repository;
using System.Threading.Tasks;
using AQ.ModelExtension;
/*[begin custom code head]*/
//自定义命名空间区域
/*[end custom code head]*/

namespace AQ.IRepository
{
    public interface IAQsysMenuRepository : IBaseRepository<AQsysMenu, String>
    {
        /*[begin custom code body]*/
        //自定义代码区域,重新生成代码不会覆盖
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

        /*[begin custom code bottom]*/
        //自定义代码区域,重新生成代码不会覆盖

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        ListPagedResult<AQsysMenu> GetListPaged(SysMenuCondition condition);

        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        int UpdateStatus(string[] keys, int status);

        /*[end custom code bottom]*/
    }
}
