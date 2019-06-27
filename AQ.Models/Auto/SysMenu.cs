using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
/*[begin custom code head]*/
//自定义命名空间区域
using Dapper;
using IgnoreUpdateAttribute = System.ComponentModel.DataAnnotations.IgnoreUpdateAttribute;
using KeyAttribute = Dapper.KeyAttribute;
using RequiredAttribute = Dapper.RequiredAttribute;

/*[end custom code head]*/

namespace AQ.Models
{
    /// <summary>
    /// SysMenu
    /// </summary>
    public class SysMenu
    {

        /*[begin custom code body]*/
        #region 自定义代码区域,重新生成代码不会覆盖
        #endregion
        /*[end custom code body]*/

        /// <summary>Id</summary>
        [Required]
        [Key]
        public String Id { get; set; }

        /// <summary>Name</summary>
        [Required]
        [MaxLength(30)]
        public String Name { get; set; }

        /// <summary>ParentId</summary>
        [Required]
        [MaxLength(30)]
        public String ParentId { get; set; }

        /// <summary>ModuleId</summary>
        [Required]
        [MaxLength(30)]
        public String ModuleId { get; set; }

        /// <summary>Type</summary>
        [Required]
        [MaxLength(30)]
        public String Type { get; set; }

        /// <summary>Level</summary>
        [Required]
        [MaxLength(10)]
        public Int32 Level { get; set; }

        /// <summary>PageUrl</summary>
        [Required]
        [MaxLength(255)]
        public String PageUrl { get; set; }

        /// <summary>Ico</summary>
        [Required]
        [MaxLength(255)]
        public String Ico { get; set; }

        /// <summary>Status</summary>
        [Required]
        [MaxLength(10)]
        public Int32 Status { get; set; }

        /// <summary>Sort</summary>
        [Required]
        [MaxLength(10)]
        public Int32 Sort { get; set; }

        /// <summary>CreateTime</summary>
        [Required]
        [MaxLength(23)]
        [IgnoreInsert]
        [IgnoreUpdate]
        public DateTime CreateTime { get; set; }

        /// <summary>ModifyTime</summary>
        [Required]
        [MaxLength(23)]
        [IgnoreInsert]
        public DateTime ModifyTime { get; set; }

        /// <summary>CreateUser</summary>
        [Required]
        [MaxLength(30)]
        [IgnoreUpdate]
        public String CreateUser { get; set; }

        /// <summary>ModifyUser</summary>
        [Required]
        [MaxLength(30)]
        public String ModifyUser { get; set; }

        /// <summary>IsDelete</summary>
        [Required]
        [MaxLength(1)]
        [IgnoreInsert]
        [IgnoreUpdate]
        public Boolean IsDelete { get; set; }



        /*[begin custom code bottom]*/
        #region 自定义代码区域,重新生成代码不会覆盖
        //[Dapper.NotMapped]
        ///// <summary>上级菜单名称</summary>
        //public string ParentName { get; set; }

        //[Dapper.NotMapped]
        ///// <summary>所属模块</summary>
        //public string ModuleName { get; set; }

        /// <summary>上级菜单</summary>
        public SysMenu ParentMenu { get; set; }
        /// <summary>子菜单</summary>
        public List<SysMenu> ChildrenMenu { get; set; }
        /// <summary>所属模块</summary>
        public SysModule Module { get; set; }

        #endregion
        /*[end custom code bottom]*/
    }
}
