using System;
using System.Collections.Generic;
using System.Composition.Hosting.Core;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Prism.SysComposition.Utility
{
    /// <summary>
    /// ExportDescriptorProvider that allows us to immitate ComposeExportedPart for an existing instance.
    /// 
    /// This one-instance-per-provider design is not efficient for more than a few instances;
    /// we're just aiming to show the mechanics here. 
    /// </summary>
    public class InstanceExportDescriptorProvider : SinglePartExportDescriptorProvider
    {
        object _exportedInstance;

        /// <summary>
        /// Construction of a descriptor provider (ExportDescriptorProvider)
        /// that only exports the same shared part every time.
        /// </summary>
        /// <param name="exportedInstance">The part to export.</param>
        /// <param name="contractType">The type of the part.</param>
        /// <param name="contractName">The name to use as alias for this part</param>
        /// <param name="metadata">Optional metadata</param>
        public InstanceExportDescriptorProvider(
            object exportedInstance, 
            Type contractType, 
            string contractName, 
            IDictionary<string, object> metadata) : 
            base(contractType, contractName, metadata)
        {
            if (exportedInstance == null) { throw new ArgumentNullException("exportedInstance"); }

            Contract.EndContractBlock();

            _exportedInstance = exportedInstance;
        }

        /// <summary>
        /// Gets the export descriptor that returns our instance.
        /// </summary>
        private ExportDescriptor GetInstanceExportDescriptor(IEnumerable<CompositionDependency> _dependencies)
        {
            // Returns the instance, given the context
            // (currently ignores the context and the operation)
            CompositeActivator ActivatorDelegate = (context, operation) => _exportedInstance;

            // Return the descriptor
            return ExportDescriptor.Create(ActivatorDelegate, Metadata);
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
            if (IsSupportedContract(contract))
            {
                yield return new ExportDescriptorPromise(
                    contract,
                    origin: _exportedInstance.ToString(),
                    isShared: true,
                    dependencies: NoDependencies,
                    getDescriptor: new Func<IEnumerable<CompositionDependency>, ExportDescriptor>(GetInstanceExportDescriptor));
            }
        }
    }
}
