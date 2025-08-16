using System;
using System.Linq;
using Generator.Model;

namespace Generator.Renderer.Public;

public static class ClassInterfaceSignal
{
    public static string Render(GirModel.ComplexType type, GirModel.Signal signal)
    {
        try
        {
            return $@"
#region {Signal.GetName(signal)}
{SignalDescriptor.Render(type, signal)}
{SignalEvent.Render(type, signal)}
#endregion
";
        }
        catch (Exception ex)
        {
            var message = $"Did not generate signal '{type.Name}.{Signal.GetName(signal)}': {ex.Message}";

            if (ex is NotImplementedException)
                Log.Debug(message);
            else
                Log.Warning(message);

            return string.Empty;
        }
    }
}
