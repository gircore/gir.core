using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Generator.Renderer.Internal;

namespace Generator.Model;

internal static partial class Property
{
    private static readonly HashSet<GirModel.Property> ImplementExplicitly = new();
    private static readonly Dictionary<GirModel.Property, string> FixedNames = new();

    public static string GetName(GirModel.Property property)
    {
        //Does not need a lock as it is called only after all insertions are done.
        if (FixedNames.TryGetValue(property, out var value))
            return value;

        return property.Name.ToPascalCase();
    }

    internal static void SetName(GirModel.Property property, string name)
    {
        lock (FixedNames)
        {
            FixedNames[property] = name;
        }
    }

    public static string GetDescriptorName(GirModel.Property property)
    {
        return GetName(property) + "PropertyDefinition";
    }

    public static string GetNullableTypeName(GirModel.Property property)
    {
        return property.AnyType.Match(
            type => type switch
            {
                GirModel.ComplexType c => ComplexType.GetFullyQualified(c) + GetDefaultNullable(property),
                _ => Type.GetName(type) + GetDefaultNullable(property)
            },
            arrayType => ArrayType.GetName(arrayType)
        );
    }

    // Properties need special nullable handling as each C value is implicitly nullable.
    // The C# Marshaller returns default values for primitive value types. But there
    // can be null values for strings and objects, even if the nullability attribute
    // is not present in the gir file
    private static string GetDefaultNullable(GirModel.Property property)
    {
        if (!property.AnyType.TryPickT0(out var type, out _))
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

    public static bool SupportsAccessorGetMethod(GirModel.Property property, [NotNullWhen(true)] out GirModel.Method? getter)
    {
        if (!property.Readable || property.Getter is null)
        {
            getter = null;
            return false;
        }

        if (property.Getter.Parameters.Any())
        {
            //TODO: Workaround for: https://gitlab.gnome.org/GNOME/gobject-introspection/-/issues/421
            getter = null;
            return false;
        }

        getter = property.Getter;
        var getterType = property.Getter.ReturnType
            .Map(ReturnTypeRenderer.Render);

        return GetNullableTypeName(property) == getterType;
    }

    public static bool SupportsAccessorSetMethod(GirModel.Property property, [NotNullWhen(true)] out GirModel.Method? setter)
    {
        if (!property.Writeable || property.Setter is null)
        {
            setter = null;
            return false;
        }

        if (property.Setter.Parameters.Count() > 1)
        {
            //TODO: Workaround for: https://gitlab.gnome.org/GNOME/gobject-introspection/-/issues/421
            setter = null;
            return false;
        }

        setter = property.Setter;
        var setterType = property.Setter.Parameters
            .First()
            .Map(Parameters.GetNullableTypeName);

        return GetNullableTypeName(property) == setterType;
    }

    public static void ThrowIfNotSupported(GirModel.ComplexType complexType, GirModel.Property property)
    {
        if (!property.Readable && (!property.Writeable || property.ConstructOnly))
            throw new System.Exception("Not accessible");

        // Ignore properties which throw. This only occurs in deprecated WebKit2WebExtension functions.
        if (property.Getter is not null && property.Getter.Throws)
            throw new System.NotImplementedException($"Property {complexType.Name}.{property.Name} getter with throws=true is not supported");

        if (property.Setter is not null && property.Setter.Throws)
            throw new System.NotImplementedException($"Property {complexType.Name}.{property.Name} setter with throws=true is not supported");

        if (property.AnyType.Is<GirModel.PrimitiveType>())
            return;

        if (property.AnyType.IsArray<GirModel.String>())
            return;

        if (property.AnyType.IsArray<GirModel.Byte>())
            return;

        if (property.AnyType.Is<GirModel.Enumeration>())
            return;

        if (property.AnyType.Is<GirModel.Bitfield>())
            return;

        if (property.AnyType.Is<GirModel.Class>())
            return;

        if (property.AnyType.Is<GirModel.Interface>())
            return;

        if (property.AnyType.Is<GirModel.Record>())
            throw new System.NotImplementedException("There is currently no concept for transfering native records (structs) into the managed world.");

        throw new System.Exception($"Property {complexType.Name}.{property.Name} is not supported");
    }

    public static bool GetImplemnetExplicitly(GirModel.Property property)
    {
        //Does not need a lock as it is called only after all insertions are done.
        return ImplementExplicitly.Contains(property);
    }

    internal static void SetImplementExplicitly(GirModel.Property property)
    {
        lock (ImplementExplicitly)
        {
            ImplementExplicitly.Add(property);
        }
    }
}
