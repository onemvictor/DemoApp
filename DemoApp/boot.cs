using Autofac;
using Microsoft.Practices.Prism.Modularity;
using Prism.AutofacExtension;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Linq;

namespace DemoApp
{
    public class Bootstrapper : AutofacBootstrapper
    {
        private string _appPath=AppDomain.CurrentDomain.BaseDirectory;

        protected override void ConfigureContainer(ContainerBuilder builder)
        {
            base.ConfigureContainer(builder);
            builder.RegisterType<Shell>().SingleInstance();
            RegisterAutofacModules(builder);
          //  RegisterPrismModules(builder);            
        }


        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            // register prism module
            var modules = Directory.GetFiles(@".\Modules", "*.dll").SelectMany(p => Assembly.LoadFile(_appPath + p).GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IModule)))).ToList();
            modules.ForEach(m => ModuleCatalog.AddModule(
                new ModuleInfo { ModuleName = m.Name, ModuleType = m.AssemblyQualifiedName, Ref = new Uri(m.Assembly.Location).AbsoluteUri }));
        }

        private void RegisterAutofacModules(ContainerBuilder builder)
        {
            var assemblies = Directory.GetFiles("Modules", "*.dll").Select(path => Assembly.LoadFile(_appPath + path)).ToArray();
            builder.RegisterAssemblyModules(assemblies);
        }
        private void RegisterPrismModules(ContainerBuilder builder)
        {
            var types = Directory.GetFiles("Modules", "*.dll").SelectMany(path => Assembly.LoadFrom(_appPath + path).GetTypes().
                Where(type => type.GetInterfaces().Contains(typeof(IModule)))).ToArray();
            builder.RegisterTypes(types).AsSelf();
            builder.RegisterTypes(types).As<IModule>();
        }

        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<Shell>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (Shell)this.Shell;
            Application.Current.MainWindow.Show();
        }

        protected override void InitializeModules()
        {
            IModuleManager moduleManager = Container.Resolve<IModuleManager>();
            moduleManager.Run();
        }
    }
}