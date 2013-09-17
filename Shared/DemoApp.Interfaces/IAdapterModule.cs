using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Interfaces
{
    public interface IAdapterModule
    {
        Guid ID { get; }

        string Name { get; }

        string Version { get; }
    }
}
