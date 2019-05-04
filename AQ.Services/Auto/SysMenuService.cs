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

/*[end custom code head]*/

namespace AQ.Services
{
    public class SysMenuService : ISysMenuService
    {

		/*[begin custom code body]*/
        #region 自定义代码区域,重新生成代码不会覆盖
        private readonly ISysMenuRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<SysMenuService> _logger;
        private readonly ISysKeyRegulationRepository _keyRepository;

        public SysMenuService(ISysMenuRepository repo, IMapper map, ILogger<SysMenuService> log, ISysKeyRegulationRepository keyReg)
        {
            _repository = repo;
            _mapper = map;
            _logger = log;
            _keyRepository = keyReg;
        }
        #endregion
        /*[end custom code body]*/
        

		/*[begin custom code bottom]*/
        #region 自定义代码区域,重新生成代码不会覆盖

        /// <summary>
        /// 获取所有系统菜单数据信息
        /// </summary>
        /// <returns></returns>
        public ListPagedResult<SysMenuViewModel> GetListAll()
        {
            var result = new ListPagedResult<SysMenuViewModel>();
            try
            {
                var data = _repository.GetList().Where(d => !d.IsDelete && d.Status == 1).ToList();
                result.Data = _mapper.Map<List<SysMenuViewModel>>(data);
                result.ResultCode = CommonResults.Success.ResultCode;
                result.ResultMsg = CommonResults.Success.ResultMsg;
            }
            catch (Exception ex)
            {
                result.ResultCode = CommonResults.Exception.ResultCode;
                result.ResultMsg = CommonResults.Exception.ResultMsg;
                _logger.LogError(ex, $"获取所有菜单信息错误, ERROR:{ex.Message}");
            }
            return result;
        }

        /// <summary>
        /// 获取所有系统菜单数据信息
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public ListPagedResult<SysMenuViewModel> GetListPaged(SysMenuCondition condition)
        {
            var result = new ListPagedResult<SysMenuViewModel>();
            if (condition == null)
            {
                result.ResultMsg = CommonResults.ParameterError.ResultMsg;
                result.ResultCode = CommonResults.ParameterError.ResultCode;
                return result;
            }
            try
            {
                var data = _repository.GetListPaged(condition);
                result.Data = _mapper.Map<List<SysMenuViewModel>>(data.Data);
                result.PageIndex = condition.PageIndex;
                result.PageSize = condition.PageSize;
                result.PageCount = data.PageCount;
                result.TotalData = data.TotalData;
                result.ResultCode = CommonResults.Success.ResultCode;
                result.ResultMsg = CommonResults.Success.ResultMsg;
            }
            catch (Exception ex)
            {
                result.ResultCode = CommonResults.Exception.ResultCode;
                result.ResultMsg = CommonResults.Exception.ResultMsg;
                _logger.LogError(ex, "获取菜单列表信息异常");
            }
            return result;
        }

        /// <summary>
        /// 获取系统菜单详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BaseResult<SysMenuViewModel> GetDetail(string id)
        {
            var result = new BaseResult<SysMenuViewModel>();
            if (string.IsNullOrEmpty(id))
            {
                result.ResultMsg = CommonResults.ParameterError.ResultMsg;
                result.ResultCode = CommonResults.ParameterError.ResultCode;
                return result;
            }
            try
            {
                var data = _repository.Get(id);
                result.Data = _mapper.Map<SysMenuViewModel>(data);
                result.ResultCode = CommonResults.Success.ResultCode;
                result.ResultMsg = CommonResults.Success.ResultMsg;
            }
            catch (Exception ex)
            {
                result.ResultCode = CommonResults.Exception.ResultCode;
                result.ResultMsg = CommonResults.Exception.ResultMsg;
                _logger.LogError(ex, "获取菜单信息异常");
            }
            return result;
        }

        /// <summary>
        /// 添加系统菜单信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BaseResult Add(SysMenuViewModel model)
        {
            var result = new BaseResult();
            try
            {
                var validationResult = new MenuValidation().Validate(model, ruleSet: "Add");
                if (!validationResult.IsValid)
                {
                    result.ResultMsg = validationResult.ToString(";");
                    result.ResultCode = CommonResults.ParameterError.ResultCode;
                    return result;
                }
                var data = _mapper.Map<SysMenu>(model);
                data.Id = _keyRepository.GenerateKey("Id", "SysMenu");
                data.CreateUser = string.Empty;
                data.ModifyUser = string.Empty;
                data.PageUrl = data.PageUrl ?? string.Empty;
                result = _repository.Insert(data) != null ? CommonResults.Success : CommonResults.Fail;
            }
            catch (Exception ex)
            {
                result = CommonResults.Exception;
                _logger.LogError(ex, "添加菜单信息异常");
            }
            return result;
        }

        /// <summary>
        /// 更新系统菜单信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BaseResult Update(SysMenuViewModel model)
        {
            var result = new BaseResult();
            try
            {
                var validationResult = new MenuValidation().Validate(model, ruleSet: "Update");
                if (validationResult.IsValid)
                {
                    var data = _mapper.Map<SysMenu>(model);
                    data.ModifyTime = DateTime.Now;
                    if (_repository.Update(data) > 0)
                    {
                        result = CommonResults.Success;
                    }
                }
                else
                {
                    result.ResultMsg = validationResult.ToString(";");
                }
            }
            catch (Exception ex)
            {
                result.ResultCode = CommonResults.Exception.ResultCode;
                result.ResultMsg = CommonResults.Exception.ResultMsg;
                _logger.LogError(ex, "更新菜单信息异常");
            }
            return result;
        }

        /// <summary>
        /// 更改状态
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public BaseResult ChangeStatus(string[] keys, int status)
        {
            var result = new BaseResult();
            if (keys == null || keys.Length == 0)
            {
                result = CommonResults.ParameterError;
                return result;
            }
            try
            {
                result = _repository.UpdateStatus(status, keys) > 0 ? CommonResults.Success : CommonResults.Fail;
            }
            catch (Exception ex)
            {
                result = CommonResults.Exception;
                _logger.LogError(ex, "更改菜单状态信息异常");
            }
            return result;
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public BaseResult DeleteLogical(string[] keys)
        {
            var result = new BaseResult();
            try
            {
                if (keys == null || keys.Length == 0)
                {
                    result = CommonResults.ParameterError;
                    return result;
                }
                result = _repository.DeleteLogical(keys) > 0 ? CommonResults.Success : CommonResults.Fail;
            }
            catch (Exception ex)
            {
                result = CommonResults.Exception;
                _logger.LogError(ex, "逻辑删除菜单信息异常");
            }
            return result;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BaseResult Delete(string id)
        {
            var result = new BaseResult();
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    result = CommonResults.ParameterError;
                    return result;
                }
                result = _repository.Delete(id) > 0 ? CommonResults.Success : CommonResults.Fail;
            }
            catch (Exception ex)
            {
                result = CommonResults.Exception;
                _logger.LogError(ex, "删除菜单信息异常");
            }
            return result;
        }

        #endregion
        /*[end custom code bottom]*/
    }
}