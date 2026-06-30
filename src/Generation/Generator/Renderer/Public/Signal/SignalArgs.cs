using System.Linq;
using Generator.Model;

namespace Generator.Renderer.Public;

public static class SignalArgs
{
    public static string Render(GirModel.Signal signal)
    {
        return !signal.Parameters.Any()
            ? string.Empty
            : $$"""
                /// <summary>
                /// Signal (Event) Arguments for {{Signal.GetName(signal)}}
                /// </summary>
                public sealed class {{Signal.GetArgsClassName(signal)}} : SignalArgs
                {
                    {{Signals.SignalArgsParameterRenderer.Render(signal.Parameters)}}
                }
                """;
    }
}
