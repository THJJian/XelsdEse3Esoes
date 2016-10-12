using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Microsoft.Practices.ServiceLocation;

namespace CPSS.Common.Core.Mvc.Ioc
{
    /// <summary>
    ///     Autofac对CommonServiceLocator的实现.
    /// </summary>
    public class AutofacServiceLocator : ServiceLocatorImplBase
    {
        private readonly IComponentContext _container;

        /// <summary>
        ///     构造函数
        /// </summary>
        /// <param name="container">容器</param>
        public AutofacServiceLocator(IComponentContext container)
        {
            if (container == null)
                throw new ArgumentNullException("container");
            _container = container;
        }

        /// <summary>
        ///     Resolves the requested service instance.
        ///     解析指定类型的实例
        /// </summary>
        /// <param name="serviceType">实例类型</param>
        /// <param name="key">注册的名字，可为null</param>
        /// <returns> 返回请求的实例 </returns>
        protected override object DoGetInstance(System.Type serviceType, string key)
        {
            if (serviceType == null)
                throw new ArgumentNullException("serviceType");
            return key == null ? _container.Resolve(serviceType) : _container.ResolveNamed(key, serviceType);
        }

        /// <summary>
        ///     解析指定类型的所有实例
        /// </summary>
        /// <param name="serviceType">实例类型</param>
        /// <returns> 实例集合 </returns>
        protected override IEnumerable<object> DoGetAllInstances(System.Type serviceType)
        {
            if (serviceType == null)
                throw new ArgumentNullException("serviceType");
            return ((IEnumerable) _container.Resolve(typeof (IEnumerable<>).MakeGenericType(serviceType))).Cast<object>();
        }
    }
}