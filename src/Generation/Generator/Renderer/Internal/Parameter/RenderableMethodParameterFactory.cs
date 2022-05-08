﻿using System;

namespace Generator.Renderer.Internal;

internal static class MethodParameterFactory
{
    public static RenderableParameter Create(this GirModel.Parameter parameter) => parameter.AnyType.Match(
        type => type switch
        {
            GirModel.String => StringParameterFactory.Create(parameter),
            GirModel.Pointer => PointerParameter.Create(parameter),
            GirModel.UnsignedPointer => UnsignedPointerParameter.Create(parameter),
            GirModel.Class => ClassParameter.Create(parameter),
            GirModel.Interface => InterfaceParameter.Create(parameter),
            GirModel.Union => UnionParameter.Create(parameter),
            GirModel.Record => RecordParameter.Create(parameter),
            GirModel.PrimitiveValueType => PrimitiveValueTypeParameter.Create(parameter),
            GirModel.Callback => CallbackParameter.Create(parameter),
            GirModel.Enumeration => EnumerationParameter.Create(parameter),
            GirModel.Bitfield => BitfieldParameter.Create(parameter),
            GirModel.Void => VoidParameterFactory.Create(parameter),

            _ => throw new Exception($"Parameter \"{parameter.Name}\" of type {parameter.AnyType} can not be converted into a model")
        },
        arrayType => arrayType.AnyType.Match(
            type => type switch
            {
                GirModel.Class => ArrayClassParameterForMethods.Create(parameter),
                GirModel.Interface => ArrayInterfaceParameterForMethods.Create(parameter),
                GirModel.Record when arrayType.IsPointer => ArrayPointerRecordParameterForMethod.Create(parameter),
                GirModel.Record => ArrayRecordParameterForMethod.Create(parameter),
                GirModel.String => ArrayStringParameterForMethod.Create(parameter),
                GirModel.Enumeration => ArrayEnumerationParameter.Create(parameter),
                _ => StandardParameter.Create(parameter)
            },
            _ => throw new NotSupportedException("Arrays of arrays not yet supported")
        )
    );
}
