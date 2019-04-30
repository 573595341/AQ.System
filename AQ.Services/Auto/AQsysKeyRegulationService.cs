using System;
using AQ.IRepository;
using AQ.Models;
using AQ.ViewModels;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using AQ.IServices;
using AutoMapper;
/*[begin custom code head]*/
//自定义命名空间区域
/*[end custom code head]*/

namespace AQ.Services
{
    public class AQsysKeyRegulationService : IAQsysKeyRegulationService
    {

		/*[begin custom code body]*/

		//自定义代码区域,重新生成代码不会覆盖
		private IAQsysKeyRegulationRepository _repository;
        private IMapper _mapper;
        public AQsysKeyRegulationService(IAQsysKeyRegulationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

		/*[end custom code body]*/
        

		/*[begin custom code bottom]*/
		//自定义代码区域,重新生成代码不会覆盖
		/*[end custom code bottom]*/
    }
}