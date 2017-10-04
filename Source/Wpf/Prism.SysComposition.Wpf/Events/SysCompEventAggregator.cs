using Prism.Events;
using System.Composition;

namespace Prism.SysComposition.Events
{
    /// <summary>
    /// Exports the EventAggregator using the Managed Extensibility Framework (MEF).
    /// </summary>
    /// <remarks>
    /// This allows the MefBootstrapper to provide this class as a default implementation.
    /// If another implementation is found, this export will not be used.
    /// </remarks>
    [Shared]
    [Export(typeof(IEventAggregator))]
    public class SysCompEventAggregator : EventAggregator
    {
    }
}