using System;
using System.Collections.Generic;
using System.Text;

namespace AQ.ModelExtension
{
    /// <summary>
    /// 共用结果信息
    /// </summary>
    public class CommonResults
    {
        #region 系统
        /// <summary>操作成功</summary>
        public static readonly BaseResult Success = new BaseResult(0, "success");
        /// <summary>操作失败</summary>
        public static readonly BaseResult Fail = new BaseResult(1, "操作失败");
        /// <summary>系统异常</summary>
        public static readonly BaseResult Exception = new BaseResult(-1, "系统异常");
        #endregion

        #region 权限
        /// <summary>暂无权限</summary>
        public static readonly BaseResult NoAuthority = new BaseResult(1001, "暂无权限");
        #endregion

        #region 参数
        /// <summary>参数错误</summary>
        public static readonly BaseResult ParameterError = new BaseResult(2001, "参数错误");
        #endregion
    }

}
