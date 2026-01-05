using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;

namespace Gtk.Integration.SourceGenerator;

internal static class TemplateCode
{
    public static void Generate(SourceProductionContext context, TemplateData data)
    {
        context.AddSource(
            hintName: $"{data.TypeData.Filename}.Template.g.cs",
            source: ToCode(data)
        );
    }

    private static string ToCode(TemplateData data)
    {
        return data.TypeData.IsGlobalNamespace
            ? RenderGlobalNamespace(data)
            : RenderNamespace(data);
    }

    private static string RenderGlobalNamespace(TemplateData data)
    {
        return $"""
                #nullable enable
                {RenderClassHierarchy(data)}
                """;
    }

    private static string RenderNamespace(TemplateData data)
    {
        return $$"""
                 #nullable enable
                 namespace {{data.TypeData.Namespace}};
                 {{RenderClassHierarchy(data)}}
                 """;
    }

    private static string RenderClassHierarchy(TemplateData data)
    {
        var sb = new StringBuilder();
        foreach (var typeData in data.TypeData.UpperNestedTypes)
            sb.AppendLine(CultureInfo.InvariantCulture, $"{typeData.Accessibility} partial {typeData.Kind} {typeData.NameGenericArguments} {{");

        sb.AppendLine(RenderClassContent(data));

        foreach (var _ in data.TypeData.UpperNestedTypes)
            sb.AppendLine("}");

        return sb.ToString();
    }

    private static string RenderClassContent(TemplateData data)
    {
        return $$"""
                {{data.TypeData.Properties.Accessibility}} partial class {{data.TypeData.Properties.NameGenericArguments}}
                {
                    {{GeneratedCodeAttribute.Render()}}
                    static partial void CompositeTemplateClassInit(System.IntPtr cls, System.IntPtr clsData)
                    {
                        var bytes = {{data.Loader}}.Load("{{data.ResourceName}}");

                        var classHandle = new Gtk.Internal.WidgetClassUnownedHandle(cls);
                        Gtk.Internal.WidgetClass.SetTemplate(
                            widgetClass: classHandle,
                            templateBytes: bytes.Handle
                        );
                    
                        {{RenderChildBindings(data)}}

                        /*

                        Gtk.Internal.WidgetClass.BindTemplateCallbackFull(
                            widgetClass: classHandle,
                            callbackName: GLib.Internal.NonNullableUtf8StringOwnedHandle.Create("Button"),
                            callbackSymbol: null! //TODO
                        );*/
                    }
                
                    {{GeneratedCodeAttribute.Render()}}
                    static partial void CompositeTemplateInstanceInit(System.IntPtr instance, System.IntPtr cls)
                    {
                        Gtk.Internal.Widget.InitTemplate(instance);
                    }

                    {{GeneratedCodeAttribute.Render()}}
                    static partial void CompositeTemplateDispose(System.IntPtr instance)
                    {
                        Gtk.Internal.Widget.DisposeTemplate(instance, GType);
                    }
                    
                    {{GeneratedCodeAttribute.Render()}}
                    {{RenderMemberNotNull(data)}}
                    partial void CompositeTemplateInitialize()
                    {
                        {{RenderMemberMapping(data)}}
                    }
                }
                """;
    }

    private static string RenderChildBindings(TemplateData data)
    {
        var sb = new StringBuilder();

        foreach (var connection in data.Connections)
        {
            sb.AppendLine(
                provider: CultureInfo.InvariantCulture,
                handler: $"""
                          Gtk.Internal.WidgetClass.BindTemplateChildFull(
                              widgetClass: classHandle,
                              name: GLib.Internal.NonNullableUtf8StringOwnedHandle.Create("{connection.ObjectId}"),
                              internalChild: false,
                              structOffset: 0
                          );
                          """
            );
        }

        return sb.ToString();
    }

    private static string RenderMemberMapping(TemplateData data)
    {
        var sb = new StringBuilder();

        foreach (var connection in data.Connections)
        {
            sb.AppendLine(
                provider: CultureInfo.InvariantCulture,
                handler: $"""{connection.MemberName} = ({connection.Type}) GetTemplateChild(GetGType(), "{connection.ObjectId}");"""
            );
        }

        return sb.ToString();
    }

    private static string RenderMemberNotNull(TemplateData data)
    {
        var names = string.Join(", ", data.Connections.Select(x => $"nameof({x.MemberName})"));
        return $"[System.Diagnostics.CodeAnalysis.MemberNotNull({names})]";
    }
}
