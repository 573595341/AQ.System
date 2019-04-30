using System;
/*[begin custom code head]*/
//自定义命名空间区域
/*[end custom code head]*/

namespace AQ.ViewModels
{
    /// <summary>
    /// AQsysMenu
    /// </summary>
    public class AQsysMenuViewModel
    {

        /*[begin custom code body]*/
        //自定义代码区域,重新生成代码不会覆盖
        /*[end custom code body]*/

        /// <summary>MenuId</summary>
        public String MenuId { get; set; }

        /// <summary>MenuName</summary>
        public String MenuName { get; set; }

        /// <summary>MenuParentId</summary>
        public String MenuParentId { get; set; }

        /// <summary>ModuleId</summary>
        public String ModuleId { get; set; }

        /// <summary>MenuType</summary>
        public String MenuType { get; set; }

        /// <summary>MenuLevel</summary>
        public Int32? MenuLevel { get; set; }

        /// <summary>MenuPageUrl</summary>
        public String MenuPageUrl { get; set; }

        /// <summary>MenuIco</summary>
        public String MenuIco { get; set; }

        /// <summary>Status</summary>
        public Int32? Status { get; set; }

        /// <summary>Sort</summary>
        public Int32? Sort { get; set; }

        /// <summary>CreateDate</summary>
        public DateTime? CreateDate { get; set; }

        /// <summary>LastModifyDate</summary>
        public DateTime? LastModifyDate { get; set; }

        /// <summary>CreateUserAccount</summary>
        public String CreateUserAccount { get; set; }

        /// <summary>ModifyUserAccount</summary>
        public String ModifyUserAccount { get; set; }

        /// <summary>IsDelete</summary>
        public Boolean? IsDelete { get; set; }



        /*[begin custom code bottom]*/
        //自定义代码区域,重新生成代码不会覆盖

        /// <summary>上级菜单名称</summary>
        public string ParentName { get; set; }

        /// <summary>所属模块</summary>
        public string ModuleName { get; set; }

        /*[end custom code bottom]*/
    }
}
