using System;
using CWrapper;

namespace Gir
{
    public class GirEnumFieldAdapter : EnumField
    {
        private readonly GMember enumField;
        private readonly IdentifierFixer identifierFixer;

        public string Name => identifierFixer.Fix(enumField.Name);

        public string Value => enumField.Value;

        public GirEnumFieldAdapter(GMember enumField, IdentifierFixer identifierFixer)
        {
            this.enumField = enumField ?? throw new ArgumentNullException(nameof(enumField));
            this.identifierFixer = identifierFixer ?? throw new ArgumentNullException(nameof(identifierFixer));
        }
    }
}