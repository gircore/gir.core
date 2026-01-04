using System.Globalization;
using System.Text;
using Microsoft.CodeAnalysis;

namespace GObject.Integration.SourceGenerator;

internal static class SubclassCode
{
    public static void Generate(SourceProductionContext context, SubclassData subclassData)
    {
        context.AddSource(
            hintName: $"{subclassData.TypeData.Filename}.Subclass.g.cs",
            source: ToCode(subclassData)
        );
    }
    private static string ToCode(SubclassData subclassData)
    {
        return subclassData.TypeData.IsGlobalNamespace
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
                namespace {subclassData.TypeData.Namespace};
                {RenderClassHierarchy(subclassData)}
                """;
    }

    private static string RenderClassHierarchy(SubclassData subclassData)
    {
        var sb = new StringBuilder();
        foreach (var typeData in subclassData.TypeData.UpperNestedTypes)
            sb.AppendLine(CultureInfo.InvariantCulture, $"{typeData.Accessibility} partial {typeData.Kind} {typeData.NameGenericArguments} {{");

        sb.AppendLine(RenderClassContent(subclassData));

        foreach (var _ in subclassData.TypeData.UpperNestedTypes)
            sb.AppendLine("}");

        return sb.ToString();
    }

    private static string RenderClassContent(SubclassData subclassData)
    {
        return $$"""
                 {{subclassData.TypeData.Properties.Accessibility}} unsafe partial class {{subclassData.TypeData.Properties.NameGenericArguments}} : {{subclassData.Parent}}, GObject.GTypeProvider, GObject.InstanceFactory
                 {
                      {{GeneratedCodeAttribute.Render()}}
                      private static readonly GObject.Type GType = GObject.Internal.SubclassRegistrar.Register<{{subclassData.TypeData.Properties.NameGenericArguments}}, {{subclassData.Parent}}>(&ClassInit, &InstanceInit);
                      
                      {{GeneratedCodeAttribute.Render()}}
                      public static new GObject.Type GetGType() => GType;
                 
                      {{GeneratedCodeAttribute.Render()}}
                      static object GObject.InstanceFactory.Create(System.IntPtr handle, bool ownsHandle)
                      {
                          return new {{subclassData.TypeData.Properties.NameGenericArguments}}(new {{subclassData.ParentHandle}}(handle, ownsHandle));
                      }
                      
                      {{GeneratedCodeAttribute.Render()}}
                      public {{subclassData.TypeData.Properties.Name}}({{subclassData.ParentHandle}} handle) : base(handle) 
                      {
                          Initialize();
                      }
                      
                      {{GeneratedCodeAttribute.Render()}}
                      public {{subclassData.TypeData.Properties.Name}}(params GObject.ConstructArgument[] constructArguments) : this({{subclassData.ParentHandle}}.For<{{subclassData.TypeData.Properties.NameGenericArguments}}>(constructArguments)) 
                      {
                          //Do not call 'Initialize();' here. It will be called by 'this(...)'.
                          //https://github.com/gircore/gir.core/issues/1421 
                      }
                      
                      {{GeneratedCodeAttribute.Render()}}
                      [System.Runtime.InteropServices.UnmanagedCallersOnly]
                      private static void ClassInit(System.IntPtr cls, System.IntPtr clsData)
                      {
                          var classDefinition = (GObject.Internal.ObjectClassUnmanaged*) cls;
                          classDefinition->Dispose = &Dispose;
                     
                          CompositeTemplateClassInit(cls, clsData);
                      }
                     
                      {{GeneratedCodeAttribute.Render()}}
                      [System.Runtime.InteropServices.UnmanagedCallersOnly]
                      private static void InstanceInit(System.IntPtr instance, System.IntPtr cls)
                      {
                          CompositeTemplateInstanceInit(instance, cls);
                      }
                     
                      {{GeneratedCodeAttribute.Render()}}
                      [System.Runtime.InteropServices.UnmanagedCallersOnly]
                      private static void Dispose(System.IntPtr instance)
                      {
                          CompositeTemplateDispose(instance);
                     
                          //Call into parents dispose method
                          var parentType = GObject.Internal.Functions.TypeParent(GType);
                          var parentTypeClass = (GObject.Internal.ObjectClassUnmanaged*) GObject.Internal.TypeClass.Peek(parentType).DangerousGetHandle();
                          parentTypeClass->Dispose(instance);
                      }
                      
                      /// <summary>
                      /// This method is called by all generated constructors.
                      /// Implement this partial method to initialize all members.
                      /// Decorating this method with "MemberNotNullAttribute" for
                      /// the appropriate members can remove nullable warnings.
                      /// </summary>
                      partial void Initialize();
                      
                      /// <summary>
                      /// This method is called during GObject class initialization. It is
                      /// meant to set up Gtk composite templates. The content of this method
                      /// is generated by Gtk-4.0.Integration nuget package if a class is
                      /// decorated with the [Template] attribute.
                      /// </summary>
                      /// <remarks>
                      /// Do not implement this method manually.
                      /// </remarks>
                      static partial void CompositeTemplateClassInit(System.IntPtr cls, System.IntPtr clsdata);
                     
                      /// <summary>
                      /// This method is called during GObject instance initialization. It is
                      /// meant to set up Gtk composite templates. The content of this method
                      /// is generated by Gtk-4.0.Integration nuget package if a class is
                      /// decorated with the [Template] attribute.
                      /// </summary>
                      /// <remarks>
                      /// Do not implement this method manually.
                      /// </remarks>
                      static partial void CompositeTemplateInstanceInit(System.IntPtr instance, System.IntPtr cls);
                     
                      /// <summary>
                      /// This method is called during GObject instance disposal. It is
                      /// meant to dispose Gtk composite templates. The content of this method
                      /// is generated by Gtk-4.0.Integration nuget package if a class is
                      /// decorated with the [Template] attribute.
                      /// </summary>
                      /// <remarks>
                      /// Do not implement this method manually.
                      /// </remarks>
                      static partial void CompositeTemplateDispose(System.IntPtr instance);
                 }
                 """;
    }
}
