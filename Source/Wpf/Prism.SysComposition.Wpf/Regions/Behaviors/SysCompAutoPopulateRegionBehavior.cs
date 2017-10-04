using Prism.Regions;
using Prism.Regions.Behaviors;
using System.Composition;

namespace Prism.SysComposition.Regions.Behaviors
{
    /// <summary>
    /// Exports the AutoPopulateRegionBehavior using the Managed Extensibility Framework (MEF).
    /// </summary>
    /// <remarks>
    /// This allows the SysCompBootstrapper to provide this class as a default implementation.
    /// If another implementation is found, this export will not be used.
    /// </remarks>
    [Export(typeof(AutoPopulateRegionBehavior))]
    
    public class SysCompAutoPopulateRegionBehavior : AutoPopulateRegionBehavior
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SysCompAutoPopulateRegionBehavior"/> class.
        /// </summary>
        /// <param name="regionViewRegistry"><see cref="IRegionViewRegistry"/> that the behavior will monitor for views to populate the region.</param>
        [ImportingConstructor]
        public SysCompAutoPopulateRegionBehavior(IRegionViewRegistry regionViewRegistry)
            : base(regionViewRegistry)
        {
        }
    }
}