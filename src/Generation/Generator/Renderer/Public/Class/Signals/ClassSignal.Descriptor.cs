using Generator.Model;

namespace Generator.Renderer.Public;

public static partial class ClassSignal
{
    private static string RenderDescriptor(GirModel.Class cls, GirModel.Signal signal)
    {
        return @$"
/// <summary>
/// Signal Descriptor for {Signal.GetName(signal)}.
/// </summary>
public static readonly Signal<{GetGenericArgs(cls, signal)}> {Signal.GetDescriptorName(signal)} = Signal<{GetGenericArgs(cls, signal)}>.Wrap(""{signal.Name}"");";
    }
}
