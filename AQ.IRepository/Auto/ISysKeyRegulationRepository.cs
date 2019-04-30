using System;
using System.Threading.Tasks;
/*[begin custom code head]*/
//自定义命名空间区域
using AQ.Models;
using AQ.Core.Repository;
using AQ.ModelExtension;

/*[end custom code head]*/

namespace AQ.IRepository
{
    public interface ISysKeyRegulationRepository : IBaseRepository<SysKeyRegulation, Int64>
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
        int DeleteLogical(Int64[] keys);

		/// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        Task<int> DeleteLogicalAsync(Int64[] keys);

		/// <summary>
        /// 更改状态
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="keys">主键</param>
        /// <returns></returns>
        int UpdateStatus(int status, Int64[] keys);

        /// <summary>
        /// 更改状态
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="keys">主键</param>
        /// <returns></returns>
        Task<int> UpdateStatusAsync(int status, Int64[] keys);

		/*[begin custom code bottom]*/
        #region 自定义代码区域,重新生成代码不会覆盖

        /// <summary>
        /// 生成主键
        /// </summary>
        /// <param name="keyName">key名称</param>
        /// <param name="tabName">表名称</param>
        /// <returns></returns>
        string GenerateKey(string keyName, string tabName);

        #endregion
        /*[end custom code bottom]*/
    }
}
