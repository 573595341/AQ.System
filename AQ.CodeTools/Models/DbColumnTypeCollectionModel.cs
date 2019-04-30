using AQ.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace AQ.CodeTools
{
    /// <summary>
    /// 各数据库中数据类型集合
    /// </summary>
    internal class DbColumnTypeCollectionModel
    {
        /// <summary>
        /// 获取各数据库中数据类型映射
        /// </summary>
        /// <returns></returns>
        public static List<DbColumnDataTypeModel> DbColumnDataTypes()
        {
            var result = new List<DbColumnDataTypeModel>() {

                #region MSSQL，https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/sql-server-data-type-mappings

                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.MSSQL, ColumnTypes = "bigint", CSharpType = "Int64" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.MSSQL, ColumnTypes = "binary,image,varbinary(max),rowversion,timestamp,varbinary", CSharpType = "Byte[]" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.MSSQL, ColumnTypes = "bit", CSharpType = "Boolean" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.MSSQL, ColumnTypes = "char,nchar,text,ntext,varchar,nvarchar", CSharpType = "String" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.MSSQL, ColumnTypes = "date,datetime,datetime2,smalldatetime", CSharpType = "DateTime" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.MSSQL, ColumnTypes = "datetimeoffset", CSharpType = "DateTimeOffset" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.MSSQL, ColumnTypes = "decimal,money,numeric,smallmoney", CSharpType = "Decimal" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.MSSQL, ColumnTypes = "float", CSharpType = "Double" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.MSSQL, ColumnTypes = "int", CSharpType = "Int32" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.MSSQL, ColumnTypes = "real", CSharpType = "Single" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.MSSQL, ColumnTypes = "smallint", CSharpType = "Int16" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.MSSQL, ColumnTypes = "sql_variant", CSharpType = "Object" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.MSSQL, ColumnTypes = "time", CSharpType = "TimeSpan" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.MSSQL, ColumnTypes = "tinyint", CSharpType = "Byte" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.MSSQL, ColumnTypes = "uniqueidentifier", CSharpType = "Guid" },

                #endregion

                #region PostgreSQL，http://www.npgsql.org/doc/types/basic.html

                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.PostgreSQL, ColumnTypes = "boolean,bit(1)", CSharpType = "Boolean" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.PostgreSQL, ColumnTypes = "smallint", CSharpType = "short" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.PostgreSQL, ColumnTypes = "integer", CSharpType = "int" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.PostgreSQL, ColumnTypes = "bigint", CSharpType = "long" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.PostgreSQL, ColumnTypes = "real", CSharpType = "float" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.PostgreSQL, ColumnTypes = "double precision", CSharpType = "Double" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.PostgreSQL, ColumnTypes = "numeric,money", CSharpType = "decimal" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.PostgreSQL, ColumnTypes = "text,character,character varying,citext,json,jsonb,xml,name", CSharpType = "String" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.PostgreSQL, ColumnTypes = "point", CSharpType = "NpgsqlTypes.NpgsqlPoint" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.PostgreSQL, ColumnTypes = "lseg", CSharpType = "NpgsqlTypes.NpgsqlLSeg" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.PostgreSQL, ColumnTypes = "path", CSharpType = "NpgsqlTypes.NpgsqlPath" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.PostgreSQL, ColumnTypes = "polygon", CSharpType = "NpgsqlTypes.NpgsqlPolygon" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.PostgreSQL, ColumnTypes = "line", CSharpType = "NpgsqlTypes.NpgsqlLine" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.PostgreSQL, ColumnTypes = "circle", CSharpType = "NpgsqlTypes.NpgsqlCircle" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.PostgreSQL, ColumnTypes = "box", CSharpType = "NpgsqlTypes.NpgsqlBox" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.PostgreSQL, ColumnTypes = "bit(n),bit varying", CSharpType = "BitArray" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.PostgreSQL, ColumnTypes = "hstore", CSharpType = "IDictionary<string, string>" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.PostgreSQL, ColumnTypes = "uuid", CSharpType = "Guid" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.PostgreSQL, ColumnTypes = "cidr", CSharpType = "ValueTuple<IPAddress, int>" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.PostgreSQL, ColumnTypes = "inet", CSharpType = "IPAddress" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.PostgreSQL, ColumnTypes = "macaddr", CSharpType = "PhysicalAddress" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.PostgreSQL, ColumnTypes = "tsquery", CSharpType = "NpgsqlTypes.NpgsqlTsQuery" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.PostgreSQL, ColumnTypes = "tsvector", CSharpType = "NpgsqlTypes.NpgsqlTsVector" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.PostgreSQL, ColumnTypes = "date,timestamp,timestamp with time zone,timestamp without time zone", CSharpType = "DateTime" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.PostgreSQL, ColumnTypes = "interval,time", CSharpType = "TimeSpan" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.PostgreSQL, ColumnTypes = "time with time zone", CSharpType = "DateTimeOffset" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.PostgreSQL, ColumnTypes = "bytea", CSharpType = "Byte[]" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.PostgreSQL, ColumnTypes = "oid,xid,cid", CSharpType = "uint" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.PostgreSQL, ColumnTypes = "oidvector", CSharpType = "uint[]" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.PostgreSQL, ColumnTypes = "char", CSharpType = "char" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.PostgreSQL, ColumnTypes = "geometry", CSharpType = "NpgsqlTypes.PostgisGeometry" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.PostgreSQL, ColumnTypes = "record", CSharpType = "object[]" },

                #endregion

                #region MySQL，https://www.devart.com/dotconnect/mysql/docs/DataTypeMapping.html

                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.MySQL, ColumnTypes = "bool,boolean,bit(1),tinyint(1)", CSharpType = "Boolean" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.MySQL, ColumnTypes = "tinyint", CSharpType = "SByte" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.MySQL, ColumnTypes = "tinyint unsigned", CSharpType = "Byte" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.MySQL, ColumnTypes = "smallint, year", CSharpType = "Int16" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.MySQL, ColumnTypes = "int, integer, smallint unsigned, mediumint", CSharpType = "Int32" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.MySQL, ColumnTypes = "bigint, int unsigned, integer unsigned, bit", CSharpType = "Int64" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.MySQL, ColumnTypes = "float", CSharpType = "Single" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.MySQL, ColumnTypes = "double, real", CSharpType = "Double" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.MySQL, ColumnTypes = "decimal, numeric, dec, fixed, bigint unsigned, float unsigned, double unsigned, serial", CSharpType = "Decimal" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.MySQL, ColumnTypes = "date, timestamp, datetime", CSharpType = "DateTime" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.MySQL, ColumnTypes = "datetimeoffset", CSharpType = "DateTimeOffset" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.MySQL, ColumnTypes = "time", CSharpType = "TimeSpan" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.MySQL, ColumnTypes = "char, varchar, tinytext, text, mediumtext, longtext, set, enum, nchar, national char, nvarchar, national varchar, character varying", CSharpType = "String" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.MySQL, ColumnTypes = "binary, varbinary, tinyblob, blob, mediumblob, longblob, char byte", CSharpType = "Byte[]" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.MySQL, ColumnTypes = "geometry", CSharpType = "System.Data.Spatial.DbGeometry" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.MySQL, ColumnTypes = "geometry", CSharpType = "System.Data.Spatial.DbGeography" },

                #endregion

                #region Oracle, https://docs.oracle.com/cd/E14435_01/win.111/e10927/featUDTs.htm#CJABAEDD

                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.Oracle, ColumnTypes = "BFILE,BLOB,RAW,LONG RAW", CSharpType = "Byte[]" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.Oracle, ColumnTypes = "CHAR, NCHAR, VARCHAR2, CLOB, NCLOB,NVARCHAR2,REF,XMLTYPE,ROWID,LONG", CSharpType = "String" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.Oracle, ColumnTypes = "BINARY FLOAT,BINARY DOUBLE", CSharpType = "System.Byte" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.Oracle, ColumnTypes = "INTERVAL YEAR TO MONTH", CSharpType = "Int32" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.Oracle, ColumnTypes = "FLOAT,INTEGER,NUMBER", CSharpType = "Decimal" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.Oracle, ColumnTypes = "DATE, TIMESTAMP, TIMESTAMP WITH LOCAL TIME ZONE,TIMESTAMP WITH TIME ZONE", CSharpType = "DateTime" },
                new DbColumnDataTypeModel() { DatabaseType = EnumDbType.Oracle, ColumnTypes = "INTERVAL DAY TO SECOND", CSharpType = "TimeSpan" }

                #endregion
            };
            return result;
        }
    }

    /// <summary>
    /// 数据字段类型
    /// </summary>
    public class DbColumnDataTypeModel
    {
        /// <summary>
        /// 数据库类型
        /// </summary>
        public EnumDbType DatabaseType { get; set; }

        /// <summary>
        /// 对应数据库中字段类型
        /// </summary>
        public string ColumnTypes { get; set; }

        /// <summary>
        /// 对应c#数据类型
        /// </summary>
        public string CSharpType { get; set; }
    }
}
