using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AQ.WebMain.Commons.Extensions
{
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// 注册指定程序集中所有服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblyName"></param>
        public static void AddScopedByAssemblyName(this IServiceCollection services, string assemblyName)
        {
            var assembly = Assembly.Load(assemblyName);
            var types = assembly.GetTypes().ToList();
            foreach (var t in types)
            {
                if (!t.IsInterface && !string.IsNullOrEmpty(t.Namespace) && t.Namespace.IndexOf(assemblyName) > -1)
                {
                    var interfaces = t.GetInterfaces();
                    if (interfaces.Length > 0)
                    {
                        services.AddScoped(interfaces.Last(), t);
                        //foreach (var i in interfaces)
                        //{
                        //    services.AddScoped(i, t);
                        //}
                    }
                    else
                    {
                        services.AddScoped(t);
                    }

                }
            }
        }

        /// <summary>
        /// 注册指定程序集中所有服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblyName"></param>
        public static void AddTransientByAssemblyName(this IServiceCollection services, string assemblyName)
        {
            var assembly = Assembly.Load(assemblyName);
            var types = assembly.GetTypes().ToList();
            foreach (var t in types)
            {
                if (!t.IsInterface && !string.IsNullOrEmpty(t.Namespace) && t.Namespace.IndexOf(assemblyName) > -1)
                {
                    var interfaces = t.GetInterfaces();
                    if (interfaces.Length > 0)
                    {
                        services.AddTransient(interfaces.Last(), t);
                        //foreach (var i in interfaces)
                        //{
                        //    services.AddTransient(i, t);
                        //}
                    }
                    else
                    {
                        services.AddTransient(t);
                    }
                }
            }
        }
    }
}
