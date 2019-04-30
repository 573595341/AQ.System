using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
/*[begin custom code head]*/
//自定义命名空间区域
using Dapper;
using KeyAttribute = Dapper.KeyAttribute;
using RequiredAttribute = Dapper.RequiredAttribute;

/*[end custom code head]*/


namespace AQ.Models
{
	/// <summary>
	/// SysModule
	/// </summary>
	public class SysModule
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
		#endregion
		/*[end custom code bottom]*/
	}
}
