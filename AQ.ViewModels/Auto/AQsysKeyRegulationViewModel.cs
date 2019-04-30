using System;
/*[begin custom code head]*/
//自定义命名空间区域
/*[end custom code head]*/

namespace AQ.ViewModels
{
	/// <summary>
	/// AQsysKeyRegulation
	/// </summary>
	public class AQsysKeyRegulationViewModel
	{
	
		/*[begin custom code body]*/
		//自定义代码区域,重新生成代码不会覆盖
		/*[end custom code body]*/

		/// <summary>Id</summary>
		public Int32 Id { get; set; }

		/// <summary>KeyName</summary>
		public String KeyName { get; set; }

		/// <summary>TabName</summary>
		public String TabName { get; set; }

		/// <summary>Format</summary>
		public String Format { get; set; }

		/// <summary>FormatDate</summary>
		public String FormatDate { get; set; }

		/// <summary>Number</summary>
		public Int32? Number { get; set; }

		/// <summary>NumberLength</summary>
		public Int32? NumberLength { get; set; }

		/// <summary>Status</summary>
		public Int32? Status { get; set; }

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
