using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AQ.Core.Repository
{
    public class BaseRepositoryNew<T, Tkey> where T : class //: IBaseRepository<T, Tkey> 
    {
        /// <summary>
        /// 数据库链接对象
        /// </summary>
        protected readonly IDbConnection dbConnection;
        protected readonly BaseDbContext dbContext;

        /// <summary>
        /// 数据库选项
        /// </summary>
        protected readonly DbOption dbOption;

        protected readonly DbSet<T> dbSet;

        public BaseRepositoryNew(DbOption option, BaseDbContext baseDbContext)
        {
            dbOption = option;
            if (dbOption == null)
            {
                throw new ArgumentNullException(nameof(option));
            }
            dbConnection = DbConnectionFactory.CreateConnection(option.DbType, option.DbConnString);
            dbContext = baseDbContext;
        }

        public void Delete(Tkey id)
        {
            var data = dbSet.Find(id);
            if (data != null)
            {
                dbSet.Remove(data);
            }
        }

        public void Delete(T entity)
        {
            if (entity != null)
            {
                dbSet.Remove(entity);
            }
        }

        public async void DeleteAsync(Tkey id)
        {
            var data = await dbSet.FindAsync(id);
            if (data != null)
            {
                dbSet.Remove(data);
            }
        }

        public void DeleteAsync(T entity)
        {
            dbSet.Remove(entity);
        }

        public void DeleteList(IEnumerable<T> entities)
        {
            if (entities != null)
            {
                dbSet.RemoveRange(entities);
            }
        }

        public void DeleteList(params T[] entities)
        {
            if (entities != null && entities.Length > 0)
            {
                dbSet.RemoveRange(entities);
            }
        }

        public T Get(Expression<Func<T, bool>> predicate)
        {
            return dbSet.FirstOrDefault(predicate);
        }

        public Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return dbSet.FirstOrDefaultAsync(predicate);
        }

        public IQueryable<T> GetAllList(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return dbSet;
            }
            return dbSet.Where(predicate);
        }

        public IQueryable<T> GetAllList(Expression<Func<T, int, bool>> predicate)
        {
            if (predicate == null)
            {
                return dbSet;
            }
            return dbSet.Where(predicate);
        }

        //public Task<IEnumerable<T>> GetListAsync(Expression<Func<T, int, bool>> predicate)
        //{
        //    return dbSet.Where(predicate);
        //}

        //public Task<IEnumerable<T>> GetListAsync(string conditions, object parameters = null)
        //{
        //}

        public void Add(T entity)
        {
            if (entity != null)
            {
                dbSet.Add(entity);
            }
        }

        public void AddAsync(T entity)
        {
            if (entity != null)
            {
                dbSet.AddAsync(entity);
            }
        }

        public void AddRange(IEnumerable<T> entities)
        {
            if (entities != null && entities.Count() > 0)
            {
                dbSet.AddRange(entities);
            }
        }

        public void AddRange(params T[] entities)
        {
            if (entities != null && entities.Length > 0)
            {
                dbSet.AddRange(entities);
            }
        }

        public void AddRangeAsync(IEnumerable<T> entities)
        {
            if (entities != null && entities.Count() > 0)
            {
                dbSet.AddRangeAsync(entities);
            }
        }
        public void AddRangeAsync(params T[] entities)
        {
            if (entities != null && entities.Length > 0)
            {
                dbSet.AddRangeAsync(entities);
            }
        }

        public int Count(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return dbSet.Count();
            }
            return dbSet.Count(predicate);
        }

        public Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return dbSet.CountAsync();
            }
            return dbSet.CountAsync(predicate);
        }

        public void Update(T entity)
        {
            if (entity != null)
            {
                dbSet.Update(entity);
            }
        }

        public void UpdateRange(T entity)
        {
            if (entity != null)
            {
                dbSet.UpdateRange(entity);
            }
        }

        #region Dispose
        private bool disposedValue = false; // 要检测冗余调用
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    dbConnection.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
