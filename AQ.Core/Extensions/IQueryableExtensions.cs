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
        /// 条件排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TProp"></typeparam>
        /// <param name="source"></param>
        /// <param name="sortName">指定排序字段名称(必须为T对象属性, 如果不存在则按照默认属性排序)</param>
        /// <param name="defaultPredicate">默认排序字段</param>
        /// <param name="isSortByDesc">是否降序排列</param>
        /// <returns></returns>
        public static IOrderedQueryable<T> OrderIf<T, TProp>(this IQueryable<T> source, string sortName, Expression<Func<T, TProp>> defaultPredicate, bool isSortByDesc = true)
        {
            if (string.IsNullOrEmpty(sortName))
            {
                return isSortByDesc ? source.OrderByDescending(defaultPredicate) : source.OrderBy(defaultPredicate);
            }
            else
            {
                return _OrderBy(source, sortName, isSortByDesc);
            }
        }

        /// <summary>
        /// 按照指定字段升序排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="sortName">排序字段名称(必须为T对象字段)</param>
        /// <returns></returns>
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string sortName)
        {
            return _OrderBy<T>(source, sortName, false);
        }

        /// <summary>
        /// 按照指定字段倒序排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="sortName">排序字段名称(必须为T对象字段)</param>
        /// <returns></returns>
        public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string sortName)
        {
            return _OrderBy<T>(source, sortName, true);
        }

        /// <summary>
        /// 根据指定字段执行排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="propertyName">排序字段名称</param>
        /// <param name="isDesc"></param>
        /// <returns></returns>
        private static IOrderedQueryable<T> _OrderBy<T>(IQueryable<T> source, string propertyName, bool isDesc)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                return (IOrderedQueryable<T>)source;
            }
            var memberProp = typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
            if (memberProp == null)
            {
                return (IOrderedQueryable<T>)source;
            }
            string methodname = isDesc ? "OrderByDescendingInternal" : "OrderByInternal";
            var method = typeof(IQueryableExtensions).GetMethod(methodname, BindingFlags.NonPublic | BindingFlags.Static).MakeGenericMethod(typeof(T), memberProp.PropertyType);
            return (IOrderedQueryable<T>)method.Invoke(null, new object[] { source, memberProp });
        }

        /// <summary>
        /// 升序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TProp"></typeparam>
        /// <param name="source"></param>
        /// <param name="memberProperty"></param>
        /// <returns></returns>
        private static IOrderedQueryable<T> OrderByInternal<T, TProp>(IQueryable<T> source, PropertyInfo memberProperty)
        {
            return source.OrderBy(_GetLamba<T, TProp>(memberProperty));
        }

        /// <summary>
        /// 降序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TProp"></typeparam>
        /// <param name="source"></param>
        /// <param name="memberProperty"></param>
        /// <returns></returns>
        private static IOrderedQueryable<T> OrderByDescendingInternal<T, TProp>(IQueryable<T> source, PropertyInfo memberProperty)
        {
            return source.OrderByDescending(_GetLamba<T, TProp>(memberProperty));
        }

        /// <summary>
        /// 获取lambda表达式树
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TProp"></typeparam>
        /// <param name="memberProperty">对象指定属性</param>
        /// <returns></returns>
        private static Expression<Func<T, TProp>> _GetLamba<T, TProp>(PropertyInfo memberProperty)
        {
            //if (memberProperty.PropertyType != typeof(TProp)) throw new Exception();
            //lambda表达式树参数,和委托参数保持一致
            var lambdaPara = Expression.Parameter(typeof(T));
            //lambda表达式树主体，访问对象T中的某个字段或者属性
            var body = Expression.Property(lambdaPara, memberProperty);
            //创建lambda表达式树
            var lambda = Expression.Lambda<Func<T, TProp>>(body, lambdaPara);
            return lambda;
        }
    }
}
