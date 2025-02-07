using System.Globalization;
using System.Text;
using Microsoft.CodeAnalysis;

namespace GObject.Integration.SourceGenerator;

internal static class SubclassCode
{
    public static void Generate(SourceProductionContext context, SubclassData subclassData)
    {
        context.AddSource(
            hintName: $"{subclassData.FileName}.Subclass.g.cs",
            source: ToCode(subclassData)
        );
    }
    private static string ToCode(SubclassData subclassData)
    {
        return $$"""
               namespace {{subclassData.Namespace}};
               
               {{RenderClassHierarchy(subclassData)}}
               """;
    }

    private static string RenderClassHierarchy(SubclassData subclassData)
    {
        var sb = new StringBuilder();
        foreach (var typeData in subclassData.UpperNestedClasses)
            sb.AppendLine(CultureInfo.InvariantCulture, $"{typeData.Accessibility} partial {typeData.Kind} {typeData.NameGenericArguments} {{");

        sb.AppendLine(RenderClassContent(subclassData));

        foreach (var _ in subclassData.UpperNestedClasses)
            sb.AppendLine("}");

        return sb.ToString();
    }

    private static string RenderClassContent(SubclassData subclassData)
    {
        return $$"""
                 {{subclassData.Accessibility}} partial class {{subclassData.NameGenericArguments}}({{subclassData.ParentHandle}} handle) : {{subclassData.Parent}}(handle), GObject.GTypeProvider, GObject.InstanceFactory
                 {
                      private static readonly GObject.Type GType = GObject.Internal.SubclassRegistrar.Register<{{subclassData.NameGenericArguments}}, {{subclassData.Parent}}>();
                      public static new GObject.Type GetGType() => GType;
                 
                      static object GObject.InstanceFactory.Create(System.IntPtr handle, bool ownsHandle)
                      {
                          return new {{subclassData.NameGenericArguments}}(new {{subclassData.ParentHandle}}(handle, ownsHandle));
                      }
                      
                      public {{subclassData.Name}}(params GObject.ConstructArgument[] constructArguments) : this({{subclassData.ParentHandle}}.For<{{subclassData.NameGenericArguments}}>(constructArguments)) { }
                 }
                 """;
    }
}
