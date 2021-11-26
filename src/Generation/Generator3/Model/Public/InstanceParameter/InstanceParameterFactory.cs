using System;

namespace Generator3.Model.Public
{
    public static class InstanceParameterFactory
    {
        public static InstanceParameter CreatePublicModel(this GirModel.InstanceParameter parameter) => parameter.Type switch
        {
            _ => throw new Exception($"Instance parameter \"{parameter.Name}\" of type {parameter.Type} can not be converted into a model")
        };
    }
}
