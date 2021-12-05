using System.Linq;

namespace Generator3.Generation.Class.Standard
{
    public static class ObjectIndexer
    {
        public static string RenderObjectIndexer(this PublicSignalsModel model)
        {
            return !model.Signals.Any()
                ? string.Empty
                : @$"
/// <summary>
/// Indexer to connect with a SignalHandler&lt;{model.Name}&gt;
/// </summary>
public SignalHandler<{model.Name}> this[Signal<{model.Name}> signal]
{{
    set => signal.Connect(this, value, true);
}}";
        }
    }
}
