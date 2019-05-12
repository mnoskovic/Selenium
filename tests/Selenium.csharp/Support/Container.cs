using Autofac;
using SpecFlow.Autofac;
using System;
using System.Linq;
using TechTalk.SpecFlow;

namespace Selenium.csharp
{
    public static class Container
    {
        [ScenarioDependencies]
        public static ContainerBuilder CreateContainerBuilder()
        {
            var builder = new ContainerBuilder();

            var provider = new DriverProvider();
            builder.RegisterInstance<IDriverProvider>(provider);
            builder.RegisterInstance<IDriverManager>(provider);

            builder.RegisterTypes(typeof(Container).Assembly.GetTypes().Where(t => Attribute.IsDefined(t, typeof(BindingAttribute))).ToArray()).SingleInstance();

            return builder;
        }
    }
}
