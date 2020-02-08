using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Gir
{
    public class GAliasResolver : AliasResolver
    {
        private IEnumerable<GAlias> aliases;

        public GAliasResolver(IEnumerable<GAlias> aliases)
        {
            this.aliases = aliases ?? throw new System.ArgumentNullException(nameof(aliases));
        }

        public bool TryGet(string type, [NotNullWhen(returnValue: true)] out string t)
        {
            var matching = aliases.Where(x => x.Type == type);

            if(matching.Count() != 1)
            {
                t = default!;
                return false;
            }

            t = matching.First().For.Name;
            return true;
        }
    }
}