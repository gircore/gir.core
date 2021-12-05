namespace Generator3.Renderer.Public
{
    public static class SignalEvent
    {
        public static string RenderEvent(this Model.Public.Signal signal)
        {
            return $@"
public event SignalHandler<{signal.GenericArgs}> {signal.ManagedName}
{{
    add => {signal.DescriptorName}.Connect(this, value);
    remove => {signal.DescriptorName}.Disconnect(this, value);
}}";
        }
    }
}
