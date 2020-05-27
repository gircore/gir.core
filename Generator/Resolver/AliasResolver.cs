using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gir;

namespace Generator
{
    public class AliasResolver
    {
        private IEnumerable<GAlias> aliases;

        public AliasResolver(IEnumerable<GAlias> aliases)
        {
            this.aliases = aliases ?? throw new System.ArgumentNullException(nameof(aliases));
        }

        public bool TryGet(string type, [NotNullWhen(returnValue: true)] out string? t)
        {
            var matching = aliases.FirstOrDefault(x => x.Type == type);

            if(matching is null || matching.For?.Name is null)
            {
                t = default;
                return false;
            }

            t = matching.For.Name;
            return true;
        }
    }
}