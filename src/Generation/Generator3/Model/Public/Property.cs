using System;
using Generator3.Converter;

namespace Generator3.Model.Public
{
    public class Property
    {
        private readonly GirModel.Property _property;

        public string ClassName { get; }
        public string NativeName => _property.Name;
        public string PublicName => GetPublicName();
        public string DescriptorName => PublicName + "PropertyDefinition";
        public string NullableTypeName => GetNullableTypeName();

        public bool Readable => _property.Readable;
        public bool Writeable => _property.Writeable;

        public GirModel.AnyType AnyType => _property.AnyType;

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

        private string GetPublicName()
        {
            var name = _property.GetPublicName();

            if (name == ClassName)
                throw new Exception($"Property {name} is identical to it's class which is not yet supported. Property should be created with a suffix and in a separate build step be rewritten to it's original name");

            return name;
        }

        private string GetNullableTypeName()
        {
            return _property.AnyType.Match(
                type => type switch
                {
                    GirModel.ComplexType c => c.GetFullyQualified() + GetDefaultNullable(),
                    _ => type.GetName() + GetDefaultNullable()
                },
                arrayType => arrayType.GetName()
            );
        }
    }
}
