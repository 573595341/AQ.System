using System;
using System.Collections.Generic;
using System.Text;

namespace AQ.ViewModels
{
    public class MenuPermissionViewModel
    {
        /// <summary>菜单id</summary>
        public string Id { get; set; }
        /// <summary>菜单名称</summary>
        public string Name { get; set; }
        /// <summary>上级菜单id</summary>
        public string ParentId { get; set; }
        /// <summary>权限值</summary>
        public Int32 Value { get; set; }
    }
}
