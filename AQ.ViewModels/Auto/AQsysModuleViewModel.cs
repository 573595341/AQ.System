using System;
/*[begin custom code head]*/
//自定义命名空间区域
/*[end custom code head]*/

namespace AQ.ViewModels
{
	/// <summary>
	/// AQsysModule
	/// </summary>
	public class AQsysModuleViewModel
	{
	
		/*[begin custom code body]*/
		//自定义代码区域,重新生成代码不会覆盖
		/*[end custom code body]*/

		/// <summary>ModuleId</summary>
		public String ModuleId { get; set; }

		/// <summary>ModuleName</summary>
		public String ModuleName { get; set; }

		/// <summary>ModuleIco</summary>
		public String ModuleIco { get; set; }

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
		/*[end custom code bottom]*/
	}
}
