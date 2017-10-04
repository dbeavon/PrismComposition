using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Composition;

using Microsoft.Practices.ServiceLocation;

using Prism.Logging;
using Prism.Modularity;

namespace Prism.SysComposition.Modularity
{
    /// <summary>
    /// Exports the ModuleInitializer using the Managed Extensibility Framework (MEF).
    /// </summary>
    /// <remarks>
    /// This allows the SysCompBootstrapper to provide this class as a default implementation.
    /// If another implementation is found, this export will not be used.
    /// </remarks>
    [Shared]
    [Export(typeof(IModuleInitializer))]
    public class SysCompModuleInitializer : ModuleInitializer
    {
        private DownloadedPartCatalogCollection downloadedPartCatalogs;


// disable the warning that the field is never assigned to, and will always have its default value null
// as it is imported by MEF
#pragma warning disable 0649

        /// <summary>
        /// Import the available modules from the MEF container
        /// </summary>
        [ImportMany()]
        public IEnumerable<Lazy<IModule, ModuleExportAttribute>> ImportedModules { get; set; }



#pragma warning restore 0649

        /// <summary>
        /// Initializes a new instance of the <see cref="SysCompModuleInitializer"/> class.
        /// </summary>
        /// <param name="serviceLocator">The container that will be used to resolve the modules by specifying its type.</param>
        /// <param name="loggerFacade">The logger to use.</param>
        /// <param name="downloadedPartCatalogs">The downloaded part catalogs.</param>
        [ImportingConstructor()]
        public SysCompModuleInitializer(
            IServiceLocator serviceLocator, 
            ILoggerFacade loggerFacade, 
            DownloadedPartCatalogCollection downloadedPartCatalogs)
            : base(serviceLocator, loggerFacade)
        {
            if (downloadedPartCatalogs == null)
            {
                throw new ArgumentNullException(nameof(downloadedPartCatalogs));
            }
             

            this.downloadedPartCatalogs = downloadedPartCatalogs;
        }

        /// <summary>
        /// Uses the container to resolve a new <see cref="IModule"/> by specifying its <see cref="Type"/>.
        /// </summary>
        /// <param name="moduleInfo">The module to create.</param>
        /// <returns>
        /// A new instance of the module specified by <paramref name="moduleInfo"/>.
        /// </returns>
        protected override IModule CreateModule(ModuleInfo moduleInfo)
        {

            if (this.ImportedModules != null && this.ImportedModules.Count() != 0)
            {
                Lazy<IModule, ModuleExportAttribute> lazyModule =
                    this.ImportedModules.FirstOrDefault(x => (x.Metadata.ModuleName == moduleInfo.ModuleName));
                if (lazyModule != null)
                {
                    return lazyModule.Value;
                }
            }

            // This does not fall back to the base implementation because the type must be in the MEF container and not just in the application domain.
            throw new ModuleInitializeException(
                string.Format(CultureInfo.CurrentCulture, Properties.Resources.FailedToGetType, moduleInfo.ModuleType));
        }
    }
}