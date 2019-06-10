using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace System.Linq
{
    public static class IQueryableExtensions
    {
        /// <summary>
        /// 条件筛选
        /// </summary>
        /// <typeparam name="T">实体对象</typeparam>
        /// <param name="source"></param>
        /// <param name="condition">true:执行predicate false:返回原集合</param>
        /// <param name="predicate">条件树表达式</param>
        /// <returns></returns>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, bool condition, Expression<Func<T, bool>> predicate)
        {
            if (!condition)
            {
                return source;
            }
            return source.Where(predicate);
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <typeparam name="T">实体对象</typeparam>
        /// <typeparam name="TKey">排序字段</typeparam>
        /// <param name="source"></param>
        /// <param name="isSortByDesc">是否按照倒序排序</param>
        /// <param name="predicate">条件树表达式</param>
        /// <returns></returns>
        public static IQueryable<T> OrderIf<T, TKey>(this IQueryable<T> source, bool isSortByDesc, Expression<Func<T, TKey>> predicate)
        {
            var memberExpression = predicate.Body as System.Linq.Expressions.MemberExpression;
            var prop = typeof(T).GetProperty(memberExpression.Member.Name, Reflection.BindingFlags.IgnoreCase | Reflection.BindingFlags.Instance | Reflection.BindingFlags.Public);
            if (prop != null)
            {
                return isSortByDesc ? source.OrderByDescending(predicate) : source.OrderBy(predicate);
            }
            return source;
        }
    }
}
