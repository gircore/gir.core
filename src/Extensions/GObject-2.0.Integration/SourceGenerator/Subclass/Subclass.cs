using Microsoft.CodeAnalysis;

namespace GObject.Integration.SourceGenerator;

internal static class Subclass
{
    public static void EnableSubclassSupport(this IncrementalGeneratorInitializationContext context)
    {
        context.RegisterImplementationSourceOutput(
            source: context.GetSubclassValuesProvider(),
            action: SubclassCode.Generate
        );
    }
}
