using Generator.Model;

namespace Generator.Renderer.Public;

public static class SignalEvent
{
    public static string Render(GirModel.ComplexType type, GirModel.Signal signal)
    {
        return $@"
public event {Signal.GetDelegateName(signal, type)} {Signal.GetName(signal)}
{{
    add => {Signal.GetDescriptorName(signal)}.Connect(this, value);
    remove => {Signal.GetDescriptorName(signal)}.Disconnect(this, value);
}}";
    }
}
