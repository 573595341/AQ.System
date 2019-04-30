using System;
using System.Collections.Generic;
using System.Text;

namespace AQ.ViewModels
{
    public class BaseResult
    {
        /// <summary>结果编码</summary>
        public int ResultCode { get; set; }
        /// <summary>结果消息</summary>
        public string ResultMsg { get; set; }

        /// <summary>
        /// 默认操作失败
        /// </summary>
        public BaseResult()
        {
            ResultCode = CommonResults.Fail.ResultCode;
            ResultMsg = CommonResults.Fail.ResultMsg;
        }

        /// <summary>
        /// 结果信息
        /// </summary>
        /// <param name="resultCode">结果编码</param>
        /// <param name="resultMsg">结果消息</param>
        public BaseResult(int resultCode, string resultMsg)
        {
            SetResult(resultCode, resultMsg);
        }

        /// <summary>
        /// 设置结果信息
        /// </summary>
        /// <param name="resultCode">结果编码</param>
        /// <param name="resultMsg">结果消息</param>
        /// <returns></returns>
        public BaseResult SetResult(int resultCode, string resultMsg)
        {
            ResultCode = resultCode;
            ResultMsg = resultMsg;
            return this;
        }

    }


    public class BaseResult<TModul> : BaseResult where TModul : class
    {
        public TModul Data { get; set; }
    }
}
