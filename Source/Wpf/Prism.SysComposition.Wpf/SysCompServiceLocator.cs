﻿using System;
using System.Collections.Generic;
using System.Composition.Hosting;
using Microsoft.Practices.ServiceLocation;

namespace Prism.SysComposition
{
    public class SysCompServiceLocator : ServiceLocatorImplBase
    {
        public static CompositionHost Host;

        protected override object DoGetInstance(Type serviceType, string key)
        {
            return Host.GetExport(serviceType, key);
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            return Host.GetExports(serviceType);
        }
    }
}



