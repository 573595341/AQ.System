using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Mvc
{
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// 获取版本号
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <returns></returns>
        public static string GetVersion(this IHtmlHelper htmlHelper)
        {
            return "0.0.02";
        }
    }


}
