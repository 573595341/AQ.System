using System;
using System.Collections.Generic;
using System.Text;
using AQ.ModelExtension;
using AQ.ViewModels;

namespace AQ.IServices
{
    public interface IAuthorizeService
    {

        /// <summary>
        /// 获取授权模块菜单信息
        /// </summary>
        /// <returns></returns>
        BaseResult<List<AuthorizeModuleViewModel>> GetAuthModuleData();
    }
}
