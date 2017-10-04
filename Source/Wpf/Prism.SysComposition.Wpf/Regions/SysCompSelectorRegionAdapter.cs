using Prism.Regions;
using System.Composition;

namespace Prism.SysComposition.Regions
{
    /// <summary>
    /// Exports the SelectorRegionAdapter using the Managed Extensibility Framework (MEF).
    /// </summary>
    /// <remarks>
    /// This allows the SysCompBootstrapper to provide this class as a default implementation.
    /// If another implementation is found, this export will not be used.
    /// </remarks>
    [Export(typeof(SelectorRegionAdapter))]
    [Shared]

    public class SysCompSelectorRegionAdapter : SelectorRegionAdapter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SysCompSelectorRegionAdapter"/> class.
        /// </summary>
        /// <param name="regionBehaviorFactory">The factory used to create the region behaviors to attach to the created regions.</param>
        [ImportingConstructor]
        public SysCompSelectorRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory) : base(regionBehaviorFactory)
        {
        }
    }
}