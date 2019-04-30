using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Linq;

namespace AQ.CodeTools
{
    internal static class ObjectExtenstion
    {
        /// <summary>
        /// 将DataTable转换为T类型对象集合
        /// </summary>
        /// <typeparam name="T">转换后类型</typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> ToList<T>(this DataTable dt) where T : class, new()
        {
            if (dt == null) { return null; };
            var result = new List<T>();
            var type = typeof(T);
            var allProps = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty | BindingFlags.IgnoreCase).ToList<PropertyInfo>();
            List<PropertyInfo> props = new List<PropertyInfo>();
            foreach (var prop in allProps)
            {
                if (dt.Columns.Contains(prop.Name) && prop.CanWrite)
                {
                    props.Add(prop);
                }
            }

            foreach (DataRow row in dt.Rows)
            {
                T obj = new T();
                foreach (var prop in props)
                {
                    var value = row[prop.Name];
                    if (value != DBNull.Value)
                    {
                        prop.SetValue(obj, ChangeType(value, prop.PropertyType));
                    }
                }
                result.Add(obj);
            }
            return result;
        }

        /// <summary>
        /// 转换指定类型对象值（包含Nullable<>和非Nullable<>转换）
        /// </summary>
        /// <param name="value">转换的值</param>
        /// <param name="conversionType">转换类型</param>
        /// <returns></returns>
        private static object ChangeType(object value, Type conversionType)
        {
            if (conversionType == null)
            {
                return null;
            }

            //判断类型是否为可空类型
            if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null) { return null; }
                //获取可空对象的底层对象类型
                System.ComponentModel.NullableConverter nullableConverter = new System.ComponentModel.NullableConverter(conversionType);
                conversionType = nullableConverter.UnderlyingType;
            }
            return Convert.ChangeType(value, conversionType);
        }
    }
}
