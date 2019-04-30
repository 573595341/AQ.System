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
    public interface ISysModuleRepository : IBaseRepository<SysModule, String>
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
        /// 获取分页列表
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        ListPagedResult<SysModule> GetListPaged(SysModuleCondition condition);

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        Task<ListPagedResult<SysModule>> GetListPagedAsync(SysModuleCondition condition);

        #endregion
        /*[end custom code bottom]*/
    }
}
