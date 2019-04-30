
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace AQ.Core.Repository
{
    public class BaseRepository<T, Tkey> : IBaseRepository<T, Tkey> where T : class
    {
        //protected readonly DbContext dbContext;
        protected readonly DbSet<T> dbSet;

        /// <summary>
        /// 数据库链接对象
        /// </summary>
        protected readonly IDbConnection dbConnection;
        /// <summary>
        /// 数据库选项
        /// </summary>
        protected readonly DbOption dbOption;

        public BaseRepository(DbOption option)
        {
            dbOption = option;
            if (dbOption == null)
            {
                throw new ArgumentNullException(nameof(option));
            }
            dbConnection = DbConnectionFactory.CreateConnection(option.DbType, option.DbConnString);
            //var opt = new DbContextOptionsBuilder().Options;
            //dbContext = new DbContext(opt);
            //dbSet = dbContext.Set<T>();
        }

        /// <summary>
        /// 根据实体主键删除一条数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>影响的行数</returns>
        public int Delete(Tkey id)
        {
            return dbConnection.Delete<T>(id);
        }
        /// <summary>
        /// 根据实体删除一条数据
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>返回影响的行数</returns>
        public int Delete(T entity)
        {
            return dbConnection.Delete(entity);
        }
        /// <summary>
        /// 根据实体主键删除一条数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>影响的行数</returns>
        public async Task<int> DeleteAsync(Tkey id)
        {
            return await dbConnection.DeleteAsync<T>(id);
        }
        /// <summary>
        /// 根据实体删除一条数据
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>返回影响的行数</returns>
        public async Task<int> DeleteAsync(T entity)
        {
            return await dbConnection.DeleteAsync(entity);
        }
        /// <summary>
        /// 条件删除多条记录
        /// </summary>
        /// <param name="whereConditions">条件</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">超时时间</param>
        /// <returns>影响的行数</returns>
        public int DeleteList(object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return dbConnection.DeleteList<T>(whereConditions, transaction, commandTimeout);
        }
        /// <summary>
        /// 使用where子句删除多个记录
        /// </summary>
        /// <param name="conditions">wher子句</param>
        /// <param name="parameters">参数</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">超时时间</param>
        /// <returns>影响的行数</returns>
        public int DeleteList(string conditions, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return dbConnection.DeleteList<T>(conditions, parameters, transaction, commandTimeout);
        }
        /// <summary>
        /// 条件删除多条记录
        /// </summary>
        /// <param name="whereConditions">条件</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">超时时间</param>
        /// <returns>影响的行数</returns>
        public async Task<int> DeleteListAsync(object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return await dbConnection.DeleteListAsync<T>(whereConditions, transaction, commandTimeout);
        }
        /// <summary>
        /// 使用where子句删除多个记录
        /// </summary>
        /// <param name="conditions">wher子句</param>
        /// <param name="parameters">参数</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">超时时间</param>
        /// <returns>影响的行数</returns>
        public async Task<int> DeleteListAsync(string conditions, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return await dbConnection.DeleteListAsync<T>(conditions, parameters, transaction, commandTimeout);
        }
        /// <summary>
        /// 通过主键获取实体对象
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        public T Get(Tkey id)
        {
            return dbConnection.Get<T>(id);
        }
        /// <summary>
        /// 通过主键获取实体对象
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        public async Task<T> GetAsync(Tkey id)
        {
            return await dbConnection.GetAsync<T>(id);
        }
        /// <summary>
        /// 获取所有的数据
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetList()
        {
            return dbConnection.GetList<T>();
        }
        /// <summary>
        /// 执行具有条件的查询，并将结果映射到强类型列表
        /// </summary>
        /// <param name="whereConditions">条件</param>
        /// <returns></returns>
        public IEnumerable<T> GetList(object whereConditions)
        {
            return dbConnection.GetList<T>(whereConditions);
        }
        /// <summary>
        /// 带参数的查询满足条件的数据
        /// </summary>
        /// <param name="conditions">条件</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public IEnumerable<T> GetList(string conditions, object parameters = null)
        {
            return dbConnection.GetList<T>(conditions, parameters);
        }
        /// <summary>
        /// 获取所有的数据
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetListAsync()
        {
            return await dbConnection.GetListAsync<T>();
        }
        /// <summary>
        /// 执行具有条件的查询，并将结果映射到强类型列表
        /// </summary>
        /// <param name="whereConditions">条件</param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetListAsync(object whereConditions)
        {
            return await dbConnection.GetListAsync<T>(whereConditions);
        }
        /// <summary>
        /// 带参数的查询满足条件的数据
        /// </summary>
        /// <param name="conditions">条件</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetListAsync(string conditions, object parameters = null)
        {
            return await dbConnection.GetListAsync<T>(conditions, parameters);
        }
        /// <summary>
        /// 使用where子句执行查询，并将结果映射到具有Paging的强类型List
        /// </summary>
        /// <param name="pageNumber">页码</param>
        /// <param name="rowsPerPage">每页显示数据</param>
        /// <param name="conditions">查询条件</param>
        /// <param name="orderby">排序</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        //public IEnumerable<T> GetListPaged(int pageNumber, int rowsPerPage, string conditions, string orderby, object parameters = null)
        //{
        //    return dbConnection.GetListPaged<T>(pageNumber, rowsPerPage, conditions, orderby, parameters);
        //}
        /// <summary>
        /// 使用where子句执行查询，并将结果映射到具有Paging的强类型List
        /// </summary>
        /// <param name="pageNumber">页码</param>
        /// <param name="rowsPerPage">每页显示数据</param>
        /// <param name="conditions">查询条件</param>
        /// <param name="orderby">排序</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        //public async Task<IEnumerable<T>> GetListPagedAsync(int pageNumber, int rowsPerPage, string conditions, string orderby, object parameters = null)
        //{
        //    return await dbConnection.GetListPagedAsync<T>(pageNumber, rowsPerPage, conditions, orderby, parameters);
        //}

        ///// <summary>
        ///// 插入一条记录并返回主键值(自增类型返回主键值，否则返回null)
        ///// </summary>
        ///// <param name="entity"></param>
        ///// <returns></returns>
        //public int? Insert(T entity)
        //{
        //    return dbConnection.Insert(entity);
        //}

        /// <summary>
        /// 插入一条记录并返回主键值(自增类型返回主键值，否则返回null)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Tkey Insert(T entity)
        {
            return dbConnection.Insert<Tkey, T>(entity);
        }
        /// <summary>
        /// 插入一条记录并返回主键值
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int?> InsertAsync(T entity)
        {
            return await dbConnection.InsertAsync(entity);
        }
        /// <summary>
        /// 满足条件的记录数量
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int RecordCount(string conditions = "", object parameters = null)
        {
            return dbConnection.RecordCount<T>(conditions, parameters);
        }
        /// <summary>
        /// 满足条件的记录数量
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<int> RecordCountAsync(string conditions = "", object parameters = null)
        {
            return await dbConnection.RecordCountAsync<T>(conditions, parameters);
        }
        /// <summary>
        /// 更新一条数据并返回影响的行数
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>影响的行数</returns>
        public int Update(T entity)
        {
            return dbConnection.Update(entity);
        }
        /// <summary>
        /// 更新一条数据并返回影响的行数
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>影响的行数</returns>
        public async Task<int> UpdateAsync(T entity)
        {
            return await dbConnection.UpdateAsync(entity);
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
        }
        #endregion
    }
}
