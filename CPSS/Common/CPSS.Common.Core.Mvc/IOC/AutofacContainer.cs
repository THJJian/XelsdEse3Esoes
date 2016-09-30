using Autofac;

namespace CPSS.Common.Core.Mvc.Ioc
{
    /// <summary>
    ///     注册器
    /// </summary>
    public class AutofacContainerBuilder
    {
        public static ContainerBuilder CurrentContainerBuilder { get; } = new ContainerBuilder();
    }

    /// <summary>
    ///     注入容器
    /// </summary>
    public class AutofacServiceContainer
    {
        public static IContainer CurrentServiceContainer { get; } = AutofacContainerBuilder.CurrentContainerBuilder.Build();
    }
}