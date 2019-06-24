using AQ.EntityFrameworkCore;
using AQ.EntityFrameworkCore.Repository;
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
    public class RepositoryBase<TEntity, TKey> : EFCoreRepositoryBase<TEntity, TKey>, IDisposable where TEntity : class
    {
        /// <summary>
        /// 数据库链接对象
        /// </summary>
        public IDbConnection DBConnection { get; private set; }

        public RepositoryBase(DbContextBase baseDbContext) : base(baseDbContext)
        {
            DBConnection = DbConnectionFactory.CreateConnection(DBContext.Database.GetDbType(), DBContext.Database.GetDbConnection().ConnectionString);
        }

        #region Sql
        /// <summary>
        /// 执行sql查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public IQueryable<TEntity> FromSql(FormattableString sql)
        {
            return DBContext.Set<TEntity>().FromSql(sql);
        }
        /// <summary>
        /// 执行sql查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IQueryable<TEntity> FromSql(string sql, params object[] parameters)
        {
            return DBContext.Set<TEntity>().FromSql(sql, parameters);
        }
        /// <summary>
        /// 执行sql命令
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int ExecSql(FormattableString sql)
        {
            return DBContext.Database.ExecuteSqlCommand(sql);
        }
        /// <summary>
        /// 执行sql命令
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int ExecSql(string sql, params object[] parameters)
        {
            return DBContext.Database.ExecuteSqlCommand(sql, parameters);
        }
        /// <summary>
        /// 执行sql命令
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int ExecSql(string sql, IEnumerable<object> parameters)
        {
            return DBContext.Database.ExecuteSqlCommand(sql, parameters);
        }
        /// <summary>
        /// 执行sql命令
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public Task<int> ExecSqlAsync(FormattableString sql)
        {
            return DBContext.Database.ExecuteSqlCommandAsync(sql);
        }
        /// <summary>
        /// 执行sql命令
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public Task<int> ExecSqlAsync(string sql, IEnumerable<object> parameters)
        {
            return DBContext.Database.ExecuteSqlCommandAsync(sql, parameters);
        }
        /// <summary>
        /// 执行sql命令
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public Task<int> ExecSqlAsync(string sql, params object[] parameters)
        {
            return DBContext.Database.ExecuteSqlCommandAsync(sql, parameters);
        }

        #endregion

        #region Dispose
        private bool disposedValue = false; // 要检测冗余调用
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    DBConnection.Dispose();
                    DBContext.Dispose();
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
