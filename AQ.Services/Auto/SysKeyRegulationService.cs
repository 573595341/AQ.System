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

/*[end custom code head]*/

namespace AQ.Services
{
    public class SysKeyRegulationService : ISysKeyRegulationService
    {

		/*[begin custom code body]*/
		#region 自定义代码区域,重新生成代码不会覆盖
		private ISysKeyRegulationRepository repository;
        private IMapper mapper;
        ILogger<SysKeyRegulationService> logger;
        public SysKeyRegulationService(ISysKeyRegulationRepository repo, IMapper map,ILogger<SysKeyRegulationService> log)
        {
            repository = repo;
            mapper = map;
            logger = log;
        }
		#endregion
		/*[end custom code body]*/
        

		/*[begin custom code bottom]*/
		#region 自定义代码区域,重新生成代码不会覆盖
		#endregion
		/*[end custom code bottom]*/
    }
}