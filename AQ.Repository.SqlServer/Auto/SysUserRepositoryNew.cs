using System;
using System.Threading.Tasks;
using System.Text;
using System.Linq;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
/*[begin custom code head]*/
//自定义命名空间区域
using Dapper;
using AQ.Core;
using AQ.Core.Repository;
using AQ.Models;
using AQ.IRepository;
using AQ.ModelExtension;
using AQ.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Collections.Generic;

/*[end custom code head]*/

namespace AQ.Repository.SqlServer
{
    public class SysUserRepositoryNew : RepositoryBase<SysUser, String>, ISysUserRepositoryNew
    {

        /*[begin custom code body]*/
        #region 自定义代码区域,重新生成代码不会覆盖
        private readonly ILogger<SysUserRepository> _logger;
        public SysUserRepositoryNew(DbContextBase dbContextBase, ILogger<SysUserRepository> log) : base(dbContextBase)
        {
            _logger = log;
        }
        #endregion
        /*[end custom code body]*/

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public void DeleteLogical(String[] keys)
        {
            foreach (var key in keys)
            {
                var item = new SysUser()
                {
                    Id = key
                };
                DBContext.Attach(item);
                item.IsDelete = true;
                item.ModifyTime = DateTime.Now;
            }
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public void DeleteLogicalAsync(String[] keys)
        {
            foreach (var key in keys)
            {
                var item = new SysUser()
                {
                    Id = key
                };
                DBContext.Attach(item);
                item.IsDelete = true;
                item.ModifyTime = DateTime.Now;
            }
        }

        /// <summary>
        /// 更改状态
        /// </summary>
        /// <param name="status">状态(1:启用 其他:停用)</param>
        /// <param name="keys">主键</param>
        /// <returns></returns>
        public void UpdateStatus(int status, String[] keys)
        {
            foreach (var key in keys)
            {
                var item = new SysUser()
                {
                    Id = key,
                    Status = -1
                };
                DBContext.Attach(item);
                item.Status = status == 1 ? 1 : 0;
                item.ModifyTime = DateTime.Now;
            }
        }

        /// <summary>
        /// 更改状态
        /// </summary>
        /// <param name="status">状态(1:启用 其他:停用)</param>
        /// <param name="keys">主键</param>
        /// <returns></returns>
        public void UpdateStatusAsync(int status, String[] keys)
        {
            foreach (var key in keys)
            {
                var item = new SysUser()
                {
                    Id = key,
                    Status = -1
                };
                DBContext.Attach(item);
                item.Status = status == 1 ? status : 0;
                item.ModifyTime = DateTime.Now;
            }
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="keys">主键</param>
        /// <returns></returns>
        public void Delete(string[] keys)
        {
            var data = new List<SysUser>();
            foreach (var key in keys)
            {
                data.Add(
                    new SysUser()
                    {
                        Id = key
                    });
            }
            DBContext.Set<SysUser>().RemoveRange();
        }

        /*[begin custom code bottom]*/
        #region 自定义代码区域,重新生成代码不会覆盖

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public ListPagedResult<SysUser> GetListPaged(SysUserCondition condition)
        {
            var result = new ListPagedResult<SysUser>();
            result.TotalData = SetWhere(GetAllList(), condition).Count();
            result.GetPageCount();
            result.Data = SetWhere(GetAllList(), condition)
                    .OrderIf(condition.SortName, d => condition.SortName, condition.IsSortByDesc)
                    .Skip(condition.StartNum)
                    .Take(condition.PageSize)
                    .ToList();
            return result;
        }

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public async Task<ListPagedResult<SysUser>> GetListPagedAsync(SysUserCondition condition)
        {
            var result = GetListPaged(condition);
            return await Task.Run(() => result);
        }

        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <param name="source"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        private IQueryable<SysUser> SetWhere(IQueryable<SysUser> source, SysUserCondition condition)
        {
            return source.WhereIf(true, t => t.IsDelete == false)
                .WhereIf(!string.IsNullOrEmpty(condition.Id), t => t.Id == condition.Id)
                .WhereIf(!string.IsNullOrEmpty(condition.JobCode), t => t.JobCode == condition.JobCode)
                .WhereIf(!string.IsNullOrEmpty(condition.Mobile), t => t.Mobile == condition.Mobile)
                .WhereIf(!string.IsNullOrEmpty(condition.NickName), t => t.NickName.Contains(condition.NickName))
                .WhereIf(condition.Sex != null, t => t.Sex == condition.Sex)
                .WhereIf(!string.IsNullOrEmpty(condition.Account), t => t.Account == condition.Account)
                .WhereIf(!string.IsNullOrEmpty(condition.CName), t => t.NickName.Contains(condition.CName));
        }

        #endregion
        /*[end custom code bottom]*/
    }
}
