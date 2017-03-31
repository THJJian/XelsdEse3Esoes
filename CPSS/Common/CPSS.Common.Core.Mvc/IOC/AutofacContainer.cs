using Autofac;

namespace CPSS.Common.Core.Mvc.Ioc
{
    /// <summary>
    ///     注册器
    /// </summary>
    public class AutofacContainerBuilder
    {
        private static readonly ContainerBuilder SysContainerBuilder = new ContainerBuilder();

        public static ContainerBuilder CurrentContainerBuilder
        {
            get { return SysContainerBuilder; }
        }
    }

    /// <summary>
    ///     注入容器
    /// </summary>
    public class AutofacServiceContainer
    {
        private static readonly IContainer SysServiceContainer = AutofacContainerBuilder.CurrentContainerBuilder.Build();

        public static IContainer CurrentServiceContainer
        {
            get { return SysServiceContainer; }
        }
    }
}