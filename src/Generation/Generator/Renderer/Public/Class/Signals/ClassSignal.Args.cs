using System.Linq;
using Generator.Model;

namespace Generator.Renderer.Public;

public static partial class ClassSignal
{
    private static string RenderArgs(GirModel.Signal signal)
    {
        return !signal.Parameters.Any()
            ? string.Empty
            : @$"
/// <summary>
/// Signal (Event) Arguments for {Signal.GetName(signal)}
/// </summary>
public sealed class {Signal.GetArgsClassName(signal)} : SignalArgs
{{
    {RenderAsSignalParammeters(signal.Parameters)}
}}";
    }
}
