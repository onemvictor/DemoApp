//using Autofac;
//using DemoApp.Infrastructure;
//using Microsoft.Practices.Prism;
//using Microsoft.Practices.Prism.Events;
//using Microsoft.Practices.Prism.Logging;
//using Microsoft.Practices.Prism.Modularity;
//using Microsoft.Practices.Prism.Regions;
//using Microsoft.Practices.Prism.Regions.Behaviors;
//using Microsoft.Practices.ServiceLocation;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Reflection;
//using System.Windows;

//namespace DemoApp
//{
//    internal class AutofacBootstrapper:Bootstrapper
//    {
//        private string _appPath = AppDomain.CurrentDomain.BaseDirectory;

//        private ContainerBuilder _builder;

//        public override void Run(bool runWithDefaultConfiguration)
//        {
//            Logger=CreateLogger();
//            this.ModuleCatalog=CreateModuleCatalog();
//            ConfigureContainer(Builder);
//            Container=CreateContainer();
//            ConfigureServiceLocator();
//            ConfigureRegionAdapterMappings();
//            ConfigureDefaultRegionBehaviors();
//            RegisterFrameworkExceptionTypes();
//            Shell=CreateShell();
//            RegionManager.SetRegionManager(Shell, Container.Resolve<IRegionManager>());
//            InitializeShell();
//            ConfigureModuleCatalog();
//            InitializeModules();
//        }

//        protected override ILoggerFacade CreateLogger()
//        {
//            return new DemoApp.Logging.Logger();
//        }

//        protected override void ConfigureModuleCatalog()
//        {
//            base.ConfigureModuleCatalog();

//            // register prism module
//            var modules=Container.Resolve<IEnumerable<IModule>>().Select(m=>m.GetType()).ToList();
//            modules.ForEach(m => ModuleCatalog.AddModule(
//                new ModuleInfo { ModuleName = m.Name, ModuleType = m.AssemblyQualifiedName, Ref =new Uri(m.Assembly.Location).AbsoluteUri }));
//        }

//        protected virtual void ConfigureContainer(ContainerBuilder builder)
//        {
//            builder.RegisterInstance<ILoggerFacade>(this.Logger);

//            builder.RegisterInstance<IModuleCatalog>(this.ModuleCatalog);

//            RegisterTypeIfMissing(builder, typeof(IServiceLocator), typeof(AutofacServiceLocator), true);
//            RegisterTypeIfMissing(builder, typeof(IModuleInitializer), typeof(ModuleInitializer), true);
//            RegisterTypeIfMissing(builder, typeof(IModuleManager), typeof(ModuleManager), true);
//            RegisterTypeIfMissing(builder, typeof(RegionAdapterMappings), typeof(RegionAdapterMappings), true);
//            RegisterTypeIfMissing(builder, typeof(IRegionManager), typeof(RegionManager), true);

//            RegisterTypeIfMissing(builder, typeof(SelectorRegionAdapter), typeof(SelectorRegionAdapter), true);
//            RegisterTypeIfMissing(builder, typeof(ItemsControlRegionAdapter), typeof(ItemsControlRegionAdapter), true);
//            RegisterTypeIfMissing(builder, typeof(ContentControlRegionAdapter), typeof(ContentControlRegionAdapter), true);

//            RegisterTypeIfMissing(builder, typeof(DelayedRegionCreationBehavior), typeof(DelayedRegionCreationBehavior), true);
//            RegisterTypeIfMissing(builder, typeof(BindRegionContextToDependencyObjectBehavior), typeof(BindRegionContextToDependencyObjectBehavior), true);
//            RegisterTypeIfMissing(builder, typeof(AutoPopulateRegionBehavior), typeof(AutoPopulateRegionBehavior), true);
//            RegisterTypeIfMissing(builder, typeof(RegionActiveAwareBehavior), typeof(RegionActiveAwareBehavior), true);
//            RegisterTypeIfMissing(builder, typeof(SyncRegionContextWithHostBehavior), typeof(SyncRegionContextWithHostBehavior), true);
//            RegisterTypeIfMissing(builder, typeof(RegionManagerRegistrationBehavior), typeof(RegionManagerRegistrationBehavior), true);
//            RegisterTypeIfMissing(builder, typeof(RegionMemberLifetimeBehavior), typeof(RegionMemberLifetimeBehavior), true);
//            RegisterTypeIfMissing(builder, typeof(ClearChildViewsRegionBehavior), typeof(ClearChildViewsRegionBehavior), true);

//            RegisterTypeIfMissing(builder, typeof(IEventAggregator), typeof(EventAggregator), true);
//            RegisterTypeIfMissing(builder, typeof(IRegionViewRegistry), typeof(RegionViewRegistry), true);
//            RegisterTypeIfMissing(builder, typeof(IRegionBehaviorFactory), typeof(RegionBehaviorFactory), true);
//            RegisterTypeIfMissing(builder, typeof(IRegionNavigationContentLoader), typeof(RegionNavigationContentLoader), true);
//            RegisterTypeIfMissing(builder, typeof(IRegionNavigationJournalEntry), typeof(RegionNavigationJournalEntry), false);
//            RegisterTypeIfMissing(builder, typeof(IRegionNavigationJournal), typeof(RegionNavigationJournal), false);
//            RegisterTypeIfMissing(builder, typeof(IRegionNavigationService), typeof(RegionNavigationService), false);

//            RegisterTypeIfMissing(builder, typeof(Shell), typeof(Shell), true);
//            RegisterAutofacModules(builder);
//            RegisterPrismModules(builder);
//        }

//        private IContainer CreateContainer()
//        {
//            return Builder.Build();
//        }

//        protected override void ConfigureServiceLocator()
//        {
//            ServiceLocator.SetLocatorProvider(() => Container.Resolve<IServiceLocator>());
//        }

//        protected override void RegisterFrameworkExceptionTypes()
//        {
//            base.RegisterFrameworkExceptionTypes();

//            ExceptionExtensions.RegisterFrameworkExceptionType(typeof(Autofac.Core.DependencyResolutionException));
//        }

//        protected override DependencyObject CreateShell()
//        {
//            return Container.Resolve<Shell>();
//        }

//        protected override void InitializeShell()
//        {
//            Application.Current.MainWindow = (Shell)this.Shell;
//            Application.Current.MainWindow.Show();
//        }

//        protected override void InitializeModules()
//        {
//            IModuleManager moduleManager = Container.Resolve<IModuleManager>();
//            moduleManager.Run();
//        }
//        /// <summary>
//        /// registers autofac modules
//        /// </summary>
//        /// <param name="builder">The builder</param>
//        private void RegisterAutofacModules(ContainerBuilder builder)
//        {
//            var assemblies = Directory.GetFiles("Modules", "*.dll").Select(path => Assembly.LoadFile(_appPath + path)).ToArray();
//            builder.RegisterAssemblyModules(assemblies);
//        }

//        private void RegisterPrismModules(ContainerBuilder builder)
//        {
//            var types = Directory.GetFiles("Modules", "*.dll").SelectMany(path => Assembly.LoadFrom(_appPath + path).GetTypes().
//                Where(type => type.GetInterfaces().Contains(typeof(IModule)))).ToArray();
//            builder.RegisterTypes(types).AsSelf();
//            builder.RegisterTypes(types).As<IModule>();
//        }
//        /// <summary>
//        /// Registers the type if missing.
//        /// </summary>
//        /// <param name="builder">The builder.</param>
//        /// <param name="fromType">From type.</param>
//        /// <param name="toType">To type.</param>
//        /// <param name="registerAsSingleton">if set to <c>true</c> [register as singleton].</param>
//        private void RegisterTypeIfMissing(ContainerBuilder builder, Type fromType, Type toType, bool registerAsSingleton)
//        {
//            if (fromType == (Type)null)
//            {
//                throw new ArgumentNullException("fromType");
//            }

//            if (toType == (Type)null)
//            {
//                throw new ArgumentNullException("toType");
//            }

//            //// todo check errors if type already in container

//            if (registerAsSingleton)
//            {
//                builder.RegisterType(toType).As(fromType).SingleInstance();
//            }
//            else
//            {
//                builder.RegisterType(toType).As(fromType);
//            }
//        }

//        private ContainerBuilder Builder
//        {
//            get
//            {
//                return _builder ?? (_builder = new ContainerBuilder());
//            }
//        }

//        private IContainer Container { get; set; }
//    }


//}
