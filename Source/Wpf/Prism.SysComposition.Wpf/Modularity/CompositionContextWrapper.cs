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
using System.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Collections.ObjectModel;

namespace Prism.SysComposition.Modularity
{

    /// <summary>
    /// Allows a simple way to generate an export descriptior based on delegating to a context .
    /// </summary>
    internal class CompositionContextContractWrapper
    {



        /// <summary>
        /// Constant value provided so that subclasses can avoid creating additional duplicate values.
        /// </summary>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        protected static readonly IDictionary<string, object> NoMetadata = new ReadOnlyDictionary<string, object>(new Dictionary<string, object>());

        /// <summary>
        /// The context and contract.
        /// </summary>
        private CompositionContext _wrappedContext;
        private CompositionContract _contract;

        /// <summary>
        /// Construct the contract wrapper.
        /// </summary>
        public CompositionContextContractWrapper(CompositionContext wrappedContext, CompositionContract contract)
        {
            _wrappedContext = wrappedContext;
            _contract = contract;
        }

        /// <summary>
        /// Gets the export descriptor that delegates to the wrapped context.
        /// </summary>
        internal ExportDescriptor GetInstanceExportDescriptor(IEnumerable<CompositionDependency> dependencies)
        {
            // Returns the instance, given the context
            // (currently ignores the context and the operation)
            CompositeActivator ActivatorDelegate = (context, operation) => _wrappedContext.GetExport(_contract);

            // Return the descriptor
            return ExportDescriptor.Create(ActivatorDelegate, NoMetadata);
        }

        /// <summary>
        /// Create an instance.  Use the specified context and contract.
        /// </summary>
        public static CompositionContextContractWrapper NewContractHelper(CompositionContext wrappedContext, CompositionContract contract)
        {
            return new CompositionContextContractWrapper(wrappedContext, contract);

        }

    }


    /// <summary>
    /// 
    /// For very large or unusual modules, this allows us 
    /// to create an independent CompositionContext (ie typically a CompositionHost)
    /// and use it to delegate some of our CompositionContracts.
    /// 
    /// This prevents us from forcing the app to preload all assemblies for MEF2 type registration.
    /// Instead, they can be lazy-loaded when the module is initialized.
    /// 
    /// </summary>
    /// <typeparam name="T">The type of the module that will generate its own CompositionContext</typeparam>
    public class CompositionContextWrapper<T> : ExportDescriptorProvider where T: class
    {
        /// <summary>
        /// should be readonly .
        /// </summary>
        object _moduleInstance;

        private CompositionContext _wrappedContext;

        readonly string _xxx;
        readonly bool _yyy;

        /// <summary>
        /// Get the related prism module.
        /// </summary>
        public T PrismModule {  get { return _moduleInstance as T; } 
            
            // debug
            set {  _moduleInstance = value; }
            }

        /// <summary>
        /// THe wrapped context (may be null)
        /// </summary>
        public CompositionContext WrappedContext {  get {  return _wrappedContext; } }


        /// <summary>
        /// Construction.  The wrapped context remains null for now.
        /// </summary>
        public CompositionContextWrapper(
            object instance)
        {
            _moduleInstance = instance;
            _wrappedContext = null;
            _yyy = false;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wrappedContext"></param>
        public void SaveCompositionContext(CompositionContext wrappedContext)
        {
            if(_wrappedContext != null) {  throw new InvalidOperationException(); }

            // Save the context for exporting descriptors
            _wrappedContext = wrappedContext;

        }


        
        /// <summary>
        /// Overridden method to implement base class requirements.
        /// </summary>
        /// <param name="contract">Contract for the dependency we want.</param>
        /// <param name="descriptorAccessor">Ignored</param>
        public override IEnumerable<ExportDescriptorPromise> GetExportDescriptors(
            CompositionContract contract,
            DependencyAccessor descriptorAccessor)
        {
            
            // Nothing to do
            if (WrappedContext == null) { yield break; }

            // Slow.  Need to revisit this.
            if(!WrappedContext.TryGetExport(contract, out object ExportObject))
            {
                yield break;
            }

            //protected static readonly IEnumerable<ExportDescriptorPromise> NoExportDescriptors;


            ////if
            //if (contract.)
            //if ((contract.ContractType == typeof(T))
            //    && (contract.ContractName == _xxx || _yyy == false))
            //{

            // Create a helpful context wrapper instance.
            CompositionContextContractWrapper contractWrapper = CompositionContextContractWrapper.NewContractHelper(_wrappedContext, contract);

            // Return the promise
            yield return new ExportDescriptorPromise(
                    contract,
                    origin: typeof(T).FullName,
                    isShared: false,
                    dependencies: NoDependencies,
                    getDescriptor: new Func<IEnumerable<CompositionDependency>, ExportDescriptor>(contractWrapper.GetInstanceExportDescriptor));


              
            //    //public ExportDescriptorPromise(CompositionContract contract, string origin, bool isShared, Func<IEnumerable<CompositionDependency>> dependencies, Func<IEnumerable<CompositionDependency>, ExportDescriptor> getDescriptor);



            //}
        }

    }

}