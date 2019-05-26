using System;
using System.Collections.Generic;
using System.Text;

namespace AQ.Core.Enums
{
    /// <summary>
    /// 权限操作
    /// </summary>
    public enum EnumPermissionValue
    {
        /// <summary>查看权限</summary>
        View = 1,
        /// <summary>添加/创建权限</summary>
        Add = 2,
        /// <summary>编辑/修改权限</summary>
        Edit = 4,
        /// <summary>删除权限</summary>
        Delete = 8,
        /// <summary>下载权限</summary>
        Download = 16
    }
}
