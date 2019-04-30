using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;

namespace Demo.Demo
{
    public class DemoService
    {
        public void Start()
        {
            var rootProvider = GetServiceProvider();

            var provider1 = rootProvider.CreateScope().ServiceProvider;
            var provider2 = rootProvider.CreateScope().ServiceProvider;
            //var provider3 = provider2.CreateScope().ServiceProvider;

            var demob = rootProvider.GetRequiredService<IOptions<DemoB>>();


            Console.WriteLine("DemoB rootProvider");
            GetService<DemoB>(rootProvider);
            Console.WriteLine("DemoB provider1");
            GetService<DemoB>(provider1);
            Console.WriteLine("DemoB provider2");
            GetService<DemoB>(provider2);

            Console.WriteLine("DemoB provider3");
            using (var scope = provider2.CreateScope())
            {
                GetService<DemoB>(scope.ServiceProvider);
            }



            //using (var provider3 = rootProvider.CreateScope().ServiceProvider)
            //{
            //    Console.WriteLine("DemoA provider3");
            //    GetService<DemoA>(provider3);
            //}

        }


        public void GetService<T>(IServiceProvider provider)
        {
            provider.GetService<T>();
            provider.GetService<T>();
        }

        public IServiceProvider GetServiceProvider()
        {
            var services = new ServiceCollection();
            services.AddSingleton<DemoA>();
            //services.AddScoped<DemoB>(s => new DemoB() { Key = "demob" });
            services.Configure<DemoB>(d =>
            {
                d.Key = "demob";
            });
            services.AddTransient<DemoC>();
            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider;
        }


        class DemoA : IDisposable
        {
            public string Key;
            public DemoA()
            {
                Console.WriteLine($"DemoA 创建");
            }

            public void Dispose()
            {
                Console.WriteLine($"DemoA 释放");
            }
        }

        class DemoB : IDisposable
        {
            public string Key;
            public DemoB()
            {
                Console.WriteLine($"DemoB 创建");

            }

            public void Dispose()
            {
                Console.WriteLine($"DemoB 释放");
            }
        }

        class DemoC : IDisposable
        {
            public string Key;
            public DemoC()
            {
                Console.WriteLine($"DemoC 创建");
            }

            public void Dispose()
            {
                Console.WriteLine($"DemoC 释放");
            }
        }
    }
}
