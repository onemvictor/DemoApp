using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Interfaces
{
    public interface IAdapterModuleRegistry
    {
        void RegisterAdapterModule(IAdapterModule module);
        void UnRegisterAdapterModule(IAdapterModule module);
    }
}
