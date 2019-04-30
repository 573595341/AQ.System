using System;
using System.Collections.Generic;
using System.Text;

namespace AQ.ViewModels
{
    /// <summary>
    /// 共用结果信息
    /// </summary>
    public class CommonResults
    {
        /// <summary>操作成功</summary>
        public static readonly BaseResult Success = new BaseResult(0, "success");
        /// <summary>操作失败</summary>
        public static readonly BaseResult Fail = new BaseResult(1, "Fail");
        /// <summary>操作异常</summary>
        public static readonly BaseResult Exception = new BaseResult(-1, "system exception");
        /// <summary>无权限</summary>
        public static readonly BaseResult NoAuthority = new BaseResult(101, "暂无权限");
    }

}
