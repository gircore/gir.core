using System;
using System.Collections.Generic;

namespace GirLoader.Output;

public partial class Method : GirModel.Method
{
    GirModel.ComplexType GirModel.Method.Parent => _parent ?? throw new Exception($"{Identifier}: Unknown parent");
    string GirModel.Method.Name => Name;
    GirModel.ReturnType GirModel.Method.ReturnType => ReturnValue;
    string GirModel.Method.CIdentifier => Identifier;
    GirModel.InstanceParameter GirModel.Method.InstanceParameter => ParameterList.InstanceParameter ?? throw new Exception("Instance parameter mis missing");
    IEnumerable<GirModel.Parameter> GirModel.Method.Parameters => ParameterList.SingleParameters;
    bool GirModel.Method.Introspectable => Introspectable;
    GirModel.Property? GirModel.Method.GetProperty => GetProperty?.GetProperty();
    GirModel.Property? GirModel.Method.SetProperty => SetProperty?.GetProperty();
    string? GirModel.Method.Version => Version;
}
