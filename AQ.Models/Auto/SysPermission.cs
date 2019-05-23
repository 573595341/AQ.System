using System;
using System.ComponentModel.DataAnnotations;
/*[begin custom code head]*/
//自定义命名空间区域
using Dapper;
using KeyAttribute = Dapper.KeyAttribute;
using RequiredAttribute = Dapper.RequiredAttribute;

/*[end custom code head]*/

namespace AQ.Models
{
	/// <summary>
	/// SysPermission
	/// </summary>
	public class SysPermission
	{

		/*[begin custom code body]*/
		#region 自定义代码区域,重新生成代码不会覆盖
		#endregion
		/*[end custom code body]*/

		/// <summary>Id</summary>
		[Required]
		[Key]
		public Int64 Id { get; set; }

		/// <summary>PerType</summary>
		[Required]
		[MaxLength(30)]
		public String PerType { get; set; }

		/// <summary>ResourceId</summary>
		[Required]
		[MaxLength(30)]
		public String ResourceId { get; set; }

		/// <summary>Status</summary>
		[Required]
		[MaxLength(10)]
		public Int32 Status { get; set; }

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
		#endregion
		/*[end custom code bottom]*/
	}
}
