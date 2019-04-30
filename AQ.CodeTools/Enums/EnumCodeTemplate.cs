using System;
using System.Collections.Generic;
using System.Text;

namespace AQ.CodeTools.Enums
{

    public enum EnumCodeTemplate
    {
        /// <summary>生成实体模型代码</summary>
        Model,
        /// <summary>生成视图实体模型代码</summary>
        ViewModel,
        /// <summary>生成仓储层代码</summary>
        IRepository,
        /// <summary>生成仓储层代码</summary>
        Repository,
        /// <summary>生成服务层代码</summary>
        IService,
        /// <summary>生成服务层代码</summary>
        Service,
        /// <summary>生成所有层代码</summary>
        All
    }
}
