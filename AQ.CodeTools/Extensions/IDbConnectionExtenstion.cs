using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AQ.Core;
using Dapper;
using System.Linq;

namespace AQ.CodeTools
{
    internal static class IDbConnectionExtenstion
    {
        /// <summary>
        /// 获取数据库表信息集合
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="dbType">数据库类型</param>
        /// <returns></returns>
        public static List<DbTableModel> GetTables(this IDbConnection conn, EnumDbType dbType)
        {
            var sql = GetTableSql(conn, dbType);
            return conn.Query<DbTableModel>(sql).ToList();
        }

        /// <summary>
        /// 获取数据库表集合(表信息和表字段信息)
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="dbType">数据库类型</param>
        /// <returns></returns>
        public static List<DbTableModel> GetDbTables(this IDbConnection conn, EnumDbType dbType)
        {
            var dbTables = GetTables(conn, dbType);
            dbTables.ForEach(tab =>
            {
                tab.Columns = GetDbTableColumns(conn, dbType, tab.TableName).ToList<DbTableColumnModel>();
            });
            return dbTables;
        }

        /// <summary>
        /// 获取指定表字段集合
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="dbType">数据库类型</param>
        /// <param name="tableName">表名称</param>
        /// <returns></returns>
        public static List<DbTableColumnModel> GetDbTableColumns(this IDbConnection conn, EnumDbType dbType, string tableName)
        {
            var sql = GetTableColumnsSql(conn, dbType, tableName);
            var cols = conn.Query<DbTableColumnModel>(sql).ToList();
            if (cols != null)
            {
                cols.ForEach(col =>
                {
                    var colDataType = DbColumnTypeCollectionModel.DbColumnDataTypes().FirstOrDefault(t =>
                            t.DatabaseType == dbType && t.ColumnTypes.Split(',').Any(p =>
                                p.Trim().Equals(col.ColumnType, StringComparison.OrdinalIgnoreCase)
                            )
                        );
                    var csharpType = string.Empty;
                    csharpType = colDataType != null ? colDataType.CSharpType : "";
                    if (string.IsNullOrEmpty(csharpType))
                    {
                        throw new Exception($"未从字典中找到{col.ColumnType}对应的C#数据类型，请更新DbColumnTypeCollection类型映射字典。");
                    }
                    col.CSharpType = csharpType;
                });
            }
            return cols;
        }

        /// <summary>
        /// 获取表信息sql
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <param name="dbType"></param>
        /// <returns></returns>
        private static string GetTableSql(IDbConnection dbConnection, EnumDbType dbType)
        {
            string sql = string.Empty;
            switch (dbType)
            {
                case EnumDbType.MSSQL:
                    #region sql
                    sql = @"
select * from (SELECT (case when a.colorder=1 then d.name else '' end) as TableName,
(case when a.colorder=1 then isnull(f.value,'') else '' end) as TableComment
FROM syscolumns a
inner join sysobjects d on a.id=d.id  and d.xtype='U' and  d.name<>'dtproperties'
left join sys.extended_properties f on d.id=f.major_id and f.minor_id=0) t
where t.TableName!=''
";
                    #endregion
                    break;
                case EnumDbType.MySQL:
                    #region sql
                    sql = $@"
SELECT TABLE_NAME as TableName, Table_Comment as TableComment
FROM INFORMATION_SCHEMA.TABLES
where TABLE_SCHEMA = '{dbConnection.Database}'";
                    #endregion
                    break;
                case EnumDbType.PostgreSQL:
                    #region sql
                    sql = @"
select relname as TableName,
cast(obj_description(relfilenode, 'pg_class') as varchar) as TableComment
from pg_class c
where relkind = 'r' and relname not like 'pg_%' and relname not like 'sql_%'
order by relname
";
                    #endregion
                    break;
                case EnumDbType.SQLite:
                case EnumDbType.InMemory:
                case EnumDbType.Oracle:
                case EnumDbType.MariaDB:
                case EnumDbType.MyCat:
                case EnumDbType.Firebird:
                case EnumDbType.DB2:
                case EnumDbType.Access:
                    throw new ArgumentNullException($"暂不支持{nameof(dbType)}类型数据库");
                default:
                    throw new ArgumentNullException($"暂不支持{nameof(dbType)}类型数据库");
            }
            return sql;
        }

        /// <summary>
        /// 获取表字段信息sql
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <param name="dbType"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private static string GetTableColumnsSql(IDbConnection dbConnection, EnumDbType dbType, string tableName)
        {
            string sql = string.Empty;
            switch (dbType)
            {
                case EnumDbType.MSSQL:
                    #region sql
                    sql = $@"
SELECT a.name as ColName,
CONVERT(bit,(case when COLUMNPROPERTY(a.id,a.name,'IsIdentity')=1 then 1 else 0 end)) as IsIdentity, 
CONVERT(bit,(case when (SELECT count(*) FROM sysobjects  WHERE (name in (SELECT name FROM sysindexes  WHERE (id = a.id) AND (indid in  (SELECT indid FROM sysindexkeys  WHERE (id = a.id) AND (colid in  (SELECT colid FROM syscolumns WHERE (id = a.id) AND (name = a.name)))))))  AND (xtype = 'PK'))>0 then 1 else 0 end)) as IsPrimaryKey,
b.name as ColumnType,
COLUMNPROPERTY(a.id,a.name,'PRECISION') as ColumnLength,
CONVERT(bit,(case when a.isnullable=1 then 1 else 0 end)) as IsNullable, 
isnull(e.text,'') as DefaultValue,
isnull(g.[value], ' ') AS Comment 
FROM  syscolumns a 
left join systypes b on a.xtype=b.xusertype  
inner join sysobjects d on a.id=d.id and d.xtype='U' and d.name<>'dtproperties' 
left join syscomments e on a.cdefault=e.id  
left join sys.extended_properties g on a.id=g.major_id AND a.colid=g.minor_id 
left join sys.extended_properties f on d.id=f.class and f.minor_id=0 
where b.name is not null and d.name = '{tableName}'
order by a.id,a.colorder;
";
                    #endregion
                    break;
                case EnumDbType.MySQL:
                    #region sql
                    sql = $@"
select column_name as ColName, 
column_default as DefaultValue,
IF(extra = 'auto_increment','TRUE','FALSE') as IsIdentity,
IF(is_nullable = 'YES','TRUE','FALSE') as IsNullable,
DATA_TYPE as ColumnType,
CHARACTER_MAXIMUM_LENGTH as ColumnLength,
IF(COLUMN_KEY = 'PRI','TRUE','FALSE') as IsPrimaryKey,
COLUMN_COMMENT as Comment 
from information_schema.columns where table_schema = '{dbConnection.Database}' and table_name = '{tableName}'
";
                    #endregion
                    break;
                case EnumDbType.PostgreSQL:
                    #region sql
                    sql = $@"
select column_name as ColName,
data_type as ColumnType,
coalesce(character_maximum_length, numeric_precision, -1) as ColumnLength,
CAST((case is_nullable when 'NO' then 0 else 1 end) as bool) as IsNullable,
column_default as DefaultValue,
CAST((case when position('nextval' in column_default)> 0 then 1 else 0 end) as bool) as IsIdentity, 
CAST((case when b.pk_name is null then 0 else 1 end) as bool) as IsPrimaryKey,
c.DeText as Comment
from information_schema.columns
left join 
(select pg_attr.attname as colname,pg_constraint.conname as pk_name from pg_constraint 
inner join pg_class on pg_constraint.conrelid = pg_class.oid
inner join pg_attribute pg_attr on pg_attr.attrelid = pg_class.oid and  pg_attr.attnum = pg_constraint.conkey[1]
inner join pg_type on pg_type.oid = pg_attr.atttypid where pg_class.relname = '{tableName}' and pg_constraint.contype = 'p') b on b.colname = information_schema.columns.column_name 
left join 
(select attname, description as DeText from pg_class 
left join pg_attribute pg_attr on pg_attr.attrelid = pg_class.oid
left join pg_description pg_desc on pg_desc.objoid = pg_attr.attrelid and pg_desc.objsubid = pg_attr.attnum 
where pg_attr.attnum > 0 and pg_attr.attrelid = pg_class.oid and pg_class.relname = '{tableName}') c on c.attname = information_schema.columns.column_name
where table_schema = 'public' and table_name = '{tableName}' order by ordinal_position asc
";
                    #endregion
                    break;
                case EnumDbType.SQLite:
                case EnumDbType.InMemory:
                case EnumDbType.Oracle:
                case EnumDbType.MariaDB:
                case EnumDbType.MyCat:
                case EnumDbType.Firebird:
                case EnumDbType.DB2:
                case EnumDbType.Access:
                    throw new ArgumentNullException($"暂不支持{nameof(dbType)}类型数据库");
                default:
                    throw new ArgumentNullException($"暂不支持{nameof(dbType)}类型数据库");
            }
            return sql;
        }


    }
}
