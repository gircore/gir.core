using System.Linq;
using Generator.Model;

namespace Generator.Renderer.Public;

public static partial class ClassSignal
{
    private static string RenderArgsIndexer(GirModel.Class cls, GirModel.Signal signal)
    {
        return !signal.Parameters.Any()
            ? string.Empty
            : @$"
/// <summary>
/// Indexer to connect {Signal.GetDescriptorName(signal)} with a SignalHandler&lt;{GetGenericArgs(cls, signal)}&gt;
/// </summary>
public SignalHandler<{GetGenericArgs(cls, signal)}> this[Signal<{GetGenericArgs(cls, signal)}> signal]
{{
    set => signal.Connect(this, value, true);
}}";
    }
}
