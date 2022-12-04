using System;

namespace GirLoader.Output;

public partial class InstanceParameter : GirModel.InstanceParameter
{
    string GirModel.InstanceParameter.Name => Name;
    GirModel.Type GirModel.InstanceParameter.Type => TypeReference.Type ?? throw new Exception("Instance parameter must be set");
    GirModel.Direction GirModel.InstanceParameter.Direction => Direction.ToGirModel();
    GirModel.Transfer GirModel.InstanceParameter.Transfer => Transfer.ToGirModel();
}
