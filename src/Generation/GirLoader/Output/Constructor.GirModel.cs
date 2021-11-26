using System.Collections.Generic;
using System.Linq;

namespace GirLoader.Output
{
    public partial class Constructor : GirModel.Constructor
    {
        string GirModel.Constructor.Name => Name;
        GirModel.ReturnType GirModel.Constructor.ReturnType => ReturnValue;
        string GirModel.Constructor.CIdentifier => OriginalName;
        IEnumerable<GirModel.Parameter> GirModel.Constructor.Parameters => ParameterList.GetParameters().Cast<GirModel.Parameter>();
    }
}
