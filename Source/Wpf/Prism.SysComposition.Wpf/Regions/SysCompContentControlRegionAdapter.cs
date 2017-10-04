using Prism.Regions;
using System.Composition; 

namespace Prism.SysComposition.Regions
{
    /// <summary>
    /// Exports the ContentControlRegionAdapter using the Managed Extensibility Framework (MEF).
    /// </summary>
    /// <remarks>
    /// This allows the <see cref="SysCompBootstrapper.ConfigureContainer" /> to provide this class as a default implementation.
    /// If another implementation is found, this export will not be used.
    /// </remarks>
    [Export(typeof(ContentControlRegionAdapter))]
    [Shared]

    public class SysCompContentControlRegionAdapter : ContentControlRegionAdapter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SysCompContentControlRegionAdapter"/> class.
        /// </summary>
        /// <param name="regionBehaviorFactory">The region behavior factory.</param>
        [ImportingConstructor]
        public SysCompContentControlRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory)
            : base(regionBehaviorFactory)
        {
        }
    }
}