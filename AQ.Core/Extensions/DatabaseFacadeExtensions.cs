using AQ.Core;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.EntityFrameworkCore
{
    public static class DatabaseFacadeExtensions
    {
        /// <summary>
        /// 获取数据库类型(默认MSSQL)
        /// </summary>
        /// <param name="database"></param>
        /// <returns></returns>
        public static EnumDbType GetDbType(this DatabaseFacade database)
        {
            if (database != null)
            {
                if (database.IsSqlServer())
                {
                    return EnumDbType.MSSQL;
                }
            }
            return EnumDbType.MSSQL;
        }
    }
}
