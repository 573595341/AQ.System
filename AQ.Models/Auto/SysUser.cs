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
	/// SysUser
	/// </summary>
	public class SysUser
	{

		/*[begin custom code body]*/
		#region 自定义代码区域,重新生成代码不会覆盖
		#endregion
		/*[end custom code body]*/

		/// <summary>Id</summary>
		[Required]
		[Key]
		public String Id { get; set; }

		/// <summary>Account</summary>
		[Required]
		[MaxLength(30)]
		public String Account { get; set; }

		/// <summary>Pwd</summary>
		[Required]
		[MaxLength(100)]
		public String Pwd { get; set; }

		/// <summary>NickName</summary>
		[Required]
		[MaxLength(30)]
		public String NickName { get; set; }

		/// <summary>Mobile</summary>
		[Required]
		[MaxLength(20)]
		public String Mobile { get; set; }

		/// <summary>JobCode</summary>
		[Required]
		[MaxLength(30)]
		public String JobCode { get; set; }

		/// <summary>CName</summary>
		[Required]
		[MaxLength(30)]
		public String CName { get; set; }

		/// <summary>EName</summary>
		[Required]
		[MaxLength(30)]
		public String EName { get; set; }

		/// <summary>Alias</summary>
		[Required]
		[MaxLength(30)]
		public String Alias { get; set; }

		/// <summary>Photo</summary>
		[Required]
		[MaxLength(255)]
		public String Photo { get; set; }

		/// <summary>Sex</summary>
		[Required]
		[MaxLength(10)]
		public Int32 Sex { get; set; }

		/// <summary>Birthday</summary>
		[MaxLength(10)]
		public DateTime? Birthday { get; set; }

		/// <summary>IdCard</summary>
		[Required]
		[MaxLength(20)]
		public String IdCard { get; set; }

		/// <summary>BankCard</summary>
		[Required]
		[MaxLength(20)]
		public String BankCard { get; set; }

		/// <summary>PresentAddrress</summary>
		[Required]
		[MaxLength(255)]
		public String PresentAddrress { get; set; }

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
