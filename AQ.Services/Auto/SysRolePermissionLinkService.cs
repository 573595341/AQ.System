using System;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
/*[begin custom code head]*/
//自定义命名空间区域
using AutoMapper;
using FluentValidation;
using AQ.Models;
using AQ.ViewModels;
using AQ.IRepository;
using AQ.IServices;
using AQ.ModelExtension;
using AQ.Services.Validation;
using System.Threading.Tasks;

/*[end custom code head]*/

namespace AQ.Services
{
    public class SysRolePermissionLinkService : ISysRolePermissionLinkService
    {

        /*[begin custom code body]*/
        #region 自定义代码区域,重新生成代码不会覆盖
        private readonly ISysRolePermissionLinkRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<SysRolePermissionLinkService> _logger;
        private readonly ISysPermissionRepository _sysPermissionRepository;
        public SysRolePermissionLinkService(ISysRolePermissionLinkRepository repository,
            IMapper mapper,
            ILogger<SysRolePermissionLinkService> logger,
            ISysPermissionRepository sysPermissionRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _sysPermissionRepository = sysPermissionRepository;
        }
        #endregion
        /*[end custom code body]*/


        /*[begin custom code bottom]*/
        #region 自定义代码区域,重新生成代码不会覆盖

        /// <summary>
        /// 更新菜单权限
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <param name="moduleData">模块数据</param>
        /// <param name="menuData">菜单数据</param>
        /// <returns></returns>
        public BaseResult UpdateMenu(string roleId, ModulePermissionViewModel moduleData, List<MenuPermissionViewModel> menuData)
        {
            var result = new BaseResult();
            if (string.IsNullOrEmpty(roleId) || moduleData == null || menuData == null)
            {
                result = CommonResults.ParameterError;
                return result;
            }

            try
            {
                var validationResult = new ModulePermissionValidation().Validate(moduleData, ruleSet: "Save");
                if (!validationResult.IsValid)
                {
                    result.ResultMsg = validationResult.ToString(";");
                    result.ResultCode = CommonResults.ParameterError.ResultCode;
                    return result;
                }

                var saveData = new List<SysRolePermissionLink>();
                #region 验证需要添加权限的模块
                var moduleList = _sysPermissionRepository.GetModuleData(roleId);
                var module = moduleList.FirstOrDefault(m => m.SId == moduleData.Id);
                if (module != null && module.Value > 0)
                {
                    saveData.Add(new SysRolePermissionLink()
                    {
                        PerId = module.Id,
                        RoleId = roleId,
                        Operation = moduleData.Value
                    });
                }
                #endregion

                #region 验证需要添加权限的菜单
                var menuList = _sysPermissionRepository.GetMenuData(moduleData.Id, roleId);
                var saveMenu = from item in menuData
                               join menu in menuList on new { Id = item.Id } equals new { Id = menu.SId }
                               where item.Value > 0
                               select new SysRolePermissionLink
                               {
                                   PerId = menu.Id,
                                   Operation = item.Value,
                                   RoleId = roleId
                               };
                if (saveMenu != null && saveMenu.Count() > 0)
                {
                    saveData.AddRange(saveMenu);
                }
                #endregion

                _repository.UpdateMenu(moduleData.Id, roleId, saveData);
                result = CommonResults.Success;
            }
            catch (Exception ex)
            {
                result = CommonResults.Exception;
                _logger.LogError(ex, "更新菜单权限异常");
            }
            return result;
        }

        /// <summary>
        /// 更新菜单权限
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <param name="moduleData">模块数据</param>
        /// <param name="menuData">菜单数据</param>
        /// <returns></returns>
        public async Task<BaseResult> UpdateMenuAsync(string roleId, ModulePermissionViewModel moduleData, List<MenuPermissionViewModel> menuData)
        {
            var result = new BaseResult();
            if (string.IsNullOrEmpty(roleId) || moduleData == null || menuData == null)
            {
                result = CommonResults.ParameterError;
                return result;
            }
            try
            {
                var validationResult = await new ModulePermissionValidation().ValidateAsync(moduleData, ruleSet: "Save");
                if (!validationResult.IsValid)
                {
                    result.ResultMsg = validationResult.ToString(";");
                    result.ResultCode = CommonResults.ParameterError.ResultCode;
                    return result;
                }

                var saveData = new List<SysRolePermissionLink>();
                #region 验证需要添加权限的模块
                var moduleList = await _sysPermissionRepository.GetModuleDataAsync(roleId);
                var module = moduleList.FirstOrDefault(m => m.SId == moduleData.Id);
                if (module != null)
                {
                    saveData.Add(new SysRolePermissionLink()
                    {
                        PerId = module.Id,
                        RoleId = roleId,
                        Operation = moduleData.Value
                    });
                }
                #endregion

                #region 验证需要添加权限的菜单
                var menuList = await _sysPermissionRepository.GetMenuDataAsync(moduleData.Id, roleId);
                var saveMenu = from item in menuData
                               join menu in menuList on new { Id = item.Id } equals new { Id = menu.SId }
                               where item.Value > 0
                               select new SysRolePermissionLink
                               {
                                   PerId = menu.Id,
                                   Operation = item.Value,
                                   RoleId = roleId
                               };
                if (saveMenu != null && saveMenu.Count() > 0)
                {
                    saveData.AddRange(saveMenu);
                }
                #endregion

                if (saveData.Count() > 0)
                {
                    await _repository.UpdateMenuAsync(moduleData.Id, roleId, saveData);
                }
                result = CommonResults.Success;
            }
            catch (Exception ex)
            {
                result = CommonResults.Exception;
                _logger.LogError(ex, "更新菜单权限异常");
            }
            return result;
        }

        #endregion
        /*[end custom code bottom]*/
    }
}