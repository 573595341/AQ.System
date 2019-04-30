using System;
using System.Collections.Generic;
using System.Text;

namespace AQ.ModelExtension
{
    /// <summary>
    /// 分页结果
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class ListPagedResult<TModel> : BaseResult where TModel : class
    {
        /// <summary>页码</summary>
        public int PageIndex { get; set; }
        /// <summary>每页数据条数</summary>
        public int PageSize { get; set; }
        /// <summary>总页数</summary>
        public int PageCount { get; set; }
        /// <summary>总数据条数</summary>
        public int TotalData { get; set; }
        /// <summary>当前页码数据</summary>
        public List<TModel> Data { get; set; }

        public ListPagedResult()
        {
            if (PageSize <= 0)
            {
                PageSize = 10;
            }
            if (PageIndex <= 0)
            {
                PageIndex = 1;
            }
            Data = new List<TModel>();
        }

        ///// <summary>
        ///// 获取分页起始和结束编号
        ///// </summary>
        ///// <param name="startNum"></param>
        ///// <param name="endNum"></param>
        //public void GetStartAndEndNumber(out int startNum, out int endNum)
        //{
        //    if (PageIndex <= 0)
        //    {
        //        PageIndex = 1; ;
        //    }
        //    if (PageIndex > PageSize)
        //    {
        //        PageIndex = PageSize;
        //    }
        //    startNum = (PageIndex - 1) * PageSize + 1;
        //    endNum = startNum + PageSize - 1;
        //}

        /// <summary>
        /// 获取总数据条数
        /// (通过TotalData属性计算, 已赋值给PageCount属性)
        /// </summary>
        /// <returns></returns>
        public int GetPageCount()
        {
            if (TotalData <= 0)
            {
                return 0;
            }
            int pageCount = int.TryParse(Math.Ceiling(TotalData * 1.0 / PageSize).ToString(), out pageCount)
                ? pageCount : 0;
            PageCount = pageCount;
            return pageCount;
        }
    }
}
