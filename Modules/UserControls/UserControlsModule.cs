using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Practices.Prism.Regions;
using DemoApp.Infrastructure;
using UserControls.View;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Prism.Modularity;
using UserControls.ViewModel;
using UserControls.Service;
using DemoApp.Interfaces;

namespace UserControls
{
    [Module(ModuleName = "UserControls",OnDemand=false)]
    public class UserControlsModule:Module,IModule
    {
        IRegionManager _regionManager;

        /// <summary>
        /// constructor which will be used while loading autofac module during builder.build
        /// </summary>
        public UserControlsModule() { }

        public UserControlsModule(IRegionManager regionManager)
        {
            this._regionManager = regionManager;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserControlsModule>().AsSelf().As<IModule>();
            
            builder.RegisterType<MainMenu>();
            builder.RegisterType<MainMenuViewModel>().InstancePerLifetimeScope();

            builder.RegisterType<Home>();

            builder.RegisterType<Login>();

            builder.RegisterType<ModuleMenuRegistry>().As<IAdapterModuleRegistry>();
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion(ShellRegions.TOOLBAR, typeof(MainMenu));
            _regionManager.RegisterViewWithRegion(ShellRegions.MAINSPACE, typeof(Home));
        }
    }
}
