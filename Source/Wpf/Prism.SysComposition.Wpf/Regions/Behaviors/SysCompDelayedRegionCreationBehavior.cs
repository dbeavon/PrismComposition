using Prism.Regions;
using Prism.Regions.Behaviors;
using System.Composition;
namespace Prism.SysComposition.Regions.Behaviors
{
    /// <summary>
    /// Exports the DelayedRegionCreationBehavior using the Managed Extensibility Framework (MEF).
    /// </summary>
    /// <remarks>
    /// This allows the SysCompBootstrapper to provide this class as a default implementation.
    /// If another implementation is found, this export will not be used.
    /// </remarks>
    [Export(typeof(DelayedRegionCreationBehavior))]
    
    public class SysCompDelayedRegionCreationBehavior : DelayedRegionCreationBehavior
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SysCompDelayedRegionCreationBehavior"/> class.
        /// </summary>
        /// <param name="regionAdapterMappings">The region adapter mappings, that are used to find the correct adapter for
        /// a given controltype. The controltype is determined by the <see name="TargetElement"/> value.</param>
        [ImportingConstructor]
        public SysCompDelayedRegionCreationBehavior(RegionAdapterMappings regionAdapterMappings)
            : base(regionAdapterMappings)
        {
        }
    }
}