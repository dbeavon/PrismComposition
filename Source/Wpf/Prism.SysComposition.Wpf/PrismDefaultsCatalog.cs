/* needs review (not good for SysComposition, nor seems to have any purpose)


using System;
using System.Collections.Generic;


using System.Linq;

namespace Prism.SysComposition
{
    internal class PrismDefaultsCatalog : System.ComponentModel.Composition.Primitives.ComposablePartCatalog
    {
        
        private readonly IEnumerable<System.ComponentModel.Composition.Primitives.ComposablePartDefinition> parts;

        public PrismDefaultsCatalog(IEnumerable<System.ComponentModel.Composition.Primitives.ComposablePartDefinition> parts)
        {
            if (parts == null) throw new ArgumentNullException(nameof(parts));
            this.parts = parts;
        }

        public override IQueryable<System.ComponentModel.Composition.Primitives.ComposablePartDefinition> Parts
        {
            get { return parts.AsQueryable(); }
        }
    }
}

*/