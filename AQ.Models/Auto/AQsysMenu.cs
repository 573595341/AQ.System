using Dapper;
using System;
using System.ComponentModel.DataAnnotations;
using KeyAttribute = Dapper.KeyAttribute;
using RequiredAttribute = Dapper.RequiredAttribute;
/*[begin custom code head]*/
//自定义命名空间区域
/*[end custom code head]*/

namespace AQ.Models
{
    /// <summary>
    /// AQsysMenu
    /// </summary>
    public class AQsysMenu
    {

        /*[begin custom code body]*/
        //自定义代码区域,重新生成代码不会覆盖
        /*[end custom code body]*/

        /// <summary>MenuId</summary>
        [Key]
        public String MenuId { get; set; }

        /// <summary>MenuName</summary>
        [Required]
        [MaxLength(30)]
        public String MenuName { get; set; }

        /// <summary>MenuParentId</summary>
        [Required]
        [MaxLength(30)]
        public String MenuParentId { get; set; }

        /// <summary>ModuleId</summary>
        [Required]
        [MaxLength(30)]
        public String ModuleId { get; set; }

        /// <summary>MenuType</summary>
        [MaxLength(30)]
        public String MenuType { get; set; }

        /// <summary>MenuLevel</summary>
        [MaxLength(10)]
        public Int32? MenuLevel { get; set; }

        /// <summary>MenuPageUrl</summary>
        [MaxLength(255)]
        public String MenuPageUrl { get; set; }

        /// <summary>MenuIco</summary>
        [MaxLength(255)]
        public String MenuIco { get; set; }

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

        [Dapper.NotMapped]
        /// <summary>上级菜单名称</summary>
        public string ParentName { get; set; }

        [Dapper.NotMapped]
        /// <summary>所属模块</summary>
        public string ModuleName { get; set; }

        /*[end custom code bottom]*/
    }
}
