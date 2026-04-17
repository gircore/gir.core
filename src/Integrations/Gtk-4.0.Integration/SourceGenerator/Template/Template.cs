using Microsoft.CodeAnalysis;

namespace Gtk.Integration.SourceGenerator;

internal static class Template
{
    public static void EnableTemplateSupport(this IncrementalGeneratorInitializationContext context)
    {
        context.RegisterImplementationSourceOutput(
            source: context.GetTemplateValuesProvider(),
            action: TemplateCode.Generate
        );
    }
}
