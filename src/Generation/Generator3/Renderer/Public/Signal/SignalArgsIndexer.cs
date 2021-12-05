namespace Generator3.Renderer.Public
{
    public static class SignalArgsIndexer
    {
        public static string RenderArgsIndexer(this Model.Public.Signal signal)
        {
            return !signal.HasArgs 
                ? string.Empty 
                : @$"
/// <summary>
/// Indexer to connect {signal.DescriptorName} with a SignalHandler&lt;{signal.GenericArgs}&gt;
/// </summary>
public SignalHandler<{signal.GenericArgs}> this[Signal<{signal.GenericArgs}> signal]
{{
    set => signal.Connect(this, value, true);
}}";
        }
    }
}
