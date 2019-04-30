using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AQ.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace AQ.WebMain.Filter
{
    public class AuthorizationFilter : Attribute, IAuthorizationFilter
    {
        private ILogger<AuthorizationFilter> _logger;
        public AuthorizationFilter(ILogger<AuthorizationFilter> logger)
        {
            _logger = logger;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //跳过非权限验证action
            if (context.ActionDescriptor.EndpointMetadata.FirstOrDefault(attr => attr.ToString().Equals(typeof(NonAuthorizationAttribute))) == null)
            {
                _logger.LogDebug($"OnAuthorization 权限验证 { context.ActionDescriptor.DisplayName}");
                //context.Result = new ObjectResult(CommonResults.NoAuthority);
            }
        }
    }

    /// <summary>
    /// 非权限验证
    /// </summary>
    public class NonAuthorizationAttribute : Attribute { }
}
