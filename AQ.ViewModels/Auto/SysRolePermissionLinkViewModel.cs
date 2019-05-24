using System;
/*[begin custom code head]*/
//自定义命名空间区域

/*[end custom code head]*/

namespace AQ.ViewModels
{
	/// <summary>
	/// SysRolePermissionLink
	/// </summary>
	public class SysRolePermissionLinkViewModel
	{
	
		/*[begin custom code body]*/
		#region 自定义代码区域,重新生成代码不会覆盖
		#endregion
		/*[end custom code body]*/

		/// <summary>Id</summary>
		public Int64 Id { get; set; }

		/// <summary>RoleId</summary>
		public String RoleId { get; set; }

		/// <summary>PerId</summary>
		public Int64 PerId { get; set; }

		/// <summary>1:查看 2:添加 4:编辑 8:删除 16:下载</summary>
		public Int32 Operation { get; set; }

		/// <summary>Status</summary>
		public Int32 Status { get; set; }

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
		#endregion
		/*[end custom code bottom]*/
	}
}
