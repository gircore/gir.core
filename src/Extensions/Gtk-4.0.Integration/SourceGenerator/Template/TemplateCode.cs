using System.Globalization;
using System.Text;
using Microsoft.CodeAnalysis;

namespace Gtk.Integration.SourceGenerator;

internal static class TemplateCode
{
    public static void Generate(SourceProductionContext context, TemplateData templateData)
    {
        context.AddSource(
            hintName: $"{templateData.FileName}.Template.g.cs",
            source: ToCode(templateData)
        );
    }

    private static string ToCode(TemplateData templateData)
    {
        return templateData.IsGlobalNamespace
            ? RenderGlobalNamespace(templateData)
            : RenderNamespace(templateData);
    }

    private static string RenderGlobalNamespace(TemplateData templateData)
    {
        return $"""
                #nullable enable
                {RenderClassHierarchy(templateData)}
                """;
    }

    private static string RenderNamespace(TemplateData templateData)
    {
        return $"""
                #nullable enable
                namespace {templateData.Namespace};

                {RenderClassHierarchy(templateData)}
                """;
    }

    private static string RenderClassHierarchy(TemplateData templateData)
    {
        var sb = new StringBuilder();
        foreach (var typeData in templateData.UpperNestedClasses)
            sb.AppendLine(CultureInfo.InvariantCulture,
                $"{typeData.Accessibility} partial {typeData.Kind} {typeData.NameGenericArguments} {{");

        sb.AppendLine(RenderClassContent(templateData));

        foreach (var _ in templateData.UpperNestedClasses)
            sb.AppendLine("}");

        return sb.ToString();
    }

    private static string RenderClassContent(TemplateData templateData)
    {
        return $$"""
                 {{GeneratedCodeComment.Render()}}
                 {{templateData.Accessibility}} unsafe partial class {{templateData.NameGenericArguments}}
                 {
                     static partial void CompositeTemplateClassInit(System.IntPtr cls, System.IntPtr clsdata)
                     {
                         var assembly = System.Reflection.Assembly.GetExecutingAssembly();
                         var data = GObject.AssemblyExtension.ReadResourceAsByteArray(assembly, "{{templateData.RessourceName}}");
                         var bytes = GLib.Bytes.New(data);

                         var classHandle = new Gtk.Internal.WidgetClassUnownedHandle(cls);
                         Gtk.Internal.WidgetClass.SetTemplate(
                             widgetClass: classHandle,
                             templateBytes: bytes.Handle
                         );
                     
                         /*Gtk.Internal.WidgetClass.BindTemplateChildFull(
                             widgetClass: classHandle,
                             name: GLib.Internal.NonNullableUtf8StringOwnedHandle.Create("Button"),
                             internalChild: false,
                             structOffset: 0
                         );
                     
                         Gtk.Internal.WidgetClass.BindTemplateCallbackFull(
                             widgetClass: classHandle,
                             callbackName: GLib.Internal.NonNullableUtf8StringOwnedHandle.Create("Button"),
                             callbackSymbol: null! //TODO
                         );*/
                     }
                 
                     static partial void CompositeTemplateInstanceInit(System.IntPtr instance, System.IntPtr cls)
                     {
                         Gtk.Internal.Widget.InitTemplate(instance);
                     }

                     static partial void CompositeTemplateDispose(System.IntPtr instance)
                     {
                         Gtk.Internal.Widget.DisposeTemplate(instance, GType);
                     }
                 }
                 """;
    }
}
