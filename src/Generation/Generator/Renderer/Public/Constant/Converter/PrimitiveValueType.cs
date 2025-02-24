using System;

namespace Generator.Renderer.Public.Constant;

internal class PrimitiveValueType : ConstantsConverter
{
    public bool Supports(GirModel.Type type)
    {
        return type is GirModel.PrimitiveValueType;
    }

    public RenderableConstant Convert(GirModel.Constant constant)
    {
        if (!IsValidValue(constant))
            throw new Exception($"Can't render primitive value type constant {constant.Name} of type {constant.Type} and with value {constant.Value} as the value could not be parsed.");

        var typeName = Model.Type.GetName(constant.Type);

        return new RenderableConstant(
            Type: typeName,
            Name: Model.Constant.GetName(constant),
            Value: constant.Value
        );
    }

    private static bool IsValidValue(GirModel.Constant constant)
    {
        return constant.Type switch
        {
            GirModel.Bool => bool.TryParse(constant.Value, out _),
            GirModel.Byte => byte.TryParse(constant.Value, out _),
            GirModel.Double => double.TryParse(constant.Value, out _),
            GirModel.Integer => int.TryParse(constant.Value, out _),
            GirModel.CLong => long.TryParse(constant.Value, out _),
            GirModel.Long => long.TryParse(constant.Value, out _),
            GirModel.NativeInteger => nint.TryParse(constant.Value, out _),
            GirModel.NativeUnsignedInteger => nuint.TryParse(constant.Value, out _),
            GirModel.Short => short.TryParse(constant.Value, out _),
            GirModel.SignedByte => sbyte.TryParse(constant.Value, out _),
            GirModel.UnsignedInteger => uint.TryParse(constant.Value, out _),
            GirModel.UnsignedLong => ulong.TryParse(constant.Value, out _),
            GirModel.UnsignedCLong => ulong.TryParse(constant.Value, out _),
            GirModel.UnsignedShort => ushort.TryParse(constant.Value, out _),
            _ => false
        };
    }
}
