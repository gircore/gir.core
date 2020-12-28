using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gir;

namespace Generator
{
    public class AliasResolver
    {
        #region Fields

        private readonly IEnumerable<GAlias> _aliases;

        #endregion

        #region Constructors

        public AliasResolver(IEnumerable<GAlias> aliases)
        {
            _aliases = aliases ?? throw new ArgumentNullException(nameof(aliases));
        }

        #endregion

        #region Methods

        public bool TryGetForCType(string cType, [NotNullWhen(returnValue: true)] out string? t, out string? n)
            => TryGet(cType, (a, t) => a.Type == t || a.Name == t, out t, out n);

        private bool TryGet(string typeName, Func<GAlias, string, bool> predicate, [NotNullWhen(returnValue: true)] out string? t, out string? n)
        {
            GAlias? matching = _aliases.FirstOrDefault(x => predicate(x, typeName));

            if (matching is null || matching.For?.CType is null)
            {
                t = default;
                n = default;
                return false;
            }

            t = matching.For.CType;
            n = matching.For?.Name;
            return true;
        }

        #endregion
    }
}
