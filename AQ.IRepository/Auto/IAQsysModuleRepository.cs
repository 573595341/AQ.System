using System;
using AQ.Models;
using AQ.Core.Repository;
using System.Threading.Tasks;
/*[begin custom code head]*/
//自定义命名空间区域
using AQ.ModelExtension;
/*[end custom code head]*/

namespace AQ.IRepository
{
    public interface IAQsysModuleRepository : IBaseRepository<AQsysModule, String>
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

        /*[begin custom code bottom]*/
        #region 自定义代码区域,重新生成代码不会覆盖
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        ListPagedResult<AQsysModule> GetListPaged(AQsysModuleCondition condition);

        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        int UpdateStatus(string[] keys, int status);

        #endregion
        /*[end custom code bottom]*/
    }
}
