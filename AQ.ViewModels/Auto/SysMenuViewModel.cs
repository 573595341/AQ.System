using System;
/*[begin custom code head]*/
//自定义命名空间区域
/*[end custom code head]*/

namespace AQ.ViewModels
{
	/// <summary>
	/// SysMenu
	/// </summary>
	public class SysMenuViewModel
	{
	
		/*[begin custom code body]*/
		#region 自定义代码区域,重新生成代码不会覆盖
		#endregion
		/*[end custom code body]*/

		/// <summary>Id</summary>
		public String Id { get; set; }

		/// <summary>Name</summary>
		public String Name { get; set; }

		/// <summary>ParentId</summary>
		public String ParentId { get; set; }

		/// <summary>ModuleId</summary>
		public String ModuleId { get; set; }

		/// <summary>Type</summary>
		public String Type { get; set; }

		/// <summary>Level</summary>
		public Int32 Level { get; set; }

		/// <summary>PageUrl</summary>
		public String PageUrl { get; set; }

		/// <summary>Ico</summary>
		public String Ico { get; set; }

		/// <summary>Status</summary>
		public Int32 Status { get; set; }

		/// <summary>Sort</summary>
		public Int32 Sort { get; set; }

		/// <summary>CreateTime</summary>
		public DateTime CreateTime { get; set; }

		/// <summary>ModifyTime</summary>
		public DateTime ModifyTime { get; set; }

		/// <summary>CreateUser</summary>
		public String CreateUser { get; set; }

		/// <summary>ModifyUser</summary>
		public String ModifyUser { get; set; }

		/// <summary>IsDelete</summary>
		public Boolean IsDelete { get; set; }



		/*[begin custom code bottom]*/
        #region 自定义代码区域,重新生成代码不会覆盖
        /// <summary>上级菜单名称</summary>
        public string ParentName { get; set; }

        /// <summary>所属模块</summary>
        public string ModuleName { get; set; }
        #endregion
        /*[end custom code bottom]*/
	}
}
