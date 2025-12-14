using System;
using Generator.Model;

namespace Generator.Renderer.Internal;

internal static class ClassHandle
{
    public static string Render(GirModel.Class cls)
    {
        return cls.Final
            ? RenderFinalClassHandle(cls)
            : RenderStandardClassHandle(cls);
    }

    private static string RenderFinalClassHandle(GirModel.Class cls)
    {
        var handleName = Class.GetInternalHandleName(cls);
        var owned = Class.IsInitiallyUnowned(cls) ? "false" : "true";

        return $$"""
                 using System;
                 using System.Linq;
                 using System.Runtime.InteropServices;
                 using System.Runtime.Versioning;

                 #nullable enable

                 namespace {{Namespace.GetInternalName(cls.Namespace)}};

                 public partial class {{handleName}}(IntPtr handle, bool ownsHandle) : {{RenderParent(cls)}}(handle, ownsHandle)
                 {
                     public static {{handleName}} Create(GObject.ConstructArgument[] constructArguments)
                     {
                         var ptr = GObject.Internal.Object.NewWithProperties(
                             objectType: {{Class.GetFullyQualifiedInternalName(cls)}}.GetGType(),
                             nProperties: (uint) constructArguments.Length,
                             names: GLib.Internal.Utf8StringArraySizedOwnedHandle.Create(constructArguments.Select(x => x.Name).ToArray()),
                             values: GObject.Internal.ValueArray2OwnedHandle.Create(constructArguments.Select(x => x.Value).ToArray())
                         );
                     
                         return new {{handleName}}(ptr, {{owned}});
                     }
                 }
                 """;
    }

    private static string RenderStandardClassHandle(GirModel.Class cls)
    {
        var handleName = Class.GetInternalHandleName(cls);
        var owned = Class.IsInitiallyUnowned(cls) ? "false" : "true";

        return $$"""
                 using System;
                 using System.Linq;
                 using System.Runtime.InteropServices;
                 using System.Runtime.Versioning;

                 #nullable enable

                 namespace {{Namespace.GetInternalName(cls.Namespace)}};

                 public partial class {{handleName}}(IntPtr handle, bool ownsHandle) : {{RenderParent(cls)}}(handle, ownsHandle)
                 {
                     public static new {{handleName}} For<T>(GObject.ConstructArgument[] constructArguments) where T : {{Class.GetFullyQualifiedPublicName(cls)}}, GObject.GTypeProvider
                     {
                         var ptr = GObject.Internal.Object.NewWithProperties(
                             objectType: T.GetGType(),
                             nProperties: (uint) constructArguments.Length,
                             names: GLib.Internal.Utf8StringArraySizedOwnedHandle.Create(constructArguments.Select(x => x.Name).ToArray()),
                             values: GObject.Internal.ValueArray2OwnedHandle.Create(constructArguments.Select(x => x.Value).ToArray())
                         );
                     
                         return new {{handleName}}(ptr, {{owned}});
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
}
