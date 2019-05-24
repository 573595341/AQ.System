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
    public class SysPermissionService : ISysPermissionService
    {

        /*[begin custom code body]*/
        #region 自定义代码区域,重新生成代码不会覆盖
        private readonly ISysPermissionRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<SysPermissionService> _logger;
        public SysPermissionService(ISysPermissionRepository repository, IMapper mapper, ILogger<SysPermissionService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }
        #endregion
        /*[end custom code body]*/


        /*[begin custom code bottom]*/
        #region 自定义代码区域,重新生成代码不会覆盖

        /// <summary>
        /// 获取菜单权限信息
        /// </summary>
        /// <param name="moduleId">模块id</param>
        /// <param name="roleId">角色id</param>
        /// <returns></returns>
        public BaseResult<List<MenuPermissionViewModel>> GetMenuData(string moduleId, string roleId)
        {
            var result = new BaseResult<List<MenuPermissionViewModel>>();
            if (string.IsNullOrEmpty(moduleId) || string.IsNullOrEmpty(roleId))
            {
                result.ResultMsg = CommonResults.ParameterError.ResultMsg;
                result.ResultCode = CommonResults.ParameterError.ResultCode;
                return result;
            }
            try
            {
                result.Data = _mapper.Map<List<MenuPermissionViewModel>>(_repository.GetMenuData(moduleId, roleId));
                result.ResultCode = CommonResults.Success.ResultCode;
                result.ResultMsg = CommonResults.Success.ResultMsg;
            }
            catch (Exception ex)
            {
                result.ResultCode = CommonResults.Exception.ResultCode;
                result.ResultMsg = CommonResults.Exception.ResultMsg;
                _logger.LogError(ex, "获取菜单权限信息异常");
            }
            return result;
        }

        /// <summary>
        /// 获取菜单权限信息
        /// </summary>
        /// <param name="moduleId">模块id</param>
        /// <param name="roleId">角色id</param>
        /// <returns></returns>
        public async Task<BaseResult<List<MenuPermissionViewModel>>> GetMenuDataAsync(string moduleId, string roleId)
        {
            var result = new BaseResult<List<MenuPermissionViewModel>>();
            if (string.IsNullOrEmpty(moduleId) || string.IsNullOrEmpty(roleId))
            {
                result.ResultMsg = CommonResults.ParameterError.ResultMsg;
                result.ResultCode = CommonResults.ParameterError.ResultCode;
                return result;
            }
            try
            {
                result.Data = _mapper.Map<List<MenuPermissionViewModel>>(await _repository.GetMenuDataAsync(moduleId, roleId));
                result.ResultCode = CommonResults.Success.ResultCode;
                result.ResultMsg = CommonResults.Success.ResultMsg;
            }
            catch (Exception ex)
            {
                result.ResultCode = CommonResults.Exception.ResultCode;
                result.ResultMsg = CommonResults.Exception.ResultMsg;
                _logger.LogError(ex, "获取菜单权限信息异常");
            }
            return result;
        }

        /// <summary>
        /// 获取模块权限信息
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <returns></returns>
        public BaseResult<List<ModulePermissionViewModel>> GetModuleData(string roleId)
        {
            var result = new BaseResult<List<ModulePermissionViewModel>>();
            if (string.IsNullOrEmpty(roleId))
            {
                result.ResultMsg = CommonResults.ParameterError.ResultMsg;
                result.ResultCode = CommonResults.ParameterError.ResultCode;
                return result;
            }
            try
            {
                result.Data = _mapper.Map<List<ModulePermissionViewModel>>(_repository.GetModuleData(roleId));
                result.ResultCode = CommonResults.Success.ResultCode;
                result.ResultMsg = CommonResults.Success.ResultMsg;
            }
            catch (Exception ex)
            {
                result.ResultCode = CommonResults.Exception.ResultCode;
                result.ResultMsg = CommonResults.Exception.ResultMsg;
                _logger.LogError(ex, "获取模块权限信息异常");
            }
            return result;
        }

        /// <summary>
        /// 获取模块权限信息
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <returns></returns>
        public async Task<BaseResult<List<ModulePermissionViewModel>>> GetModuleDataAsync(string roleId)
        {
            var result = new BaseResult<List<ModulePermissionViewModel>>();
            if (string.IsNullOrEmpty(roleId))
            {
                result.ResultMsg = CommonResults.ParameterError.ResultMsg;
                result.ResultCode = CommonResults.ParameterError.ResultCode;
                return result;
            }
            try
            {
                result.Data = _mapper.Map<List<ModulePermissionViewModel>>(await _repository.GetModuleDataAsync(roleId));
                result.ResultCode = CommonResults.Success.ResultCode;
                result.ResultMsg = CommonResults.Success.ResultMsg;
            }
            catch (Exception ex)
            {
                result.ResultCode = CommonResults.Exception.ResultCode;
                result.ResultMsg = CommonResults.Exception.ResultMsg;
                _logger.LogError(ex, "获取模块权限信息异常");
            }
            return result;
        }

        #endregion
        /*[end custom code bottom]*/
    }
}