using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Practices.ServiceLocation;

namespace DemoApp.Infrastructure
{
    public class AutofacServiceLocator : IServiceLocator
    {
        private IComponentContext _container;

        public AutofacServiceLocator(IComponentContext container)
        {
            this._container = container;
        }

        public IEnumerable<TService> GetAllInstances<TService>()
        {
            return _container.Resolve<IEnumerable<TService>>();
        }

        public IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return new object[] { this._container.Resolve(serviceType) };
        }

        public TService GetInstance<TService>(string key)
        {
            return _container.ResolveKeyed<TService>(key);
        }

        public TService GetInstance<TService>()
        {
            return _container.Resolve<TService>();
        }

        public object GetInstance(Type serviceType, string key)
        {
            return _container.ResolveKeyed(key, serviceType);
        }

        public object GetInstance(Type serviceType)
        {
            return _container.Resolve(serviceType);
        }

        public object GetService(Type serviceType)
        {
            object serviceInstance;
            _container.TryResolve(serviceType, out serviceInstance);
            return serviceInstance;
        }
    }
}
