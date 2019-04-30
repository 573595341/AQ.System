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
    public class SysModuleService : ISysModuleService
    {

        /*[begin custom code body]*/
        #region 自定义代码区域,重新生成代码不会覆盖
        private readonly ISysModuleRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<SysModuleService> _logger;
        private readonly ISysKeyRegulationRepository _keyRepository;

        public SysModuleService(ISysModuleRepository repo, IMapper map, ILogger<SysModuleService> log, ISysKeyRegulationRepository keyReg)
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
        /// 获取所有系统模块数据信息
        /// </summary>
        /// <returns></returns>
        public ListPagedResult<SysModuleViewModel> GetListAll()
        {
            var result = new ListPagedResult<SysModuleViewModel>();
            var data = new List<SysModule>();
            try
            {
                data = _repository.GetList().ToList();
                result.Data = _mapper.Map<List<SysModuleViewModel>>(data.Where(d => !d.IsDelete && d.Status == 1));
                result.ResultCode = CommonResults.Success.ResultCode;
                result.ResultMsg = CommonResults.Success.ResultMsg;
            }
            catch (Exception ex)
            {
                result.ResultCode = CommonResults.Exception.ResultCode;
                result.ResultMsg = CommonResults.Exception.ResultMsg;
                _logger.LogError(ex, "获取模块列表信息异常");
            }
            return result;
        }

        /// <summary>
        /// 获取所有系统模块数据信息
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public ListPagedResult<SysModuleViewModel> GetListPaged(SysModuleCondition condition)
        {
            var result = new ListPagedResult<SysModuleViewModel>();
            try
            {
                var data = _repository.GetListPaged(condition);
                data.Data.ForEach(d =>
                {
                    result.Data.Add(_mapper.Map<SysModuleViewModel>(d));
                });
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
                _logger.LogError(ex, "获取所有系统模块数据信息异常");
            }
            return result;
        }

        /// <summary>
        /// 获取系统模块详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BaseResult<SysModuleViewModel> GetDetail(string id)
        {
            var result = new BaseResult<SysModuleViewModel>();
            if (string.IsNullOrEmpty(id))
            {
                result.ResultMsg = CommonResults.ParameterError.ResultMsg;
                result.ResultCode = CommonResults.ParameterError.ResultCode;
                return result;
            }
            try
            {
                var data = _repository.Get(id);
                result.Data = _mapper.Map<SysModuleViewModel>(data);
                result.ResultCode = CommonResults.Success.ResultCode;
                result.ResultMsg = CommonResults.Success.ResultMsg;
            }
            catch (Exception ex)
            {
                result.ResultCode = CommonResults.Exception.ResultCode;
                result.ResultMsg = CommonResults.Exception.ResultMsg;
                _logger.LogError(ex, "获取系统模块详情异常");
            }
            return result;
        }

        /// <summary>
        /// 添加系统模块信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BaseResult Add(SysModuleViewModel model)
        {
            var result = new BaseResult();
            try
            {
                var validationResult = new ModuleValidation().Validate(model, ruleSet: "Add");
                if (!validationResult.IsValid)
                {
                    result.ResultMsg = validationResult.ToString(";");
                    result.ResultCode = CommonResults.ParameterError.ResultCode;
                }
                var data = _mapper.Map<SysModule>(model);
                data.Id = _keyRepository.GenerateKey("Id", "SysModule");
                data.CreateUser = string.Empty;
                data.ModifyUser = string.Empty;
                result = _repository.Insert(data) != null ? result = CommonResults.Success : CommonResults.Fail;
            }
            catch (Exception ex)
            {
                result = CommonResults.Exception;
                _logger.LogError(ex, "添加系统模块信息异常");
            }
            return result;
        }

        /// <summary>
        /// 更新系统模块信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BaseResult Update(SysModuleViewModel model)
        {
            var result = new BaseResult();
            try
            {
                var validationResult = new ModuleValidation().Validate(model, ruleSet: "Update");
                if (validationResult.IsValid)
                {
                    result.ResultMsg = validationResult.ToString(";");
                    result.ResultCode = CommonResults.ParameterError.ResultCode;
                    return result;
                }
                var data = _mapper.Map<SysModule>(model);
                data.ModifyTime = DateTime.Now;
                result = _repository.Update(data) > 0 ? CommonResults.Success : CommonResults.Fail;
            }
            catch (Exception ex)
            {
                result = CommonResults.Exception;
                _logger.LogError(ex, "更新系统模块信息异常");
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
                _logger.LogError(ex, "逻辑删除系统模块异常");
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
            if (string.IsNullOrEmpty(id))
            {
                result = CommonResults.ParameterError;
                return result;
            }
            try
            {
                result = _repository.Delete(id) > 0 ? CommonResults.Success : CommonResults.Fail;
            }
            catch (Exception ex)
            {
                result = CommonResults.Exception;
                _logger.LogError(ex, "删除系统模块异常");
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
                _logger.LogError(ex, "更改系统模块状态异常");
            }
            return result;
        }


        #endregion
        /*[end custom code bottom]*/
    }
}