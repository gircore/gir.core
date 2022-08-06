using System;

namespace Generator.Renderer.Internal;

internal static class RenderableInstanceParameterFactory
{
    public static RenderableInstanceParameter Create(GirModel.InstanceParameter instanceParameter) => instanceParameter.Type switch
    {
        GirModel.Pointer => PointerInstanceParameter.Create(instanceParameter),
        GirModel.Class => ClassInstanceParameter.Create(instanceParameter),
        GirModel.Interface => InterfaceInstanceParameter.Create(instanceParameter),
        GirModel.Record => RecordInstanceParameter.Create(instanceParameter),
        GirModel.Union => UnionInstanceParameter.Create(instanceParameter),

        _ => throw new Exception($"Instance parameter \"{instanceParameter.Name}\" of type {instanceParameter.Type} can not be converted into a model")
    };
}
