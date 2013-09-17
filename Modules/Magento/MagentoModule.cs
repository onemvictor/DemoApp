using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Magento.Model;
using DemoApp.Infrastructure;
using Microsoft.Practices.Prism.Modularity;
using DemoApp.Interfaces;

namespace Magento
{
    [AdapterModuleMarker(guid:"BC66623F-7B3D-42B6-A24C-F3B3C4EADC12",name:"Magento",version:"1.0.0")]    
    class MagentoModule:Module,IModule,IAdapterModule
    {
        public MagentoModule() { }

        public MagentoModule(IAdapterModuleRegistry adapterModuleRegistry)
        {
            adapterModuleRegistry.RegisterAdapterModule(this);
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MagentoConfig>();
            builder.RegisterType<MagentoModule>().AsSelf().As<IModule>();
        }



        public void Initialize()
        {
            
        }

        public Guid ID
        {
            get { return Guid.Parse("BC66623F-7B3D-42B6-A24C-F3B3C4EADC12"); }
        }

        public string Name
        {
            get { return "Magento"; }
        }

        public string Version
        {
            get { return "1.0.0"; }
        }
    }
}
