using System;
using System.Collections.Generic;
using System.Text;

namespace AQ.ModelExtension
{
    public class SysRoleCondition : ListPagedRequest
    {
        /// <summary>Id</summary>
		public String Id { get; set; }

        /// <summary>Name</summary>
        public String Name { get; set; }

        /// <summary>Status</summary>
        public Int32? Status { get; set; }

        /// <summary>CreateTime</summary>
        public DateTime CreateTime { get; set; }

        /// <summary>ModifyTime</summary>
        public DateTime ModifyTime { get; set; }

        /// <summary>CreateUser</summary>
        public String CreateUser { get; set; }

        /// <summary>ModifyUser</summary>
        public String ModifyUser { get; set; }

        /// <summary>IsDelete</summary>
        public Boolean? IsDelete { get; set; }
    }
}
