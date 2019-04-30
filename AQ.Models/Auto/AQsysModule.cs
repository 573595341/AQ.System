using System;
using System.ComponentModel.DataAnnotations;
using Dapper;
using KeyAttribute = Dapper.KeyAttribute;
using RequiredAttribute = Dapper.RequiredAttribute;
/*[begin custom code head]*/
//自定义命名空间区域
/*[end custom code head]*/

namespace AQ.Models
{
    /// <summary>
    /// AQsysModule
    /// </summary>
    public class AQsysModule
    {

        /*[begin custom code body]*/
        //自定义代码区域,重新生成代码不会覆盖
        /*[end custom code body]*/

        /// <summary>ModuleId</summary>
        [Key]
        public String ModuleId { get; set; }

        /// <summary>ModuleName</summary>
        [MaxLength(30)]
        [Required]
        public String ModuleName { get; set; }

        /// <summary>ModuleIco</summary>
        [MaxLength(255)]
        public String ModuleIco { get; set; }

        /// <summary>Status</summary>
        [MaxLength(10)]
        public Int32? Status { get; set; }

        /// <summary>Sort</summary>
        [MaxLength(10)]
        public Int32? Sort { get; set; }

        /// <summary>CreateDate</summary>
        [IgnoreInsert]
        [IgnoreUpdate]
        [MaxLength(23)]
        public DateTime? CreateDate { get; set; }

        /// <summary>LastModifyDate</summary>
        [IgnoreInsert]
        [MaxLength(23)]
        public DateTime? LastModifyDate { get; set; }

        /// <summary>CreateUserAccount</summary>
        [IgnoreUpdate]
        [MaxLength(30)]
        public String CreateUserAccount { get; set; }

        /// <summary>ModifyUserAccount</summary>
        [MaxLength(30)]
        public String ModifyUserAccount { get; set; }

        /// <summary>IsDelete</summary>
        [IgnoreInsert]
        [IgnoreUpdate]
        [MaxLength(1)]
        public Boolean? IsDelete { get; set; }

        /*[begin custom code bottom]*/
        //自定义代码区域,重新生成代码不会覆盖

        /*[end custom code bottom]*/
    }
}
