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
        return subclassData.IsGlobalNamespace
            ? RenderGlobalNamespace(subclassData)
            : RenderNamespace(subclassData);
    }

    private static string RenderGlobalNamespace(SubclassData subclassData)
    {
        return $"""
               #nullable enable
               {RenderClassHierarchy(subclassData)}
               """;
    }
    
    private static string RenderNamespace(SubclassData subclassData)
    {
        return $"""
                #nullable enable
                namespace {subclassData.Namespace};
                {RenderClassHierarchy(subclassData)}
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
                 {{subclassData.Accessibility}} partial class {{subclassData.NameGenericArguments}} : {{subclassData.Parent}}, GObject.GTypeProvider, GObject.InstanceFactory
                 {
                      private static readonly GObject.Type GType = GObject.Internal.SubclassRegistrar.Register<{{subclassData.NameGenericArguments}}, {{subclassData.Parent}}>();
                      public static new GObject.Type GetGType() => GType;
                 
                      static object GObject.InstanceFactory.Create(System.IntPtr handle, bool ownsHandle)
                      {
                          return new {{subclassData.NameGenericArguments}}(new {{subclassData.ParentHandle}}(handle, ownsHandle));
                      }
                      
                      {{GeneratedCodeAttribute.Render()}}
                      public {{subclassData.Name}}({{subclassData.ParentHandle}} handle) : base(handle) 
                      {
                          Initialize();
                      }
                      
                      {{GeneratedCodeAttribute.Render()}}
                      public {{subclassData.Name}}(params GObject.ConstructArgument[] constructArguments) : this({{subclassData.ParentHandle}}.For<{{subclassData.NameGenericArguments}}>(constructArguments)) 
                      {
                          Initialize();
                      }
                      
                      /// <summary>
                      /// This method is called by all generated constructors.
                      /// Implement this partial method to initialize all members.
                      /// Decorating this method with "MemberNotNullAttribute" for
                      /// the appropriate members can remove nullable warnings.
                      /// </summary>
                      partial void Initialize();
                 }
                 """;
    }
}
