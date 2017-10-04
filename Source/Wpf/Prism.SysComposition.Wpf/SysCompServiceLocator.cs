using System;
using System.Collections.Generic;
using System.Composition.Hosting;
using System.Composition;

using Microsoft.Practices.ServiceLocation;

namespace Prism.SysComposition
{
    public class SysCompServiceLocator : ServiceLocatorImplBase
    {

        private CompositionHost _host;

        public void AssignCompositionHost(CompositionHost host)
        {
            _host = host;
        }

        public SysCompServiceLocator(CompositionHost host) : this()
        {
            _host = host;
        }

        public SysCompServiceLocator()
        {
            _host = null;
        }

        protected override object DoGetInstance(Type serviceType, string key)
        {
            return _host.GetExport(serviceType, key);
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            return _host.GetExports(serviceType);
        }
    }
}



