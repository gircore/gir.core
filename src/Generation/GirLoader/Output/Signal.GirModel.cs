using System.Collections.Generic;

namespace GirLoader.Output
{
    public partial class Signal : GirModel.Signal
    {
        string GirModel.Signal.ManagedName => Name;
        string GirModel.Signal.NativeName => OriginalName;
        IEnumerable<GirModel.Parameter> GirModel.Signal.Parameters => ParameterList.SingleParameters;
    }
}
