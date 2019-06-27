using System;
using System.Threading.Tasks;
/*[begin custom code head]*/
//自定义命名空间区域
using AQ.Models;
using AQ.Core.Repository;
using AQ.ModelExtension;
using AQ.Repositories;

/*[end custom code head]*/

namespace AQ.IRepository
{
    //public interface ISysMenuRepository : IBaseRepository<SysMenu, String>
    public interface ISysMenuRepository : IRepositoryBase<SysMenu, String>
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
        void DeleteLogical(String[] keys);

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        void DeleteLogicalAsync(String[] keys);

        /// <summary>
        /// 更改状态
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="keys">主键</param>
        /// <returns></returns>
        void UpdateStatus(int status, String[] keys);

        /// <summary>
        /// 更改状态
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="keys">主键</param>
        /// <returns></returns>
        void UpdateStatusAsync(int status, String[] keys);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="keys">主键</param>
        /// <returns></returns>
        void Delete(string[] keys);

        /*[begin custom code bottom]*/
        #region 自定义代码区域,重新生成代码不会覆盖

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        ListPagedResult<SysMenu> GetListPaged(SysMenuCondition condition);

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        Task<ListPagedResult<SysMenu>> GetListPagedAsync(SysMenuCondition condition);

        #endregion
        /*[end custom code bottom]*/
    }
}
