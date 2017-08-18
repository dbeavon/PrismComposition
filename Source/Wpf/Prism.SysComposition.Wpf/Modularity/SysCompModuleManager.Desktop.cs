

using System.Collections.Generic;
using System.ComponentModel.Composition;
using Prism.Modularity;

namespace Prism.SysComposition.Modularity
{
    /// <summary>    
    /// Component responsible for coordinating the modules' type loading and module initialization process. 
    /// </summary>
    /// <remarks>
    /// This allows the MefBootstrapper to provide this class as a default implementation.
    /// If another implementation is found, this export will not be used.
    /// </remarks>
    public partial class SysCompModuleManager : ModuleManager, IPartImportsSatisfiedNotification
    {
        // disable the warning that the field is never assigned to, and will always have its default value null
        // as it is imported by MEF
#pragma warning disable 0649
        // [Import]  NOT WORKING AS IMPORT
        public SysCompFileModuleTypeLoader _mefFileModuleTypeLoader ;
#pragma warning restore 0649

        //private IEnumerable<IModuleTypeLoader> mefTypeLoaders;

        /// <summary>
        /// Gets or sets the type loaders used by the module manager.
        /// </summary>
        public override IEnumerable<IModuleTypeLoader> ModuleTypeLoaders
        {
            get
            {
                return new List<IModuleTypeLoader>()
                                              {
                                                  this._mefFileModuleTypeLoader
                                              };
                //}

                //return this.mefTypeLoaders;
            }

            //set
            //{
            //    this.mefTypeLoaders = value;
            //}
        }
    }
}