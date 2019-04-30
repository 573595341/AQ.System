using System;
using AQ.IRepository;
using AQ.Models;
using AQ.ViewModels;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using AQ.IServices;
using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
/*[begin custom code head]*/
//自定义命名空间区域
using AQ.ModelExtension;
using AQ.Services.Validation;

/*[end custom code head]*/

namespace AQ.Services
{
    public class SysMenuService : ISysMenuService
    {

		/*[begin custom code body]*/
        #region 自定义代码区域,重新生成代码不会覆盖
        private ISysMenuRepository repository;
        private IMapper mapper;
        private ILogger<SysMenuService> logger;
        private ISysKeyRegulationRepository keyRepository;

        public SysMenuService(ISysMenuRepository repo, IMapper map, ILogger<SysMenuService> log, ISysKeyRegulationRepository keyReg)
        {
            repository = repo;
            mapper = map;
            logger = log;
            keyRepository = keyReg;
        }
        #endregion
        /*[end custom code body]*/
        

		/*[begin custom code bottom]*/
        #region 自定义代码区域,重新生成代码不会覆盖

        /// <summary>
        /// 获取所有系统模块数据信息
        /// </summary>
        /// <returns></returns>
        public ListPagedResult<SysMenuViewModel> GetListAll()
        {
            var result = new ListPagedResult<SysMenuViewModel>();
            try
            {
                var data = repository.GetList().Where(d => !d.IsDelete && d.Status == 1).ToList();
                result.Data = mapper.Map<List<SysMenuViewModel>>(data);
                result.ResultCode = CommonResults.Success.ResultCode;
                result.ResultMsg = CommonResults.Success.ResultMsg;
            }
            catch (Exception ex)
            {
                result.ResultCode = CommonResults.Exception.ResultCode;
                result.ResultMsg = CommonResults.Exception.ResultMsg;
                logger.LogError(ex, $"获取所有菜单信息错误, ERROR:{ex.Message}");
            }
            return result;
        }

        /// <summary>
        /// 获取所有系统模块数据信息
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public ListPagedResult<SysMenuViewModel> GetListPaged(SysMenuCondition condition)
        {
            var result = new ListPagedResult<SysMenuViewModel>();
            try
            {
                var data = repository.GetListPaged(condition);
                result.Data = mapper.Map<List<SysMenuViewModel>>(data.Data);
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
                logger.LogError(ex, "获取菜单列表信息异常");
            }
            return result;
        }

        /// <summary>
        /// 获取系统模块详情
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public BaseResult<SysMenuViewModel> GetDetail(string moduleId)
        {
            var result = new BaseResult<SysMenuViewModel>();
            try
            {
                if (string.IsNullOrEmpty(moduleId))
                {
                    result.ResultMsg = "参数错误";
                    return result;
                }
                var data = repository.Get(moduleId);
                result.Data = mapper.Map<SysMenuViewModel>(data);
                result.ResultCode = CommonResults.Success.ResultCode;
                result.ResultMsg = CommonResults.Success.ResultMsg;
            }
            catch (Exception ex)
            {
                result.ResultCode = CommonResults.Exception.ResultCode;
                result.ResultMsg = CommonResults.Exception.ResultMsg;
                logger.LogError(ex, "获取菜单信息异常");
            }
            return result;
        }

        /// <summary>
        /// 添加系统模块信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BaseResult Add(SysMenuViewModel model)
        {
            var result = new BaseResult();
            try
            {
                var validationResult = new MenuValidation().Validate(model, ruleSet: "Add");
                if (validationResult.IsValid)
                {
                    var data = mapper.Map<SysMenu>(model);
                    data.Id = keyRepository.GenerateKey("Id", "SysMenu");
                    data.CreateUser = string.Empty;
                    data.ModifyUser = string.Empty;
                    data.PageUrl = data.PageUrl ?? string.Empty;
                    if (repository.Insert(data) != null)
                    {
                        result = CommonResults.Success;
                    }
                    else
                    {
                        result = CommonResults.Fail;
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
                logger.LogError(ex, "添加菜单信息异常");
            }
            return result;
        }

        /// <summary>
        /// 更新系统模块信息
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
                    var data = mapper.Map<SysMenu>(model);
                    data.ModifyTime = DateTime.Now;
                    if (repository.Update(data) > 0)
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
                logger.LogError(ex, "更新菜单信息异常");
            }
            return result;
        }

        /// <summary>
        /// 更改状态
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public BaseResult<bool> ChangeStatus(string[] keys, int status)
        {
            var result = new BaseResult<bool>();
            if (keys == null || keys.Length == 0)
            {
                result.ResultCode = CommonResults.ParameterError.ResultCode;
                result.ResultMsg = CommonResults.ParameterError.ResultMsg;
                return result;
            }
            result.Data = repository.UpdateStatus(status, keys) > 0;
            if (result.Data)
            {
                result.ResultCode = CommonResults.Success.ResultCode;
                result.ResultMsg = CommonResults.Success.ResultMsg;
            }
            else
            {
                result.ResultCode = CommonResults.Fail.ResultCode;
                result.ResultMsg = CommonResults.Fail.ResultMsg;
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
                    result.ResultMsg = "参数错误";
                    return result;
                }
                if (repository.DeleteLogical(keys) > 0)
                {
                    result = CommonResults.Success;
                }
                else
                {
                    result = CommonResults.Fail;
                }
            }
            catch (Exception ex)
            {
                result.ResultCode = CommonResults.Exception.ResultCode;
                result.ResultMsg = CommonResults.Exception.ResultMsg;
                logger.LogError(ex, "逻辑删除菜单信息异常");
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
                    result.ResultMsg = "参数错误";
                    return result;
                }
                if (repository.Delete(id) > 0)
                {
                    result = CommonResults.Success;
                }
                else
                {
                    result = CommonResults.Fail;
                }
            }
            catch (Exception ex)
            {
                result.ResultCode = CommonResults.Exception.ResultCode;
                result.ResultMsg = CommonResults.Exception.ResultMsg;
                logger.LogError(ex, "删除菜单信息异常");
            }
            return result;
        }

        #endregion
        /*[end custom code bottom]*/
    }
}