using System;
using System.Collections.Generic;

using System.Linq;

using Microsoft.Practices.ServiceLocation;
using Prism.Regions;
using System.Composition;

namespace Prism.SysComposition.Regions
{
    /// <summary>
    /// Exports the LocatorNavigationTargetHandler using the Managed Extensibility Framework (MEF).
    /// </summary>
    /// <remarks>
    /// This allows the SysCompBootstrapper to provide this class as a default implementation.
    /// If another implementation is found, this export will not be used.
    /// </remarks>
    [Export(typeof(IRegionNavigationContentLoader))]
    [Shared]

    public class SysCompRegionNavigationContentLoader : RegionNavigationContentLoader
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SysCompRegionNavigationContentLoader"/> class.
        /// </summary>
        /// <param name="serviceLocator"><see cref="IServiceLocator"/> used to create the instance of the view from its <see cref="Type"/>.</param>
        [ImportingConstructor]
        public SysCompRegionNavigationContentLoader(IServiceLocator serviceLocator)
            : base(serviceLocator)
        {
        }

        /// <summary>
        /// Returns the set of candidates that may satisfiy this navigation request.
        /// </summary>
        /// <param name="region">The region containing items that may satisfy the navigation request.</param>
        /// <param name="candidateNavigationContract">The candidate navigation target.</param>
        /// <returns>An enumerable of candidate objects from the <see cref="IRegion"/></returns>
        protected override IEnumerable<object> GetCandidatesFromRegion(IRegion region, string candidateNavigationContract)
        {
            if (candidateNavigationContract == null || candidateNavigationContract.Equals(string.Empty))
                throw new ArgumentNullException(nameof(candidateNavigationContract));

            IEnumerable<object> contractCandidates = base.GetCandidatesFromRegion(region, candidateNavigationContract);

            if (!contractCandidates.Any())
            {
                contractCandidates = region.Views.Where(v => candidateNavigationContract.Equals(v.GetType().GetCustomAttributes(false).OfType<ExportAttribute>().FirstOrDefault().ContractName, StringComparison.Ordinal));
            }

            return contractCandidates;
        }
    }
}
