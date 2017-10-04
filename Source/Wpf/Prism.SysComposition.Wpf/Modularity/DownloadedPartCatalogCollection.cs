using System.Collections.Generic;
using System.Composition;

using Prism.Modularity;

namespace Prism.SysComposition.Modularity

{
    /// <summary>
    /// Holds a collection of composable part catalogs keyed by module info.
    /// Currently this feature of modularity is not implemented.
    /// </summary>
    [Shared, Export]
    public class DownloadedPartCatalogCollection
    {
        /*
        private Dictionary<ModuleInfo, System.ComponentModel.Composition.Primitives.ComposablePartCatalog> catalogs = new Dictionary<ModuleInfo, System.ComponentModel.Composition.Primitives.ComposablePartCatalog>();
        

        public void Add(ModuleInfo moduleInfo, System.ComponentModel.Composition.Primitives.ComposablePartCatalog catalog)
        {
            catalogs.Add(moduleInfo, catalog);
        }

        public System.ComponentModel.Composition.Primitives.ComposablePartCatalog Get(ModuleInfo moduleInfo)
        {
            return this.catalogs[moduleInfo];
        }

        public bool TryGet(ModuleInfo moduleInfo, out System.ComponentModel.Composition.Primitives.ComposablePartCatalog catalog)
        {
            return this.catalogs.TryGetValue(moduleInfo, out catalog);
        }

        public void Remove(ModuleInfo moduleInfo)
        {
            this.catalogs.Remove(moduleInfo);
        }

        public void Clear()
        {
            this.catalogs.Clear();
        }
        */
    }
}
