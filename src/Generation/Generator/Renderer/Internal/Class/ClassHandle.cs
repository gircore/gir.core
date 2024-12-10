using System;
using Generator.Model;

namespace Generator.Renderer.Internal;

internal static class ClassHandle
{
    public static string Render(GirModel.Class cls)
    {
        var handleName = Class.GetInternalHandleName(cls);

        return $$"""
                  using System;
                  using System.Linq;
                  using System.Runtime.InteropServices;
                  using System.Runtime.Versioning;

                  #nullable enable

                  namespace {{Namespace.GetInternalName(cls.Namespace)}};

                  public partial class {{handleName}} : {{RenderParent(cls)}}
                  {
                     public {{handleName}}(IntPtr handle, bool ownsHandle) : base(handle, ownsHandle){ }
                  
                      public static {{RenderNewKeyword(cls)}}{{handleName}} For<T>(bool owned, GObject.ConstructArgument[] constructArguments) where T : {{Class.GetFullyQualifiedPublicName(cls)}}, GObject.GTypeProvider
                      {
                          // We can't check if a reference is floating via "g_object_is_floating" here
                          // as the function could be "lying" depending on the intent of framework writers.
                          // E.g. A Gtk.Window created via "g_object_new_with_properties" returns an unowned
                          // reference which is not marked as floating as the gtk toolkit "owns" it.
                          // For this reason we just delegate the problem to the caller and require a
                          // definition whether the ownership of the new object will be transferred to us or not.
                      
                          var ptr = GObject.Internal.Object.NewWithProperties(
                              objectType: T.GetGType(),
                              nProperties: (uint) constructArguments.Length,
                              names: constructArguments.Select(x => x.Name).ToArray(),
                              values: GObject.Internal.ValueArray2OwnedHandle.Create(constructArguments.Select(x => x.Value).ToArray())
                          );
                      
                          return new {{handleName}}(ptr, owned);
                      }
                  }
                  """;
    }

    private static string RenderParent(GirModel.Class cls)
    {
        return cls.Parent is null 
            ? throw new Exception("Class is missing parent") 
            : Class.GetFullyQualifiedInternalHandleName(cls.Parent);
    }

    private static string RenderNewKeyword(GirModel.Class cls)
    {
        //A class handle is not generated for GObject.Object. This means if
        //cls.Parent.Parent is null that this class inherits GObject.Object.
        //If it is not null this handle is a subclass handle.
        
        return cls.Parent?.Parent is null
            ? string.Empty
            : "new ";
    }

}
