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
      
    }
}
