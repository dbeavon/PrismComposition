using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

using Prism.Events;
using Prism.Regions;
using Prism.Modularity;
using Prism.Logging;

using Prism.SysComposition;
using Microsoft.Practices.ServiceLocation;

using System.Composition.Hosting.Core;
using System.Composition.Convention;
using System.Composition.Hosting;

namespace Prism.SysComposition.Utility
{

    /// <summary>
    /// ExportDescriptorProvider that allows us to immitate ComposeExportedPart for an existing instance.
    /// </summary>
    public class BaseInstanceExportDescriptorProvider<T> : ExportDescriptorProvider
    {
        readonly object _instance;
        readonly string _instanceContractName;
        readonly bool _enforceContractNameMatch;

        public BaseInstanceExportDescriptorProvider(
            object instance,
            string instanceContractName)
        {
            _instance = instance;
            _instanceContractName = instanceContractName;
            _enforceContractNameMatch = true;
        }

        public BaseInstanceExportDescriptorProvider(
            object instance)
        {
            _instance = instance;
            _enforceContractNameMatch = false;

        }

        /// <summary>
        /// Gets the export descriptor that returns our instance.
        /// </summary>
        private ExportDescriptor GetInstanceExportDescriptor(IEnumerable<CompositionDependency> p_Dependencies)
        {
            // Returns the instance, given the context
            // (currently ignores the context and the operation)
            CompositeActivator ActivatorDelegate = (context, operation) => _instance;

            // Return the descriptor
            return ExportDescriptor.Create(ActivatorDelegate, NoMetadata);
        }

        /// <summary>
        /// Overridden method to implement base class requirements.
        /// </summary>
        /// <param name="contract">Contract containing the contract name.</param>
        /// <param name="descriptorAccessor">Ignored</param>
        /// <returns>A map that points to our instance</returns>
        public override IEnumerable<ExportDescriptorPromise> GetExportDescriptors(
            CompositionContract contract,
            DependencyAccessor descriptorAccessor)
        {
            if ((contract.ContractType == typeof(T))
                && (contract.ContractName == _instanceContractName || _enforceContractNameMatch == false))
            {

                yield return new ExportDescriptorPromise(
                    contract,
                    origin: typeof(T).FullName,
                    isShared: true,
                    dependencies: NoDependencies,
                    getDescriptor: new Func<IEnumerable<CompositionDependency>, ExportDescriptor>(GetInstanceExportDescriptor));




            }
        }

    }

}
