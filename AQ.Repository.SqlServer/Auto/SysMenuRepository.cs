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
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

/*[end custom code head]*/

namespace AQ.Repository.SqlServer
{
    //public class SysMenuRepository : BaseRepository<SysMenu, String>, ISysMenuRepository
    public class SysMenuRepository : RepositoryBase<SysMenu, String>, ISysMenuRepository
    {

        /*[begin custom code body]*/
        #region 自定义代码区域,重新生成代码不会覆盖
        private readonly ILogger<SysMenuRepository> _logger;
        //public SysMenuRepository(IOptionsSnapshot<DbOption> option, ILogger<SysMenuRepository> log) : base(option.Value)
        //{
        //    _logger = log;
        //}
        public SysMenuRepository(DbContextBase dbContextBase, ILogger<SysMenuRepository> log) : base(dbContextBase)
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
                var item = new SysMenu()
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
                var item = new SysMenu()
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
                var item = new SysMenu()
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
                var item = new SysMenu()
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
            var data = new List<SysMenu>();
            foreach (var key in keys)
            {
                data.Add(
                    new SysMenu()
                    {
                        Id = key
                    });
            }
            DBContext.Set<SysMenu>().RemoveRange();
        }

        /*[begin custom code bottom]*/
        #region 自定义代码区域,重新生成代码不会覆盖

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public ListPagedResult<SysMenu> GetListPaged(SysMenuCondition condition)
        {
            //condition.Parent = "MENU0012";
            //condition.Module = "MODULE0007";
            var result = new ListPagedResult<SysMenu>();
            result.TotalData = SetWhere(GetAllList().Include(t => t.Module).Include(t => t.ParentMenu), condition).Count();
            result.GetPageCount();
            var data = SetWhere(GetAllList().Include(t => t.Module).Include(t => t.ParentMenu), condition);
            result.Data = SetWhere(data, condition)
                .OrderIf(condition.SortName, d => condition.SortName, condition.IsSortByDesc)
                .Skip(condition.StartNum)
                .Take(condition.PageSize)
                .ToList();
            return result;

            //var data = GetAllList().Join(GetAllList(), t => t.ParentId, t => t.Id, (t1, t2) => new SysMenu()
            //{
            //    Id = t1.Id,
            //    Name = t1.Name,
            //    ParentMenu = new SysMenu() { Id = t2.Id, Name = t2.Name }
            //}).ToList();
        }

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public async Task<ListPagedResult<SysMenu>> GetListPagedAsync(SysMenuCondition condition)
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
        private IQueryable<SysMenu> SetWhere(IQueryable<SysMenu> source, SysMenuCondition condition)
        {
            return source.WhereIf(true, t => t.IsDelete == false)
                .WhereIf(!string.IsNullOrEmpty(condition.Id), t => t.Id == condition.Id)
                .WhereIf(!string.IsNullOrEmpty(condition.Name), t => t.Name.Contains(condition.Name))
                .WhereIf(!string.IsNullOrEmpty(condition.Parent) && condition.Parent != "0", t => t.ParentMenu.Id == condition.Parent)
                .WhereIf(!string.IsNullOrEmpty(condition.Module), t => t.Module.Id == condition.Module);
        }

        /// <summary>
        /// 获取条件字符串
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        private StringBuilder GetConditionSql(SysMenuCondition condition)
        {
            StringBuilder sqlWhere = new StringBuilder();
            if (condition == null) return sqlWhere;

            if (condition.CreateTime != null)
            {
                sqlWhere.Append(" and a.CreateTime = @CreateTime ");
            }
            if (condition.ModifyTime != null)
            {
                sqlWhere.Append(" and a.ModifyTime = @ModifyTime ");
            }
            if (!string.IsNullOrEmpty(condition.CreateUser))
            {
                sqlWhere.Append(" and a.CreateUser like @CreateUser ");
                condition.CreateUser = $"{condition.CreateUser}%";
            }
            if (!string.IsNullOrEmpty(condition.ModifyUser))
            {
                sqlWhere.Append(" and a.ModifyUser like @ModifyUser ");
                condition.ModifyUser = $"{condition.ModifyUser}%";
            }
            if (!string.IsNullOrEmpty(condition.Id))
            {
                sqlWhere.Append(" and a.Id = @Id ");
            }
            if (!string.IsNullOrEmpty(condition.Name))
            {
                sqlWhere.Append(" and a.Name like @Name ");
                condition.Name = $"{condition.Name}%";
            }
            if (!string.IsNullOrEmpty(condition.Parent))
            {
                sqlWhere.Append(" and b.Name like @Parent ");
                condition.Parent = $"{condition.Parent}%";
            }
            if (!string.IsNullOrEmpty(condition.Module))
            {
                sqlWhere.Append(" and c.Id = @Module ");
            }
            return sqlWhere;
        }

        #endregion
        /*[end custom code bottom]*/
    }
}
