namespace Generator3.Renderer.Public
{
    public static class SignalDescriptor
    {
        public static string RenderDescriptor(this Model.Public.Signal signal)
        {
            return @$"
/// <summary>
/// Signal Descriptor for {signal.PublicName}.
/// </summary>
public static readonly Signal<{signal.GenericArgs}> {signal.DescriptorName} = Signal<{signal.GenericArgs}>.Wrap(""{signal.NativeName}"");";
        }
    }
}
