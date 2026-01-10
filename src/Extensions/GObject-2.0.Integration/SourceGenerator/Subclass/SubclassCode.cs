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
        var owned = subclassData.IsInitiallyUnowned ? "false" : "true";

        return $$"""
                  {{subclassData.TypeData.Properties.Accessibility}} unsafe partial class {{subclassData.TypeData.Properties.NameGenericArguments}} : {{subclassData.Parent}}, GObject.GTypeProvider, GObject.InstanceFactory
                  {
                       {{GeneratedCodeAttribute.Render()}}
                       private static readonly GObject.Type GType = GObject.Internal.SubclassRegistrar.Register<{{subclassData.TypeData.Properties.NameGenericArguments}}, {{subclassData.Parent}}>(&ClassInit, &InstanceInit);
                       
                       /// <summary>
                       /// Return the registered GObject type for this class.
                       /// </summary>
                       {{GeneratedCodeAttribute.Render()}}
                       public static new GObject.Type GetGType() => GType;
                  
                       /// <summary>
                       /// Creates a new instance of {{subclassData.TypeData.Properties.Name}}.
                       /// </summary>
                       /// <param name="handle">A handle to the C instance.</param>
                       /// <remarks> To create a new instance call <see cref="NewWithProperties" />. 
                       /// If you want to initialize any custom dotnet properties, create a new static factory method to set those properties.
                       /// </remarks>
                       {{GeneratedCodeAttribute.Render()}}
                       internal {{subclassData.TypeData.Properties.Name}}({{subclassData.ParentHandle}} handle) : base(handle) 
                       {
                           CompositeTemplateInitialize();
                           Initialize();
                       }
                       
                       /// <summary>
                       /// Creates a new "{{subclassData.TypeData.Properties.Name}}" instance and sets the properties specified by the construct arguments.
                       /// </summary>
                       /// <param name="constructArguments">The properties to set.</param>
                       /// <returns>A new instance of "{{subclassData.TypeData.Properties.Name}}".</returns>
                       /// <remarks>Currently it is only supported to set properties which are defined in C. Any properties defined in C# are not known. This will be fixed once https://github.com/gircore/gir.core/issues/1433 is implemented.</remarks> 
                       public static new {{subclassData.TypeData.Properties.NameGenericArguments}} NewWithProperties(GObject.ConstructArgument[] constructArguments)
                       {
                            var handle = GObject.Internal.Object.NewWithProperties(
                                objectType: GetGType(),
                                nProperties: (uint) constructArguments.Length,
                                names: GLib.Internal.Utf8StringArraySizedOwnedHandle.Create(System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Select(constructArguments, x => x.Name))),
                                values: GObject.Internal.ValueArray2OwnedHandle.Create(System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Select(constructArguments, x => x.Value)))
                            );

                            return CreateIntern(handle, {{owned}});
                       }

                       {{GeneratedCodeAttribute.Render()}}
                       static object GObject.InstanceFactory.Create(System.IntPtr handle, bool ownsHandle)
                       {
                           return CreateIntern(handle, ownsHandle);
                       }

                       private static {{subclassData.TypeData.Properties.NameGenericArguments}} CreateIntern(System.IntPtr ptr, bool ownsHandle)
                       {
                           var handle = new {{subclassData.ParentHandle}}(ptr, ownsHandle);
                           var obj = new {{subclassData.TypeData.Properties.NameGenericArguments}}(handle);

                           GObject.Internal.InstanceCache.Add(handle.DangerousGetHandle(), obj);

                           return obj;
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
                       /// meant to set up Gtk composite templates.
                       /// </summary>
                       /// <remarks>
                       /// The content of this method can be generated by Gtk-4.0.Integration 
                       /// nuget package if a class is decorated with the [Gtk.Template] attribute.
                       /// </remarks>
                       static partial void CompositeTemplateClassInit(System.IntPtr cls, System.IntPtr clsData);
                      
                       /// <summary>
                       /// This method is called during GObject instance initialization. It is
                       /// meant to set up Gtk composite templates.
                       /// </summary>
                       /// <remarks>
                       /// The content of this method can be generated by Gtk-4.0.Integration 
                       /// nuget package if a class is decorated with the [Gtk.Template] attribute.
                       /// </remarks>
                       static partial void CompositeTemplateInstanceInit(System.IntPtr instance, System.IntPtr cls);
                      
                       /// <summary>
                       /// This method is called during GObject instance disposal. It is
                       /// meant to dispose Gtk composite templates.
                       /// </summary>
                       /// <remarks>
                       /// The content of this method can be generated by Gtk-4.0.Integration 
                       /// nuget package if a class is decorated with the [TGtk.emplate] attribute.
                       /// </remarks>
                       static partial void CompositeTemplateDispose(System.IntPtr instance);
                       
                       /// <summary>
                       /// This method is called during the dotnet constructor call. It is
                       /// meant to initialize composite templates.
                       /// </summary>
                       /// <remarks>
                       /// The content of this method can be generated by Gtk-4.0.Integration 
                       /// nuget package if a member is decorated with the [Gtk.Connect] attribute.
                       /// </remarks>
                       partial void CompositeTemplateInitialize();
                  }
                  """;
    }
}
