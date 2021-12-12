using System;
using System.Collections.Generic;

namespace GirLoader.Output
{
    public partial class Method : GirModel.Method
    {
        string GirModel.Method.Name => Name;
        GirModel.ReturnType GirModel.Method.ReturnType => ReturnValue;
        string GirModel.Method.CIdentifier => Identifier;
        GirModel.InstanceParameter GirModel.Method.InstanceParameter => ParameterList.InstanceParameter ?? throw new Exception("Instance parameter mis missing");
        IEnumerable<GirModel.Parameter> GirModel.Method.Parameters => ParameterList.SingleParameters;
    }
}
