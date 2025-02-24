using System.Collections.Generic;

namespace GirLoader.Output;

public partial class Signal : GirModel.Signal
{
    string GirModel.Signal.Name => Name;
    IEnumerable<GirModel.Parameter> GirModel.Signal.Parameters => ParameterList.SingleParameters;
    GirModel.ReturnType GirModel.Signal.ReturnType => ReturnValue;
    bool GirModel.Signal.Introspectable => Introspectable;
}
