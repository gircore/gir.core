using System;
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

        public bool TryGetForCType(string cType, [NotNullWhen(returnValue: true)] out string? t)
            => TryGet(cType, (a, t) => a.Type == t, out t);

        private bool TryGet(string typeName, Func<GAlias, string, bool> predicate, [NotNullWhen(returnValue: true)] out string? t)
        {
            var matching = aliases.FirstOrDefault(x => predicate(x, typeName));

            if(matching is null || matching.For?.CType is null)
            {
                t = default;
                return false;
            }

            t = matching.For.CType;
            return true;
        }
    }
}