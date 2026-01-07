using System;
using System.Linq;
using CompositeTemplate;
using GLib;

var application = Gtk.Application.New("org.gir.core", Gio.ApplicationFlags.FlagsNone);
application.OnActivate += (sender, args) =>
{

    var a = Bla1.New();
    
    var window = Gtk.ApplicationWindow.New((Gtk.Application) sender);
    window.Title = "Gtk4 Window";
    window.SetDefaultSize(300, 300);
    window.Child = new CompositeBoxWidget();
    window.Show();
};
return application.RunWithSynchronizationContext(null);


public unsafe partial class Bla1 : global::GObject.Object, GObject.GTypeProvider, GObject.InstanceFactory
{
    partial void Initialize()
    {
        System.Console.WriteLine("Bla1");
    }
    
     [System.CodeDom.Compiler.GeneratedCode("GirCore.GObject-2.0.Integration", "0.7.0")]
     private static readonly GObject.Type GType = GObject.Internal.SubclassRegistrar.Register<Bla1, global::GObject.Object>(&ClassInit, &InstanceInit);
     
     [System.CodeDom.Compiler.GeneratedCode("GirCore.GObject-2.0.Integration", "0.7.0")]
     public new static GObject.Type GetGType() => GType;

     [System.CodeDom.Compiler.GeneratedCode("GirCore.GObject-2.0.Integration", "0.7.0")]
     static object GObject.InstanceFactory.Create(System.IntPtr handle, bool ownsHandle)
     {
         //TODO: ownsHandle must be respected. Sometimes an instance might be owned, somteimes not
         return GetFromCache(handle);
     }
     
     [System.CodeDom.Compiler.GeneratedCode("GirCore.GObject-2.0.Integration", "0.7.0")]
     private Bla1(global::GObject.Internal.ObjectHandle handle) : base(handle) 
     {
         CompositeTemplateInitialize();
         Initialize();
     }

     public static Bla1 New(params GObject.ConstructArgument[] constructArguments)
     {
         var ptr = GObject.Internal.Object.NewWithProperties(
             objectType: GetGType(),
             nProperties: (uint) constructArguments.Length,
             names: GLib.Internal.Utf8StringArraySizedOwnedHandle.Create(constructArguments.Select(x => x.Name).ToArray()),
             values: GObject.Internal.ValueArray2OwnedHandle.Create(constructArguments.Select(x => x.Value).ToArray())
         );

         return GetFromCache(ptr);
     }


     private static Bla1 GetFromCache(System.IntPtr ptr)
     {
         //Ref to own the managed instance until we recover it from the instance cache
         //This is needed as the managed instance is not referenced by any code and
         //thus could get garbage collected.
         GObject.Internal.Object.Ref(ptr);  //TODO: The instance might already be collected here!!!
         
         if (!GObject.Internal.InstanceCache.TryGetObject(ptr, out var obj))
             throw new Exception($"Missing instance for pointer: {ptr}");
         
         //Unref to transfer ownership back to the managed instance
         GObject.Internal.Object.Unref(ptr);
         
         return (Bla1) obj;
     }
     
     [System.CodeDom.Compiler.GeneratedCode("GirCore.GObject-2.0.Integration", "0.7.0")]
     [System.Runtime.InteropServices.UnmanagedCallersOnly]
     private static void ClassInit(System.IntPtr cls, System.IntPtr clsData)
     {
         var classDefinition = (GObject.Internal.ObjectClassUnmanaged*) cls;
         classDefinition->Dispose = &Dispose;
    
         CompositeTemplateClassInit(cls, clsData);
     }
    
     [System.CodeDom.Compiler.GeneratedCode("GirCore.GObject-2.0.Integration", "0.7.0")]
     [System.Runtime.InteropServices.UnmanagedCallersOnly]
     private static void InstanceInit(System.IntPtr instance, System.IntPtr cls)
     {
         CompositeTemplateInstanceInit(instance, cls);

         if (GObject.Internal.TypeClass.Peek(GetGType()).DangerousGetHandle() != cls)
             return;
         
         var handle = new global::GObject.Internal.ObjectHandle(instance, true);
         var obj = new Bla1(handle);

         GObject.Internal.InstanceCache.Add(instance, obj);
     }
    
     [System.CodeDom.Compiler.GeneratedCode("GirCore.GObject-2.0.Integration", "0.7.0")]
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
     static partial void CompositeTemplateClassInit(System.IntPtr cls, System.IntPtr clsdata);
    
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
