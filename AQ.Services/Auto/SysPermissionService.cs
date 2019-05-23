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
    public class SysPermissionService : ISysPermissionService
    {

		/*[begin custom code body]*/
		#region 自定义代码区域,重新生成代码不会覆盖
		private readonly ISysPermissionRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<SysPermissionService> _logger;
        public SysPermissionService(ISysPermissionRepository repository, IMapper mapper,ILogger<SysPermissionService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }
		#endregion
		/*[end custom code body]*/
        

		/*[begin custom code bottom]*/
		#region 自定义代码区域,重新生成代码不会覆盖
		#endregion
		/*[end custom code bottom]*/
    }
}