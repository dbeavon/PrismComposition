using Microsoft.Practices.ServiceLocation;
using Prism.Regions;
using System.Composition;

namespace Prism.SysComposition.Regions
{
    /// <summary>
    /// Exports the SysCompRegionNavigationService using the Managed Extensibility Framework (MEF).
    /// </summary>
    /// <remarks>
    /// This allows the SysCompBootstrapper to provide this class as a default implementation.
    /// If another implementation is found, this export will not be used.
    /// </remarks>
    [Export(typeof(IRegionNavigationService))]
    
    public class SysCompRegionNavigationService : RegionNavigationService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SysCompRegionNavigationService"/> class.
        /// </summary>
        /// <param name="serviceLocator">The service locator.</param>
        /// <param name="navigationContentLoader">The navigation content loader.</param>
        /// <param name="journal">The navigation journal.</param>
        [ImportingConstructor]
        public SysCompRegionNavigationService(IServiceLocator serviceLocator, IRegionNavigationContentLoader navigationContentLoader, IRegionNavigationJournal journal)
            : base(serviceLocator, navigationContentLoader, journal)
        { }
    }
}
