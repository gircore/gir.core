using Generator.Model;

namespace Generator.Renderer.Public;

public static partial class ClassSignal
{
    private static string RenderEvent(GirModel.Class cls, GirModel.Signal signal)
    {
        return $@"
public event {Signal.GetDelegateName(signal, cls)} {Signal.GetName(signal)}
{{
    add => {Signal.GetDescriptorName(signal)}.Connect(this, value);
    remove => {Signal.GetDescriptorName(signal)}.Disconnect(this, value);
}}";
    }
}
