using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AQ.Repositories
{
    public interface IRepositoryBase<TEntity, TKey> where TEntity : class
    {
        #region SaveChanges
        /// <summary>
        /// 保存更改到数据库
        /// </summary>
        /// <returns></returns>
        int SaveChanges();
        /// <summary>
        /// 保存更改到数据库
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync();
        #endregion

        #region Delete
        /// <summary>
        /// 删除指定主键数据
        /// </summary>
        /// <param name="id">主键值</param>
        void Delete(TKey id);
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        void Delete(TEntity entity);
        /// <summary>
        /// 删除指定主键数据
        /// </summary>
        /// <param name="id">主键值</param>
        void DeleteAsync(TKey id);
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        void DeleteAsync(TEntity entity);
        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        void DeleteList(IEnumerable<TEntity> entities);
        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="entities">实体对象数组</param>
        void DeleteList(params TEntity[] entities);
        #endregion

        #region Query
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        TEntity Get(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 查询指定条件数据集合
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IQueryable<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate = null);
        /// <summary>
        /// 查询指定条件数据集合
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IQueryable<TEntity> GetAllList(Expression<Func<TEntity, int, bool>> predicate);
        /// <summary>
        /// 查询指定条件数据数量
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int Count(Expression<Func<TEntity, bool>> predicate = null);
        /// <summary>
        /// 查询指定条件数据数量
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null);
        #endregion

        #region Add
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        void Add(TEntity entity);
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        void AddAsync(TEntity entity);
        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        void AddRange(IEnumerable<TEntity> entities);
        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="entities">实体对象数组</param>
        void AddRange(params TEntity[] entities);
        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        void AddRangeAsync(IEnumerable<TEntity> entities);
        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="entities">实体对象数组</param>
        void AddRangeAsync(params TEntity[] entities);
        #endregion

        #region Update
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        void Update(TEntity entity);

        /// <summary>
        /// 批量更新数据
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        void UpdateRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// 批量更新数据
        /// </summary>
        /// <param name="entities">实体对象数组</param>
        void UpdateRange(params TEntity[] entities);
        #endregion
    }
}
