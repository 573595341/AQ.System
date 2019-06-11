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
    public class SysUserServiceNew : ISysUserServiceNew
    {

        /*[begin custom code body]*/
        #region 自定义代码区域,重新生成代码不会覆盖
        private readonly ISysUserRepositoryNew _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<SysUserService> _logger;
        private readonly ISysKeyRegulationRepository _keyRepository;
        public SysUserServiceNew(ISysUserRepositoryNew repository, IMapper mapper, ILogger<SysUserService> logger, ISysKeyRegulationRepository keyRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _keyRepository = keyRepository;
        }
        #endregion
        /*[end custom code body]*/


        /*[begin custom code bottom]*/
        #region 自定义代码区域,重新生成代码不会覆盖

        /// <summary>
        /// 获取所有系统用户数据信息
        /// </summary>
        /// <returns></returns>
        public ListPagedResult<SysUserViewModel> GetListAll()
        {
            var result = new ListPagedResult<SysUserViewModel>();
            try
            {
                var data = _repository.GetAllList().ToList();
                result.Data = _mapper.Map<List<SysUserViewModel>>(data.Where(d => !d.IsDelete && d.Status == 1));
                result.ResultCode = CommonResults.Success.ResultCode;
                result.ResultMsg = CommonResults.Success.ResultMsg;
            }
            catch (Exception ex)
            {
                result.ResultCode = CommonResults.Exception.ResultCode;
                result.ResultMsg = CommonResults.Exception.ResultMsg;
                _logger.LogError(ex, "获取用户列表信息异常");
            }
            return result;
        }

        /// <summary>
        /// 获取所有系统用户数据信息
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public ListPagedResult<SysUserViewModel> GetListPaged(SysUserCondition condition)
        {
            var result = new ListPagedResult<SysUserViewModel>();
            try
            {
                var data = _repository.GetListPaged(condition);
                data.Data.ForEach(d =>
                {
                    result.Data.Add(_mapper.Map<SysUserViewModel>(d));
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
                _logger.LogError(ex, "获取用户列表信息异常");
            }
            return result;
        }

        /// <summary>
        /// 获取系统用户详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BaseResult<SysUserViewModel> GetDetail(string id)
        {
            var result = new BaseResult<SysUserViewModel>();
            if (string.IsNullOrEmpty(id))
            {
                result.ResultMsg = CommonResults.ParameterError.ResultMsg;
                result.ResultCode = CommonResults.ParameterError.ResultCode;
                return result;
            }
            try
            {
                var data = _repository.Get(t => t.Id == id);
                result.Data = _mapper.Map<SysUserViewModel>(data);
                result.ResultCode = CommonResults.Success.ResultCode;
                result.ResultMsg = CommonResults.Success.ResultMsg;
            }
            catch (Exception ex)
            {
                result.ResultCode = CommonResults.Exception.ResultCode;
                result.ResultMsg = CommonResults.Exception.ResultMsg;
                _logger.LogError(ex, "获取用户信息异常");
            }
            return result;
        }

        /// <summary>
        /// 添加系统用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BaseResult Add(SysUserViewModel model)
        {
            var result = new BaseResult();
            try
            {
                var validationResult = new UserValidation().Validate(model, ruleSet: "Add");
                if (!validationResult.IsValid)
                {
                    result.ResultMsg = validationResult.ToString(";");
                    result.ResultCode = CommonResults.ParameterError.ResultCode;
                    return result;
                }
                var data = _mapper.Map<SysUser>(model);
                data.Id = _keyRepository.GenerateKey("Id", "SysUser");
                data.CreateUser = string.Empty;
                data.ModifyUser = string.Empty;
                _repository.Add(data);
                //=  != null ? result = CommonResults.Success : CommonResults.Fail;
                result = _repository.SaveChanges() > 0 ? CommonResults.Success : CommonResults.Fail;
            }
            catch (Exception ex)
            {
                result = CommonResults.Exception;
                _logger.LogError(ex, "添加系统用户信息异常");
            }
            return result;
        }

        /// <summary>
        /// 更新系统用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BaseResult Update(SysUserViewModel model)
        {
            var result = new BaseResult();
            try
            {
                var validationResult = new UserValidation().Validate(model, ruleSet: "Update");
                if (!validationResult.IsValid)
                {
                    result.ResultMsg = validationResult.ToString(";");
                    result.ResultCode = CommonResults.ParameterError.ResultCode;
                    return result;
                }
                var data = _mapper.Map<SysUser>(model);
                data.ModifyTime = DateTime.Now;
                _repository.Update(data);
                if (_repository.SaveChanges() > 0)
                {
                    result = CommonResults.Success;
                }
            }
            catch (Exception ex)
            {
                result.ResultCode = CommonResults.Exception.ResultCode;
                result.ResultMsg = CommonResults.Exception.ResultMsg;
                _logger.LogError(ex, "更新系统用户信息异常");
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
            if (keys == null || keys.Length == 0)
            {
                result.ResultMsg = CommonResults.ParameterError.ResultMsg;
                return result;
            }
            try
            {
                result = _repository.DeleteLogical(keys) > 0 ? CommonResults.Success : CommonResults.Fail;
            }
            catch (Exception ex)
            {
                result.ResultCode = CommonResults.Exception.ResultCode;
                result.ResultMsg = CommonResults.Exception.ResultMsg;
                _logger.LogError(ex, "逻辑删除系统用户异常");
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
                return result = CommonResults.ParameterError;
            }
            try
            {
                _repository.Delete(id);
                result = _repository.SaveChanges() > 0 ? CommonResults.Success : CommonResults.Fail;
            }
            catch (Exception ex)
            {
                result = CommonResults.Exception;
                _logger.LogError(ex, "删除系统用户异常");
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
                _logger.LogError(ex, "更改系统用户状态异常");
            }
            return result;
        }

        #endregion
        /*[end custom code bottom]*/
    }
}