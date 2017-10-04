using System.Collections.Generic;
using System.Composition;
using Prism.Modularity;

namespace Prism.SysComposition.Modularity
{
    /// <summary>    
    /// Component responsible for coordinating the modules' type loading and module initialization process. 
    /// </summary>
    /// <remarks>
    /// This allows the SysCompBootstrapper to provide this class as a default implementation.
    /// If another implementation is found, this export will not be used.
    /// </remarks>
    public partial class SysCompModuleManager : ModuleManager 
    {


        // disable the warning that the field is never assigned to, and will always have its default value null
        // as it is imported by MEF
#pragma warning disable 0649
        [Import()] 
        public SysCompFileModuleTypeLoader MefFileModuleTypeLoader { get; set; }
#pragma warning restore 0649



        private IEnumerable<IModuleTypeLoader> _mefTypeLoaders;

        /// <summary>
        /// Gets or sets the type loaders used by the module manager.
        /// </summary>
        public override IEnumerable<IModuleTypeLoader> ModuleTypeLoaders
        {
            get
            {
                if (this._mefTypeLoaders == null)
                {
                    this._mefTypeLoaders = new List<IModuleTypeLoader>()
                                              {
                                                  this.MefFileModuleTypeLoader
                                              };
                }

                return this._mefTypeLoaders;
            }

            set
            {
                this._mefTypeLoaders = value;
            }
        }
    }
}