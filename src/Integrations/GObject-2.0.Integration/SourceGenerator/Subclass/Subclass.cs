using Microsoft.CodeAnalysis;

namespace GObject.Integration.SourceGenerator;

internal static class Subclass
{
    public static void EnableSubclassSupport(this IncrementalGeneratorInitializationContext context)
    {
        var subclassValuesProvider = context.GetSubclassValuesProvider();

        context.RegisterImplementationSourceOutput(
            source: subclassValuesProvider,
            action: SubclassCode.Generate
        );

        context.RegisterImplementationSourceOutput(
            source: subclassValuesProvider.Collect(),
            action: IntegrationCode.Generate
        );
    }
}
