using System.Collections.Generic;
using CWrapper;
using Enum = CWrapper.Enum;

namespace Gir
{
    public class GirEnumAdapter : GirAdapterBase, Enum
    {
        private readonly GEnumeration enumeration;

        public string Name => FixName(enumeration.Name);
        public bool HasFlags { get; }
        public IEnumerable<EnumField> Fields => GetFields(enumeration.Members);

        public GirEnumAdapter(GEnumeration enumeration, bool hasFlags, TypeResolver typeResolver, IdentifierFixer identifierFixer) : base(typeResolver, identifierFixer)
        {
            this.enumeration = enumeration ?? throw new System.ArgumentNullException(nameof(enumeration));

            HasFlags = hasFlags;
        }
    }
}