using Autofac;
using Octonauts.Channel;
using Octonauts.Core.CommandsFramework;
using OctonautsCli.FeatureLevelCommands;
using System.Reflection;
using Octonauts.Environment;
using Octonauts.Machines;
using Octonauts.Packages;
using Octonauts.Release;

namespace OctonautsCli
{
    internal static class IocSetup
    {
        public static IContainer Setup()
        {
            var builder = new ContainerBuilder();
            var assemblies = new []
            {
                Assembly.GetAssembly(typeof(ChannelFeature))!,
                Assembly.GetAssembly(typeof(ReleaseFeature))!,
                Assembly.GetAssembly(typeof(MachineFeature))!,
                Assembly.GetAssembly(typeof(PackageFeature))!,
                Assembly.GetAssembly(typeof(EnvironmentFeature))!,
            };
            builder.RegisterAssemblyTypes(assemblies)
                .AssignableTo<ICommandHandler>().Where(x => !x.IsAbstract)
                .As<ICommandHandler>();

            builder.RegisterAssemblyTypes(assemblies)
                .AssignableTo<AbstractFeature>().Where(x => !x.IsAbstract)
                .As<AbstractFeature>();

            builder.RegisterType<AllFeatures>().AsSelf().SingleInstance();
            return builder.Build();
        }
    }
}
