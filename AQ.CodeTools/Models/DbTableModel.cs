using System;
using System.Collections.Generic;
using System.Text;

namespace AQ.CodeTools
{
    /// <summary>
    /// 数据库中表模型
    /// </summary>
    [Serializable]
    public class DbTableModel
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 表说明
        /// </summary>
        public string TableComment { get; set; }

        /// <summary>
        /// 字段集合
        /// </summary>
        public virtual List<DbTableColumnModel> Columns { get; set; } = new List<DbTableColumnModel>();
    }


}
