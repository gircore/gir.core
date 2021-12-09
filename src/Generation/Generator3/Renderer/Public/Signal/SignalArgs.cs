namespace Generator3.Renderer.Public
{
    public static class SignalArgs
    {
        public static string RenderArgs(this Model.Public.Signal signal)
        {
            return !signal.HasArgs 
                ? string.Empty 
                : @$"
/// <summary>
/// Signal (Event) Arguments for {signal.PublicName}
/// </summary>
public sealed class {signal.ArgsClassName} : SignalArgs
{{
    {signal.Parameters.RenderAsSignalParammeters()}
}}";
        }
    }
}
