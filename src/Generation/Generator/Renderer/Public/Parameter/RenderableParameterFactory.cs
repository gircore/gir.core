using System;

namespace Generator.Renderer.Public;

internal static class RenderableParameterFactory
{
    //TODO: Use Interface and list of converters instead of manual mapping
    public static ParameterTypeData Create(GirModel.Parameter parameter) => parameter.AnyTypeOrVarArgs.Match(
        anyType => anyType.Match(
            type => type switch
            {
                GirModel.Class => ClassParameter.Create(parameter),
                GirModel.Interface => InterfaceParameter.Create(parameter),
                GirModel.Bitfield => BitfieldParameter.Create(parameter),
                GirModel.Enumeration => EnumerationParameter.Create(parameter),
                GirModel.Union => UnionParameter.Create(parameter),
                GirModel.Record => RecordParameter.Create(parameter),
                GirModel.Void => VoidParameter.Create(parameter),
                GirModel.Callback => CallbackParameter.Create(parameter),
                GirModel.PrimitiveValueType => PrimitiveValueTypeParameter.Create(parameter),
                _ => StandardParameter.Create(parameter) //TODO: Remove Standard Parameter
            },
            arraytype => arraytype.AnyType.Match(
                type => type switch
                {
                    GirModel.Record => ArrayRecordParameter.Create(parameter),
                    GirModel.Class => ArrayClassParameter.Create(parameter),
                    _ => StandardParameter.Create(parameter)
                },
                _ => throw new NotSupportedException("Arrays of arrays not yet supported")
            )
        ),
        varargs => throw new Exception($"Parameter \"{parameter.Name}\" can not be converted as variadic parameters are not supported")
    );
}
