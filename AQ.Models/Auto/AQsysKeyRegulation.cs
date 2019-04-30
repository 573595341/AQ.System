using System;
using System.ComponentModel.DataAnnotations;
/*[begin custom code head]*/
//自定义命名空间区域
/*[end custom code head]*/

namespace AQ.Models
{
	/// <summary>
	/// AQsysKeyRegulation
	/// </summary>
	public class AQsysKeyRegulation
	{

		/*[begin custom code body]*/
        //自定义代码区域,重新生成代码不会覆盖
        /*[end custom code body]*/

		/// <summary>Id</summary>
		[Key]
		public Int32 Id { get; set; }

		/// <summary>KeyName</summary>
		[Required]
		[MaxLength(50)]
		public String KeyName { get; set; }

		/// <summary>TabName</summary>
		[Required]
		[MaxLength(50)]
		public String TabName { get; set; }

		/// <summary>Format</summary>
		[MaxLength(30)]
		public String Format { get; set; }

		/// <summary>FormatDate</summary>
		[MaxLength(30)]
		public String FormatDate { get; set; }

		/// <summary>Number</summary>
		[MaxLength(10)]
		public Int32? Number { get; set; }

		/// <summary>NumberLength</summary>
		[MaxLength(10)]
		public Int32? NumberLength { get; set; }

		/// <summary>Status</summary>
		[MaxLength(10)]
		public Int32? Status { get; set; }

		/// <summary>CreateDate</summary>
		[MaxLength(23)]
		public DateTime? CreateDate { get; set; }

		/// <summary>LastModifyDate</summary>
		[MaxLength(23)]
		public DateTime? LastModifyDate { get; set; }

		/// <summary>CreateUserAccount</summary>
		[MaxLength(30)]
		public String CreateUserAccount { get; set; }

		/// <summary>ModifyUserAccount</summary>
		[MaxLength(30)]
		public String ModifyUserAccount { get; set; }

		/// <summary>IsDelete</summary>
		[MaxLength(1)]
		public Boolean? IsDelete { get; set; }



		/*[begin custom code bottom]*/
        //自定义代码区域,重新生成代码不会覆盖
        public DateTime? NowDate { get; set; }
        /*[end custom code bottom]*/
	}
}
