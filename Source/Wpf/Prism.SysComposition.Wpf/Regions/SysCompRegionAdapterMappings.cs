using Prism.Regions;
using System.Composition;
namespace Prism.SysComposition.Regions
{
    /// <summary>
    /// Exports the RegionAdapterMappings using the Managed Extensibility Framework (MEF).
    /// </summary>
    /// <remarks>
    /// This allows the SysCompBootstrapper to provide this class as a default implementation.
    /// If another implementation is found, this export will not be used.
    /// </remarks>
    [Export(typeof(RegionAdapterMappings))]
    [Shared]

    public class SysCompRegionAdapterMappings : RegionAdapterMappings
    {
    }
}