using Microsoft.Practices.ServiceLocation;
using Prism.Regions;
using System.Composition;

namespace Prism.SysComposition.Regions
{
    /// <summary>
    /// Exports the RegionViewRegistry using the Managed Extensibility Framework (MEF).
    /// </summary>
    /// <remarks>
    /// This allows the SysCompBootstrapper to provide this class as a default implementation.
    /// If another implementation is found, this export will not be used.
    /// </remarks>
    [Export(typeof(IRegionViewRegistry))]
    [Shared]

    public class SysCompRegionViewRegistry : RegionViewRegistry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SysCompRegionViewRegistry"/> class.
        /// </summary>
        /// <param name="serviceLocator">The service locator.</param>
        [ImportingConstructor]
        public SysCompRegionViewRegistry(IServiceLocator serviceLocator) : base(serviceLocator)
        {
        }
    }
}