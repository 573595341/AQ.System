using System;
using AQ.Models;
using AQ.Core.Repository;
using System.Threading.Tasks;
/*[begin custom code head]*/
//自定义命名空间区域
/*[end custom code head]*/

namespace AQ.IRepository
{
    public interface IAQsysKeyRegulationRepository : IBaseRepository<AQsysKeyRegulation, Int32?>
    {
		/*[begin custom code body]*/
        //自定义代码区域,重新生成代码不会覆盖
        /*[end custom code body]*/

		/// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        int DeleteLogical(Int32?[] keys);

		/// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        Task<int> DeleteLogicalAsync(Int32?[] keys);

		/*[begin custom code bottom]*/
        //自定义代码区域,重新生成代码不会覆盖
        //自定义代码区域,重新生成代码不会覆盖
        /// <summary>
        /// 生成主键
        /// </summary>
        /// <param name="keyName">key名称</param>
        /// <param name="tabName">表名称</param>
        /// <returns></returns>
        string GenerateKey(string keyName, string tabName);
        /*[end custom code bottom]*/
    }
}
