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
    public class SysKeyRegulationService : ISysKeyRegulationService
    {

        /*[begin custom code body]*/
        #region 自定义代码区域,重新生成代码不会覆盖
        private readonly ISysKeyRegulationRepository _repository;
        private readonly IMapper _mapper;
        ILogger<SysKeyRegulationService> _logger;
        public SysKeyRegulationService(ISysKeyRegulationRepository repo, IMapper map, ILogger<SysKeyRegulationService> log)
        {
            _repository = repo;
            _mapper = map;
            _logger = log;
        }
        #endregion
        /*[end custom code body]*/


        /*[begin custom code bottom]*/
        #region 自定义代码区域,重新生成代码不会覆盖
        #endregion
        /*[end custom code bottom]*/
    }
}