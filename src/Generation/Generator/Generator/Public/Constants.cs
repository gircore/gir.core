using System.Collections.Generic;
using System.Linq;
using Generator.Model;

namespace Generator.Generator.Public;

internal class Constants : Generator<IEnumerable<GirModel.Constant>>
{
    private readonly Publisher _publisher;

    public Constants(Publisher publisher)
    {
        _publisher = publisher;
    }

    public void Generate(IEnumerable<GirModel.Constant> obj)
    {
        if (!obj.Any())
            return;

        var project = Namespace.GetCanonicalName(obj.First().Namespace);

        var codeUnit = obj
            .Where(IsValid)
            .Map(Renderer.Public.Constants.Render)
            .Map(source => new CodeUnit(project, "Constants", source, false));

        _publisher.Publish(codeUnit);
    }

    private static bool IsValid(GirModel.Constant constant)
    {
        switch (constant.Type)
        {
            case GirModel.PrimitiveType:
                return IsValidPrimitiveType(constant);
            case GirModel.Bitfield:
                return true;
            default:
                Log.Warning($"Excluding {constant.Namespace} constant '{constant.Name}'. Can't assign value '{constant.Value}' to unsupported type '{Type.GetName(constant.Type)}'.");
                return false;
        }
    }

    private static bool IsValidPrimitiveType(GirModel.Constant constant)
    {
        var canParse = constant.Type switch
        {
            GirModel.Bool => bool.TryParse(constant.Value, out _),
            GirModel.Byte => byte.TryParse(constant.Value, out _),
            GirModel.Double => double.TryParse(constant.Value, out _),
            GirModel.Integer => int.TryParse(constant.Value, out _),
            GirModel.Long => long.TryParse(constant.Value, out _),
            GirModel.NativeUnsignedInteger => nuint.TryParse(constant.Value, out _),
            GirModel.Short => short.TryParse(constant.Value, out _),
            GirModel.SignedByte => sbyte.TryParse(constant.Value, out _),
            GirModel.String => true,
            GirModel.UnsignedInteger => uint.TryParse(constant.Value, out _),
            GirModel.UnsignedLong => ulong.TryParse(constant.Value, out _),
            GirModel.UnsignedShort => ushort.TryParse(constant.Value, out _),
            _ => false
        };

        if (canParse)
            return true;

        Log.Warning($"Excluding {constant.Namespace} constant '{constant.Name}'. Can't convert value '{constant.Value}' to type '{Type.GetName(constant.Type)}'.");

        return false;
    }
}
