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
public static readonly {Signal.GetDescriptorClassName(signal, cls)} {Signal.GetDescriptorName(signal)} = new (
    unmanagedName: ""{signal.Name}"",
    managedName: nameof({Signal.GetName(signal)})
);";
    }
}
