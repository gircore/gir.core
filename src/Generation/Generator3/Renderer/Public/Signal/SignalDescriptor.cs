namespace Generator3.Renderer.Public
{
    public static class SignalDescriptor
    {
        public static string RenderDescriptor(this Model.Public.Signal signal)
        {
            var signalGenericArgs = signal.HasArgs 
                ? $"{signal.ClassName}, {signal.ArgsName}"
                :signal.ClassName;

            return @$"
/// <summary>
/// Signal Descriptor for {signal.ManagedName}.
/// </summary>
public static readonly Signal<{signalGenericArgs}> {signal.DescriptorName} = Signal<{signalGenericArgs}>.Wrap(""{signal.NativeName}"");";
        }
    }
}
