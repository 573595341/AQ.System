﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AQ.ModelExtension
{
    /// <summary>
    /// 分页列表请求
    /// </summary>
    public class ListPagedRequest : BaseRequest
    {
        /// <summary>页码</summary>
        public int PageIndex { get; set; }
        /// <summary>每页数据条数</summary>
        public int PageSize { get; set; }
        /// <summary>排序字段</summary>
        public string SortName { get; set; }
        /// <summary>是否按降序排列(默认false:按升序排列)</summary>
        public bool IsSortByDesc { get; set; }

        /// <summary>
        /// 分页其实行号
        /// </summary>
        public int StartNum
        {
            get
            {
                return (PageIndex - 1) * PageSize + 1;
            }
        }

        /// <summary>
        /// 分页结束行号
        /// </summary>
        public int EndNum
        {
            get
            {
                return PageIndex * PageSize;
            }
        }

        public ListPagedRequest()
        {
            if (PageIndex <= 0)
            {
                PageIndex = 1;
            }
            if (PageSize <= 0)
            {
                PageSize = 10;
            }
        }
    }
}
