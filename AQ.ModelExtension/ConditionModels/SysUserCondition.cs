using System;
using System.Collections.Generic;
using System.Text;

namespace AQ.ModelExtension.ConditionModels
{
    public class SysUserCondition : ListPagedRequest
    {

        /// <summary>Id</summary>
        public String Id { get; set; }

        /// <summary>Account</summary>
        public String Account { get; set; }

        /// <summary>NickName</summary>
        public String NickName { get; set; }

        /// <summary>Mobile</summary>
        public String Mobile { get; set; }

        /// <summary>JobCode</summary>
        public String JobCode { get; set; }

        /// <summary>CName</summary>
        public String CName { get; set; }

        /// <summary>Sex</summary>
        public Int32? Sex { get; set; }

        /// <summary>Status</summary>
        public Int32? Status { get; set; }

    }
}
