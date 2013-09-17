using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoApp.Interfaces;
using UserControls.ViewModel;

namespace UserControls.Service
{
    public class ModuleMenuRegistry:IAdapterModuleRegistry
    {
        private MainMenuViewModel _mainMenuVm;

        public ModuleMenuRegistry(MainMenuViewModel vm)
        {
            _mainMenuVm = vm;
        }
        public void RegisterAdapterModule(IAdapterModule module)
        {
            if (_mainMenuVm.AdapterModules.SingleOrDefault(m => m.GetType() == module.GetType()) == null)
            {
                _mainMenuVm.AdapterModules.Add(module);
            }
        }


        public void UnRegisterAdapterModule(IAdapterModule module)
        {
            var registeredModule=_mainMenuVm.AdapterModules.SingleOrDefault(m => m.GetType() == module.GetType());
            if (registeredModule == null)
            {
                _mainMenuVm.AdapterModules.Add(registeredModule);
            }
        }
    }
}
