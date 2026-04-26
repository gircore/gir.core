using Microsoft.CodeAnalysis;

namespace Gtk.Integration.SourceGenerator;

[Generator]
public class Generator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.EnableTemplateSupport();
    }
}
