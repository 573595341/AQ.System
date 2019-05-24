﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AQ.Models
{
    public class ModulePermission
    {
        /// <summary>权限id</summary>
        public Int64 Id { get; set; }
        /// <summary>模块id</summary>
        public string SId { get; set; }
        /// <summary>模块名称</summary>
        public string Name { get; set; }
        /// <summary>权限值</summary>
        public Int32 Value { get; set; }
    }
}
