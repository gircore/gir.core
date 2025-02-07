using Microsoft.CodeAnalysis;

namespace GObject.Integration.SourceGenerator;

[Generator]
public class Generator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.EnableSubclassSupport();
    }
}
