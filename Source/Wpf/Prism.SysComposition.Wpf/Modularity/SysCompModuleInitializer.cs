

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Globalization;
using System.Linq;
using Microsoft.Practices.ServiceLocation;
using Prism.Logging;
using Prism.Modularity;
using System.Composition.Hosting;
using Prism.SysComposition;

namespace Prism.SysComposition.Modularity
{
    /// <summary>
    /// Exports the ModuleInitializer using the Managed Extensibility Framework (MEF).
    /// </summary>
    /// <remarks>
    /// This allows the MefBootstrapper to provide this class as a default implementation.
    /// If another implementation is found, this export will not be used.
    /// </remarks>
    [Export(typeof(IModuleInitializer))]
    public class SysCompModuleInitializer : ModuleInitializer
    {
        [Obsolete(message:"Dont use this", error: false)]
        private DownloadedPartCatalogCollection downloadedPartCatalogs;

        private CompositionHost aggregateCatalog { get; set; }

        private CompositionHost _compositionHost { get; set; }

        // disable the warning that the field is never assigned to, and will always have its default value null
        // as it is imported by MEF
#pragma warning disable 0649

        /// <summary>
        /// Import the available modules from the MEF2 container
        /// </summary>
        [ImportMany]
        private IEnumerable<Lazy<IModule, IModuleExport>> ImportedModules { get; set; }
#pragma warning restore 0649

        /// <summary>
        /// Initializes a new instance of the <see cref="SysCompModuleInitializer"/> class.
        /// </summary>
        /// <param name="serviceLocator">The container that will be used to resolve the modules by specifying its type.</param>
        /// <param name="loggerFacade">The logger to use.</param>
        /// <param name="downloadedPartCatalogs">The downloaded part catalogs.</param>
        /// <param name="aggregateCatalog">The aggregate catalog.</param>
        [ImportingConstructor()]
        public SysCompModuleInitializer(IServiceLocator serviceLocator, ILoggerFacade loggerFacade) //, CompositionHost aggregateCatalog)
            : base(serviceLocator, loggerFacade)
        {
            //CompositionHost fromServiceLocator = ((Mef2ServiceLocator)serviceLocator).;

            CompositionHost fromServiceLocator = null;
            if(((SysCompServiceLocator)serviceLocator) != null)
            {
                fromServiceLocator = SysCompServiceLocator.Host;

            }

            //public SysCompModuleInitializer(IServiceLocator serviceLocator, ILoggerFacade loggerFacade, DownloadedPartCatalogCollection downloadedPartCatalogs, AggregateCatalog aggregateCatalog)

                //if (downloadedPartCatalogs == null)
                //{
                //    throw new ArgumentNullException(nameof(downloadedPartCatalogs));
                //}

                if (fromServiceLocator == null)
            {
                throw new ArgumentNullException(nameof(fromServiceLocator));
            }

            this.downloadedPartCatalogs = null;
            _compositionHost = fromServiceLocator;
            this.aggregateCatalog = fromServiceLocator;
        }

        /// <summary>
        /// Uses the container to resolve a new <see cref="IModule"/> by specifying its <see cref="Type"/>.
        /// </summary>
        /// <param name="moduleInfo">The module to create.</param>
        /// <returns>
        /// A new instance of the module specified by <paramref name="moduleInfo"/>.
        /// </returns>
        //protected override IModule CreateModule(ModuleInfo moduleInfo)
        //{
        //                                                // If there is a catalog that needs to be integrated with the AggregateCatalog as part of initialization, I add it to the container's catalog.
        //                                                //ComposablePartCatalog partCatalog;
        //                                                //if (this.downloadedPartCatalogs.TryGet(moduleInfo, out partCatalog))
        //                                                //{
        //                                                //    if (!this.aggregateCatalog.Catalogs.Contains(partCatalog))
        //                                                //    {
        //                                                //        this.aggregateCatalog.Catalogs.Add(partCatalog);
        //                                                //    }

        //                                                //    this.downloadedPartCatalogs.Remove(moduleInfo);
        //                                                //}

        //                                                //if (this.ImportedModules != null && this.ImportedModules.Count() != 0)
        //                                                //{
        //                                                //    Lazy<IModule, IModuleExport> lazyModule =
        //                                                //        this.ImportedModules.FirstOrDefault(x => (x.Metadata.ModuleName == moduleInfo.ModuleName));
        //                                                //    if (lazyModule != null)
        //                                                //    {
        //                                                //        return lazyModule.Value;
        //                                                //    }
        //                                                //}

        //                                                //UfpBootstrapModule


        //    try
        //    {
        //        Type ModuleType = Type.GetType(moduleInfo.ModuleType);
        //        if (ModuleType == null) { throw new Exception("Module type not available for that module"); }

        //        ModuleExportAttribute AttributeObj = ModuleType.GetCustomAttributes(typeof(ModuleExportAttribute), inherit: false).FirstOrDefault() as ModuleExportAttribute;
        //        if (AttributeObj == null) { throw new Exception("ModuleExportAttribute not available for that module"); }



        //        IEnumerable<IModule> Modules = _compositionHost.GetExports<IModule>();
        //        foreach (IModule LoopModule in Modules)
        //        {
        //            bool isModuleTypeInstance = ModuleType.IsInstanceOfType(LoopModule);
        //            bool isAttributeTypeInstance = AttributeObj.ModuleType.IsInstanceOfType(LoopModule);
        //            if (isModuleTypeInstance && isAttributeTypeInstance) { return LoopModule; }

        //        }

        //        // Not found
        //        throw new Exception("The module was not found");
        //    }
        //    catch (Exception Ex)
        //    {

        //        // This does not fall back to the base implementation because the type must be in the MEF container and not just in the application domain.
        //        throw new ModuleInitializeException(
        //            string.Format(CultureInfo.CurrentCulture, Properties.Resources.FailedToGetType, moduleInfo.ModuleType), Ex);
        //    }

 
        //}
    }
}