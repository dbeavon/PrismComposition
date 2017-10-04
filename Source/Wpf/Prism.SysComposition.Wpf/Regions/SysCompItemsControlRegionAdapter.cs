using Prism.Regions;
using System.Composition;
namespace Prism.SysComposition.Regions
{
    /// <summary>
    /// Exports the ItemsControlRegionAdapter using the Managed Extensibility Framework (MEF).
    /// </summary>
    /// <remarks>
    /// This allows the SysCompBootstrapper to provide this class as a default implementation.
    /// If another implementation is found, this export will not be used.
    /// </remarks>
    [Export(typeof(ItemsControlRegionAdapter))]
    [Shared]
    public class SysCompItemsControlRegionAdapter : ItemsControlRegionAdapter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SysCompItemsControlRegionAdapter"/> class.
        /// </summary>
        /// <param name="regionBehaviorFactory">The factory used to create the region behaviors to attach to the created regions.</param>
        [ImportingConstructor]
        public SysCompItemsControlRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory) : base(regionBehaviorFactory)
        {
        }
    }
}