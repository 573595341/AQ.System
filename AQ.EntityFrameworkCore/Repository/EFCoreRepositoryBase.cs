using AQ.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AQ.EntityFrameworkCore.Repository
{
    public class EFCoreRepositoryBase<TEntity, TKey> : IRepositoryBase<TEntity, TKey> where TEntity : class
    {
        /// <summary>
        /// 数据库上下文
        /// </summary>
        public DbContextBase DBContext { get; private set; }
        /// <summary>
        /// 实体对象集合
        /// </summary>
        private readonly DbSet<TEntity> _dbSet;

        public EFCoreRepositoryBase(DbContextBase baseDbContext)
        {
            DBContext = baseDbContext;
            _dbSet = DBContext.Set<TEntity>();
        }

        #region SaveChanges
        /// <summary>
        /// 保存更改到数据库
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            return DBContext.SaveChanges();
        }
        /// <summary>
        /// 保存更改到数据库
        /// </summary>
        /// <returns></returns>
        public async Task<int> SaveChangesAsync()
        {
            return await DBContext.SaveChangesAsync();
        }
        #endregion

        #region Delete
        /// <summary>
        /// 删除指定主键数据
        /// </summary>
        /// <param name="id">主键值</param>
        public void Delete(TKey id)
        {
            var data = _dbSet.Find(id);
            if (data != null)
            {
                _dbSet.Remove(data);
            }
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        public void Delete(TEntity entity)
        {
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }
        /// <summary>
        /// 删除指定主键数据
        /// </summary>
        /// <param name="id">主键值</param>
        public async void DeleteAsync(TKey id)
        {
            var data = await _dbSet.FindAsync(id);
            if (data != null)
            {
                _dbSet.Remove(data);
            }
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        public void DeleteAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        public void DeleteList(IEnumerable<TEntity> entities)
        {
            if (entities != null)
            {
                _dbSet.RemoveRange(entities);
            }
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="entities">实体对象数组</param>
        public void DeleteList(params TEntity[] entities)
        {
            if (entities != null && entities.Length > 0)
            {
                _dbSet.RemoveRange(entities);
            }
        }
        #endregion

        #region Query
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.FirstOrDefault(predicate);
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.FirstOrDefaultAsync(predicate);
        }
        /// <summary>
        /// 查询指定条件数据集合
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IQueryable<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return _dbSet;
            }
            return _dbSet.Where(predicate);
        }
        /// <summary>
        /// 查询指定条件数据集合
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IQueryable<TEntity> GetAllList(Expression<Func<TEntity, int, bool>> predicate)
        {
            if (predicate == null)
            {
                return _dbSet;
            }
            return _dbSet.Where(predicate);
        }
        /// <summary>
        /// 查询指定条件数据数量
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
            {
                return _dbSet.Count();
            }
            return _dbSet.Count(predicate);
        }
        /// <summary>
        /// 查询指定条件数据数量
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return _dbSet.CountAsync();
            }
            return _dbSet.CountAsync(predicate);
        }
        #endregion

        #region Add
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        public void Add(TEntity entity)
        {
            if (entity != null)
            {
                _dbSet.Add(entity);
            }
        }
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        public void AddAsync(TEntity entity)
        {
            if (entity != null)
            {
                _dbSet.AddAsync(entity);
            }
        }
        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        public void AddRange(IEnumerable<TEntity> entities)
        {
            if (entities != null && entities.Count() > 0)
            {
                _dbSet.AddRange(entities);
            }
        }
        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="entities">实体对象数组</param>
        public void AddRange(params TEntity[] entities)
        {
            if (entities != null && entities.Length > 0)
            {
                _dbSet.AddRange(entities);
            }
        }
        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        public void AddRangeAsync(IEnumerable<TEntity> entities)
        {
            if (entities != null && entities.Count() > 0)
            {
                _dbSet.AddRangeAsync(entities);
            }
        }
        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="entities">实体对象数组</param>
        public void AddRangeAsync(params TEntity[] entities)
        {
            if (entities != null && entities.Length > 0)
            {
                _dbSet.AddRangeAsync(entities);
            }
        }
        #endregion

        #region Update
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        public void Update(TEntity entity)
        {
            if (entity != null)
            {
                _dbSet.Update(entity);
                var attrName = typeof(IgnoreUpdateAttribute).Name;
                var props = typeof(TEntity).GetProperties();
                foreach (var prop in props)
                {
                    if (prop.GetCustomAttributes(true).FirstOrDefault(attr => attr.GetType().Name == attrName) != null)
                    {
                        DBContext.Entry(entity).Property(prop.Name).IsModified = false;
                    }
                }
            }
        }
        /// <summary>
        /// 批量更新数据
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            if (entities != null)
            {
                _dbSet.UpdateRange(entities);
                var attrName = typeof(IgnoreUpdateAttribute).Name;
                var props = typeof(TEntity).GetProperties();
                foreach (var prop in props)
                {
                    if (prop.GetCustomAttributes(true).FirstOrDefault(attr => attr.GetType().Name == attrName) != null)
                    {
                        foreach (var item in entities)
                        {
                            DBContext.Entry(item).Property(prop.Name).IsModified = false;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 批量更新数据
        /// </summary>
        /// <param name="entities">实体对象数组</param>
        public void UpdateRange(params TEntity[] entities)
        {
            if (entities != null)
            {
                _dbSet.UpdateRange(entities);
            }
        }
        #endregion
    }
}
