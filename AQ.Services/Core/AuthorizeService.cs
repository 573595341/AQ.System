using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.Logging;
using AQ.IRepository;
using AQ.ModelExtension;
using AQ.ViewModels;
using AQ.Models;
using AQ.IServices;

namespace AQ.Services
{
    public class AuthorizeService : IAuthorizeService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<SysUserService> _logger;
        private readonly ISysMenuRepository _menuRepository;
        private readonly ISysModuleRepository _moduleRepository;

        public AuthorizeService(ILogger<SysUserService> logger, IMapper mapper, ISysMenuRepository menuRepository, ISysModuleRepository moduleRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _menuRepository = menuRepository;
            _moduleRepository = moduleRepository;
        }

        /// <summary>
        /// 获取授权模块菜单信息
        /// </summary>
        /// <returns></returns>
        public BaseResult<List<AuthorizeModuleViewModel>> GetAuthModuleData()
        {
            var result = new BaseResult<List<AuthorizeModuleViewModel>>();
            try
            {
                var moduleData = _moduleRepository.GetList().ToList();
                if (moduleData == null || moduleData.Count() == 0)
                {
                    result.ResultMsg = $"获取模块信息为空";
                    return result;
                }
                var menuData = _menuRepository.GetList();
                if (menuData == null || menuData.Count() == 0)
                {
                    result.ResultMsg = $"获取菜单信息为空";
                    return result;
                }

                //权限菜单数据
                var authMenuData = (from module in moduleData
                                    join menu in menuData on new { Id = module.Id } equals new { Id = menu.ModuleId }
                                    select new AuthorizeMenuViewModel
                                    {
                                        id = menu.Id,
                                        title = menu.Name,
                                        href = menu.PageUrl,
                                        icon = menu.Ico ?? "",
                                        parentId = menu.ParentId,
                                        moduleId = module.Id,
                                        sort = menu.Sort,
                                        moduleSort = module.Sort

                                    }).ToList();

                authMenuData = authMenuData.OrderBy(m => m.sort).ToList();
                result.Data = GetAuthorizeModules(authMenuData, moduleData);
                result.Data = result.Data.OrderBy(m => m.sort).ToList();
                result.ResultCode = CommonResults.Success.ResultCode;
                result.ResultMsg = CommonResults.Success.ResultMsg;
                return result;
            }
            catch (Exception ex)
            {
                result.ResultCode = CommonResults.Exception.ResultCode;
                result.ResultMsg = CommonResults.Exception.ResultMsg;
                _logger.LogError(ex, $"获取授权木块菜单信息异常");
            }
            return result;

            #region MyRegion
            //var data = from module in moduleData
            //           join menu in menuData on new { menu.id, menu.AppCode } equals new { module.SubSys, module.AppCode }
            //           join parentMenu in appMenuList on new { ParentId = menu.ParentMenu, AppCode = menu.AppCode } equals new { ParentId = parentMenu.MenuId, AppCode = parentMenu.AppCode } into t
            //           from temp in t.DefaultIfEmpty()
            //           where user.AppCode == appCode && user.UserId == userId && user.CustomerId == customerId
            //select new AppMenuModelView()
            //{
            //    Ico = menu.ShowIco,
            //    IsNode = menu.IsNode,
            //    Level = menu.MenuLevel,
            //    MenuId = menu.MenuId,
            //    MenuName = menu.MenuName,
            //    ModuleId = module.SubSys,
            //    Sort = menu.ShowOrder,
            //    ParentMenuId = menu.ParentMenu,
            //    ParentMenuName = temp != null ? temp.MenuName : ""
            //}; 
            #endregion

        }

        /// <summary>
        /// 结构化菜单信息
        /// </summary>
        /// <param name="authMenuData">权限菜单数据</param>
        /// <param name="moduleData"></param>
        /// <returns></returns>
        private List<AuthorizeModuleViewModel> GetAuthorizeModules(List<AuthorizeMenuViewModel> authMenuData, List<SysModule> moduleData)
        {
            var result = new List<AuthorizeModuleViewModel>();
            AuthorizeMenuViewModel prevMenu = null;
            foreach (var menu in authMenuData)
            {
                if (menu.parentId == "0")
                {
                    //添加模块
                    var module = result.FirstOrDefault(m => m.id == menu.moduleId);
                    if (module == null)
                    {
                        var itemModule = moduleData.FirstOrDefault(m => m.Id == menu.moduleId);
                        module = new AuthorizeModuleViewModel()
                        {
                            id = itemModule.Id,
                            name = itemModule.Name,
                            sort = itemModule.Sort,
                            icon = itemModule.Ico
                        };
                        result.Add(module);
                    }
                    //添加一级菜单
                    var item = module.menuData.FirstOrDefault(m => m.id == menu.id);
                    if (item == null)
                    {
                        module.menuData.Add(menu);
                    }
                }
                else
                {
                    //添加子菜单
                    if (prevMenu == null || prevMenu.id != menu.parentId)
                    {
                        prevMenu = authMenuData.FirstOrDefault(m => m.id == menu.parentId);
                    }
                    if (prevMenu == null)
                    {
                        continue;
                    }
                    var item = prevMenu.children.FirstOrDefault(m => m.id == menu.id);
                    if (item == null)
                    {
                        prevMenu.children.Add(menu);
                    }
                }
            }
            return result;
        }
    }
}
