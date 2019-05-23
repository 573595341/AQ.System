using System;
using System.Collections.Generic;
using System.Text;

namespace AQ.ViewModels
{
    /// <summary>
    /// 授权模块
    /// </summary>
    public class AuthorizeModuleViewModel
    {
        /// <summary>模块id</summary>
        public string id { get; set; }
        /// <summary>模块名称</summary>
        public string name { get; set; }
        /// <summary>图标</summary>
        public string icon { get; set; }
        /// <summary>模块排序</summary>
        public int sort { get; set; }

        public List<AuthorizeMenuViewModel> menuData { get; set; }

        public AuthorizeModuleViewModel()
        {
            id = name = "";
            icon = "&#xe857;";
            menuData = new List<AuthorizeMenuViewModel>();
        }
    }

    /// <summary>
    /// 授权菜单
    /// </summary>
    public class AuthorizeMenuViewModel
    {
        /// <summary>菜单id</summary>
        public string id { get; set; }
        /// <summary>菜单名称</summary>
        public string title { get; set; }
        /// <summary>图标</summary>
        public string icon { get; set; }
        /// <summary>跳转链接</summary>
        public string href { get; set; }
        /// <summary>是否展开</summary>
        public bool spread { get; set; }
        /// <summary>上级菜单id</summary>
        public string parentId { get; set; }
        /// <summary>所属模块</summary>
        public string moduleId { get; set; }
        /// <summary>菜单排序</summary>
        public int sort { get; set; }
        /// <summary>模块排序</summary>
        public int moduleSort { get; set; }
        /// <summary>子菜单</summary>
        public List<AuthorizeMenuViewModel> children { get; set; }

        public AuthorizeMenuViewModel()
        {
            href = title = "";
            icon = "&#xe857;";
            children = new List<AuthorizeMenuViewModel>();
        }
    }
}
