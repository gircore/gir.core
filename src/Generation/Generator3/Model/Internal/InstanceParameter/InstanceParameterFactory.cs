using System;

namespace Generator3.Model.Internal
{
    public static class InstanceParameterFactory
    {
        public static InstanceParameter CreateInternalModel(this GirModel.InstanceParameter parameter) => parameter.Type switch
        {
            GirModel.Pointer => new PointerInstanceParameter(parameter),
            GirModel.Class => new ClassInstanceParameter(parameter),
            GirModel.Interface => new InterfaceInstanceParameter(parameter),
            GirModel.Record => new RecordInstanceParameter(parameter),
            GirModel.Union => new UnionInstanceParameter(parameter),

            _ => throw new Exception($"Instance parameter \"{parameter.Name}\" of type {parameter.Type} can not be converted into a model")
        };
    }
}
