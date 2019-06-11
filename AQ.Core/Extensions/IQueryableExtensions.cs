using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using System.Reflection;

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


        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> query, string propertyName)
        {
            return _OrderBy<T>(query, propertyName, false);
        }

        public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> query, string propertyName)
        {
            return _OrderBy<T>(query, propertyName, true);
        }

        static IOrderedQueryable<T> _OrderBy<T>(IQueryable<T> query, string propertyName, bool isDesc)
        {
            string methodname = (isDesc) ? "OrderByDescendingInternal" : "OrderByInternal";

            var memberProp = typeof(T).GetProperty(propertyName);

            var method = typeof(IQueryableExtensions).GetMethod(methodname).MakeGenericMethod(typeof(T), memberProp.PropertyType);

            return (IOrderedQueryable<T>)method.Invoke(null, new object[] { query, memberProp });
        }

        public static IOrderedQueryable<T> OrderByInternal<T, TProp>(IQueryable<T> query, PropertyInfo memberProperty)
        {
            return query.OrderBy(_GetLamba<T, TProp>(memberProperty));
        }

        public static IOrderedQueryable<T> OrderByDescendingInternal<T, TProp>(IQueryable<T> query, PropertyInfo memberProperty)
        {
            return query.OrderByDescending(_GetLamba<T, TProp>(memberProperty));
        }

        static Expression<Func<T, TProp>> _GetLamba<T, TProp>(PropertyInfo memberProperty)
        {
            if (memberProperty.PropertyType != typeof(TProp)) throw new Exception();

            var thisArg = Expression.Parameter(typeof(T));
            var lamba = Expression.Lambda<Func<T, TProp>>(Expression.Property(thisArg, memberProperty), thisArg);
            return lamba;
        }
    }
}
