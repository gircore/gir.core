using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;

namespace GObject.Integration.SourceGenerator;

internal static class IntegrationCode
{
    public static void Generate(SourceProductionContext context, ImmutableArray<SubclassData> subclassData)
    {
        context.AddSource(
            hintName: "GirCore.Integration.g.cs",
            source: ToCode(subclassData)
        );
    }

    private static string ToCode(ImmutableArray<SubclassData> subclassData)
    {
        return $$"""
                 namespace GirCore;

                 internal static class Integration
                 {
                    {{GeneratedCodeAttribute.Render()}}
                    internal static void Initialize()
                    {
                        RegisterSubclasses();
                    }
                     
                    {{GeneratedCodeAttribute.Render()}}
                    private static void RegisterSubclasses()
                    {
                        //Call GetGType for all subclasses to register them
                        {{ToSubclassInitializationCode(subclassData)}}
                    }
                     
                    {{GeneratedCodeAttribute.Render()}}
                    private static void Register<T>() where T : GObject.GTypeProvider
                    {
                        try
                        {
                            _ = T.GetGType();
                        }
                        catch(System.Exception ex)
                        {
                            throw new System.Exception($"Could not register type {typeof(T).FullName}. Ensure that you initialize depending modules by calling their module initializer first. E.g. call 'Gtk.Module.Initialize()' before calling 'GirCore.Integration.Initialize()' if your subclass depends on Gtk.", ex);
                        }
                    }
                 }
                 """;
    }

    private static string ToSubclassInitializationCode(ImmutableArray<SubclassData> subclassData)
    {
        var sb = new StringBuilder();

        foreach (var data in subclassData)
        {
            var prefix = data.TypeData.IsGlobalNamespace
                ? string.Empty
                : data.TypeData.Namespace + ".";

            prefix = data.TypeData.UpperNestedTypes.Aggregate(
                seed: prefix,
                func: (current, nestedType) => current + nestedType.NameGenericArguments + "."
            );

            sb.AppendLine(
                provider: CultureInfo.InvariantCulture,
                handler: $"Register<{prefix}{data.TypeData.Properties.NameGenericArguments}>();"
            );
        }

        return sb.ToString();
    }
}
