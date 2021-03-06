using Prism.Regions.Behaviors;
using System.Composition;
namespace Prism.SysComposition.Regions.Behaviors
{
    /// <summary>
    /// Exports the ClearChildViewsRegionBehavior using the Managed Extensibility Framework (MEF).
    /// </summary>
    /// <remarks>
    /// This allows the SysCompBootstrapper to provide this class as a default implementation.
    /// If another implementation is found, this export will not be used.
    /// </remarks>
    [Export(typeof(ClearChildViewsRegionBehavior))]
    
    public class SysCompClearChildViewsRegionBehavior : ClearChildViewsRegionBehavior
    {
    }
}