using System;
using Microsoft.Practices.ServiceLocation;
using Prism.Regions;
using System.Composition;

namespace Prism.SysComposition.Regions
{
    /// <summary>
    /// Exports the RegionBehaviorFactory using the Managed Extensibility Framework (MEF).
    /// </summary>
    /// <remarks>
    /// This allows the SysCompBootstrapper to provide this class as a default implementation.
    /// If another implementation is found, this export will not be used.
    /// </remarks>
    [Export(typeof(IRegionBehaviorFactory))]
    [Shared]

    public class SysCompRegionBehaviorFactory : RegionBehaviorFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SysCompRegionBehaviorFactory"/> class.
        /// </summary>
        /// <param name="serviceLocator"><see cref="IServiceLocator"/> used to create the instance of the behavior from its <see cref="Type"/>.</param>
        [ImportingConstructor]
        public SysCompRegionBehaviorFactory(IServiceLocator serviceLocator)
            : base(serviceLocator)
        {
        }
    }
}