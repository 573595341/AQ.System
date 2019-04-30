using AQ.IRepository;
using AQ.Models;
using AQ.Core;
using AQ.Core.Repository;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Dapper;
/*[begin custom code head]*/
//自定义命名空间区域
using System.Text;
using Microsoft.Extensions.Logging;
using AQ.ModelExtension;
using System.Linq;
/*[end custom code head]*/

namespace AQ.Repository.SqlServer
{
    public class AQsysMenuRepository : BaseRepository<AQsysMenu, String>, IAQsysMenuRepository
    {

        /*[begin custom code body]*/
        #region 自定义代码区域,重新生成代码不会覆盖
        private ILogger<AQsysMenuRepository> logger;

        public AQsysMenuRepository(IOptionsSnapshot<DbOption> option, ILogger<AQsysMenuRepository> log) : base(option.Value)
        {
            //dbOption = option.Value;
            //if (dbOption == null)
            //{
            //    throw new ArgumentNullException(nameof(option));
            //}
            //dbConnection = DbConnectionFactory.CreateConnection(option.Value.DbType, option.Value.DbConnString);
            logger = log;
        }
        #endregion
        /*[end custom code body]*/



        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public int DeleteLogical(String[] keys)
        {
            var sql = $"update AQsysMenu set IsDelete = 1,ModifyTime = getdate() where MenuId in @Keys";
            return dbConnection.Execute(sql, new { Keys = keys });
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public async Task<int> DeleteLogicalAsync(String[] keys)
        {
            var sql = $"update AQsysMenu set IsDelete = 1,ModifyTime = getdate() where MenuId in @Keys";
            return await dbConnection.ExecuteAsync(sql, new { Keys = keys });
        }

        /*[begin custom code bottom]*/
        #region 自定义代码区域,重新生成代码不会覆盖

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public ListPagedResult<AQsysMenu> GetListPaged(SysMenuCondition condition)
        {
            var result = new ListPagedResult<AQsysMenu>();
            var sqlWhere = GetConditionSql(condition);
            result.TotalData = GetDataCount(condition, sqlWhere);
            result.GetPageCount();
            #region sql
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"
select * from (
    select
    ROW_NUMBER() over(order by a.{3} {4}) as RowNum
    ,a.[MenuId]
    ,a.[MenuName] 
    ,a.[MenuParentId]
    ,a.[ModuleId]
    ,a.[MenuType]
    ,a.[MenuLevel]
    ,a.[MenuPageUrl]
    ,a.[MenuIco]
    ,a.[Status]
    ,a.[Sort]
    ,a.[CreateTime]
    ,a.[ModifyTime]
    ,a.[CreateUser]
    ,a.[ModifyUser]
    ,a.[IsDelete]
    ,case b.MenuName when '0' then '无' else b.MenuName end ParentName
    ,c.ModuleName
    FROM AQsysMenu a
    INNER JOIN AQsysModule c on c.ModuleId = a.ModuleId
    LEFT JOIN AQsysMenu b on b.MenuId = a.MenuParentId
    WHERE a.IsDelete = 0 {0}
) as t where RowNum between {1} and {2}
", sqlWhere, condition.StartNum, condition.EndNum, string.IsNullOrEmpty(condition.SortName) ? "Sort" : condition.SortName, condition.IsSortByDesc ? "desc" : "asc");
            #endregion
            result.Data = dbConnection.Query<AQsysMenu>(sql.ToString(), condition).ToList();
            return result;
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int UpdateStatus(string[] keys, int status)
        {
            var sql = $"update AQsysMenu set Status = @Status,ModifyTime = getdate() where MenuId in @Keys";
            return dbConnection.Execute(sql, new { Status = status, Keys = keys });
        }

        /// <summary>
        /// 获取分页列表总条数
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        private int GetDataCount(SysMenuCondition condition, string sqlWhere)
        {
            #region sql
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"
select
    count(1)
    FROM AQsysMenu a
    INNER JOIN AQsysModule c on c.ModuleId = a.ModuleId
    LEFT JOIN AQsysMenu b on b.MenuId = a.MenuParentId
    WHERE a.IsDelete = 0 {0}
", sqlWhere);
            #endregion
            return dbConnection.ExecuteScalar<int>(sql.ToString(), condition);
        }

        /// <summary>
        /// 获取条件字符串
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        private string GetConditionSql(SysMenuCondition condition)
        {
            if (condition == null) return string.Empty;

            StringBuilder sqlWhere = new StringBuilder();
            if (condition.IsDelete != null)
            {
                sqlWhere.Append(" and a.IsDelete = @IsDelete ");
            }
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
                sqlWhere.Append(" and a.MenuId = @Id ");
            }
            if (!string.IsNullOrEmpty(condition.Name))
            {
                sqlWhere.Append(" and a.MenuName like @Name ");
                condition.Name = $"{condition.Name}%";
            }
            if (!string.IsNullOrEmpty(condition.Parent))
            {
                sqlWhere.Append(" and b.MenuName like @Parent ");
                condition.Parent = $"{condition.Parent}%";
            }
            if (!string.IsNullOrEmpty(condition.Module))
            {
                sqlWhere.Append(" and c.ModuleId = @Module ");
            }
            return sqlWhere.ToString();
        }

        #endregion
        /*[end custom code bottom]*/
    }
}
