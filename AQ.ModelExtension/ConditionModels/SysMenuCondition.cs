using AQ.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AQ.ModelExtension
{
    public class SysMenuCondition : ListPagedRequest
    {
        /// <summary>菜单Id</summary>
        public String Id { get; set; }

        /// <summary>菜单Name</summary>
        public String Name { get; set; }

        /// <summary>上级菜单</summary>
        public string Parent { get; set; }

        /// <summary>所属模块</summary>
        public string Module { get; set; }

        /// <summary>CreateTime</summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>ModifyTime</summary>
        public DateTime? ModifyTime { get; set; }

        /// <summary>CreateUser</summary>
        public String CreateUser { get; set; }

        /// <summary>ModifyUser</summary>
        public String ModifyUser { get; set; }

        /// <summary>IsDelete</summary>
        public Boolean? IsDelete { get; set; }
    }
}
