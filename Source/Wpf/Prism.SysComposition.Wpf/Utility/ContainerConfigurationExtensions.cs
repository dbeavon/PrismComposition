﻿using System;
using System.Collections.Generic;
using System.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prism.SysComposition.Utility
{
    /// <summary>
    /// Extensions for simplified configuration.
    /// </summary>
    public static class ContainerConfigurationExtensions
    {
        public static ContainerConfiguration WithExportInstance<T>(this ContainerConfiguration configuration, T exportedInstance, string contractName = null, IDictionary<string, object> metadata = null)
        {
            return WithExportInstance(configuration, exportedInstance, typeof(T), contractName, metadata);
        }

        public static ContainerConfiguration WithExportInstance(this ContainerConfiguration configuration, object exportedInstance, Type contractType, string contractName = null, IDictionary<string, object> metadata = null)
        {
            return configuration.WithProvider(new InstanceExportDescriptorProvider(
                exportedInstance, contractType, contractName, metadata));
        }

        public static ContainerConfiguration WithFactoryDelegate<T>(this ContainerConfiguration configuration, Func<T> exportedInstanceFactory, string contractName = null, IDictionary<string, object> metadata = null, bool isShared = false)
        {
            return WithFactoryDelegate(configuration, () => exportedInstanceFactory(), typeof(T), contractName, metadata, isShared);
        }

        public static ContainerConfiguration WithFactoryDelegate(this ContainerConfiguration configuration, Func<object> exportedInstanceFactory, Type contractType, string contractName = null, IDictionary<string, object> metadata = null, bool isShared = false)
        {
            return configuration.WithProvider(new DelegateExportDescriptorProvider(
                exportedInstanceFactory, contractType, contractName, metadata, isShared));
        }
    }
}
