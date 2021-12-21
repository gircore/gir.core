using System;
using System.Text;

namespace Generator3.Renderer.Public
{
    public static class Signal
    {
        public static string Render(this Model.Public.Signal signal)
        {
            try
            {
                var builder = new StringBuilder();
                builder.AppendLine($"#region {signal.PublicName}");
                builder.AppendLine(signal.RenderArgs());
                builder.AppendLine(signal.RenderArgsIndexer());
                builder.AppendLine(signal.RenderDescriptor());
                builder.AppendLine(signal.RenderEvent());
                builder.AppendLine("#endregion");

                return builder.ToString();
            }
            catch (Exception ex)
            {
                var message = $"Did not generate signal '{signal.ClassName}.{signal.PublicName}': {ex.Message}";

                if (ex is NotImplementedException)
                    Log.Debug(message);
                else
                    Log.Warning(message);

                return string.Empty;
            }
        }
    }
}
