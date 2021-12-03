namespace Generator3.Model.Public
{
    public class Property
    {
        private readonly GirModel.Property _property;

        public string ClassName { get; }
        public string NativeName => _property.NativeName;
        public string ManagedName => _property.ManagedName;
        public string DescriptorName => ManagedName + "Property";
        public string NullableTypeName => _property.AnyType.Match(
            type => type.GetName() + GetDefaultNullable(),
            arrayType => arrayType.GetName()
        );

        public bool Readable => _property.Readable;
        public bool Writeable => _property.Writeable;

        public bool IsPrimitiveType => _property.AnyType.TryPickT0(out var type, out _) && type is GirModel.PrimitiveType;

        public Property(GirModel.Property property, string className)
        {
            _property = property;
            ClassName = className;
        }

        // Properties need special nullable handling as each C value is implicitly nullable.
        // The C# Marshaller returns default values for primitive value types. But there
        // can be null values for strings and objects, even if the nullability attribute
        // is not present in the gir file
        protected string GetDefaultNullable()
        {
            if (!_property.AnyType.TryPickT0(out var type, out _))
                return string.Empty;

            switch (type)
            {
                case GirModel.String:
                case GirModel.Class:
                    return "?";
                default:
                    return string.Empty;
            }
        }
    }
}
