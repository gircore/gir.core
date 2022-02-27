using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Generator3.Converter;
using Generator3.Model.Internal;

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
        public GirModel.Method? Getter { get; }
        public GirModel.Method? Setter { get; }
        public bool Readable => _property.Readable;
        public bool Writeable => _property.Writeable;
        public bool ConstructOnly => _property.ConstructOnly;
        public bool Accessible => Readable || Writeable && !ConstructOnly;

        public GirModel.AnyType AnyType => _property.AnyType;

        public Property(GirModel.Property property, string className)
        {
            _property = property;
            ClassName = className;

            Getter = _property.Getter;
            Setter = _property.Setter;
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

        public bool SupportsAccessorGetMethod([NotNullWhen(true)] out GirModel.Method? getter)
        {
            if (!Readable || Getter is null)
            {
                getter = null;
                return false;
            }

            if (Getter.Parameters.Any())
            {
                //TODO: Workaround for: https://gitlab.gnome.org/GNOME/gobject-introspection/-/issues/421
                getter = null;
                return false;
            }

            getter = Getter;
            var getterType = Getter.ReturnType.CreateInternalModel().NullableTypeName;
            return NullableTypeName == getterType;
        }

        public bool SupportsAccessorSetMethod([NotNullWhen(true)] out GirModel.Method? setter)
        {
            if (!Writeable || Setter is null)
            {
                setter = null;
                return false;
            }

            if (Setter.Parameters.Count() > 1)
            {
                //TODO: Workaround for: https://gitlab.gnome.org/GNOME/gobject-introspection/-/issues/421
                setter = null;
                return false;
            }

            setter = Setter;
            var setterType = Setter.Parameters.First().CreateInternalModelForMethod().NullableTypeName;

            return NullableTypeName == setterType;
        }
    }
}
