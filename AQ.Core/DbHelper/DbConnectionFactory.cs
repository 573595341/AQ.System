using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using Npgsql;

namespace AQ.Core
{
    /// <summary>
    /// 数据库链接工厂
    /// </summary>
    public class DbConnectionFactory
    {
        /// <summary>
        /// 创建数据库链接
        /// </summary>
        /// <param name="dbType"></param>
        /// <param name="strConn"></param>
        /// <returns></returns>
        public static IDbConnection CreateConnection(string dbType, string strConn)
        {
            if (string.IsNullOrWhiteSpace(dbType))
            {
                throw new ArgumentNullException("参数dbType数据库类型为空");
            }
            if (string.IsNullOrWhiteSpace(strConn))
            {
                throw new ArgumentNullException("参数strConn数据库连接字符串为空");
            }
            var dbtype = GetDbType(dbType);
            return CreateConnection(dbtype, strConn);
        }

        /// <summary>
        /// 创建数据库链接
        /// </summary>
        /// <param name="dbType"></param>
        /// <param name="strConn"></param>
        /// <returns></returns>
        public static IDbConnection CreateConnection(EnumDbType dbType, string strConn)
        {
            if (string.IsNullOrWhiteSpace(strConn))
            {
                throw new ArgumentNullException("参数strConn数据库连接字符串为空");
            }
            IDbConnection result = null;
            switch (dbType)
            {
                case EnumDbType.MSSQL:
                    result = new SqlConnection(strConn);
                    break;
                case EnumDbType.MySQL:
                    result = new MySqlConnection(strConn);
                    break;
                case EnumDbType.PostgreSQL:
                    result = new NpgsqlConnection(strConn);
                    break;
                case EnumDbType.SQLite:
                case EnumDbType.InMemory:
                case EnumDbType.Oracle:
                case EnumDbType.MariaDB:
                case EnumDbType.MyCat:
                case EnumDbType.Firebird:
                case EnumDbType.DB2:
                case EnumDbType.Access:
                    throw new ArgumentNullException($"暂不支持{dbType.ToString()}类型数据库");
                default:
                    throw new ArgumentNullException($"暂不支持{dbType.ToString()}类型数据库");
            }
            return result;
        }

        /// <summary>
        /// 转换数据库类型
        /// </summary>
        /// <param name="dbType"></param>
        /// <returns></returns>
        public static EnumDbType GetDbType(string dbType)
        {
            if (string.IsNullOrWhiteSpace(dbType))
            {
                throw new ArgumentNullException("参数dbType为空");
            }
            var result = EnumDbType.MSSQL;
            foreach (EnumDbType dbtype in Enum.GetValues(typeof(EnumDbType)))
            {
                if (dbType.ToLower().Equals(dbtype.ToString().ToLower()))
                {
                    result = dbtype;
                    break;
                }
            }
            return result;
        }

    }
}
