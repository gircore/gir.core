using System.Collections.Generic;
using System.Linq;
using GirLoader.Output;

namespace GirLoader;

internal class ShadowableAdapter : ShadowableProvider
{
    public IEnumerable<Constructor>? Constructors => null;
    public IEnumerable<Method> Methods => Enumerable.Empty<Method>();
    public IEnumerable<Function> Functions { get; }

    public ShadowableAdapter(IEnumerable<Function> functions)
    {
        Functions = functions;
    }
}
