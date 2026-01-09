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
        var owned = Class.IsInitiallyUnowned(cls) ? "false" : "true"; //TODO Remove? Consider rmeoving the whole method

        return $$"""
                 using System;
                 using System.Linq;
                 using System.Runtime.InteropServices;
                 using System.Runtime.Versioning;

                 #nullable enable

                 namespace {{Namespace.GetInternalName(cls.Namespace)}};

                 public partial class {{handleName}}(IntPtr handle) : {{RenderParent(cls)}}(handle);
                 """;
    }

    private static string RenderStandardClassHandle(GirModel.Class cls)
    {
        var handleName = Class.GetInternalHandleName(cls);
        var owned = Class.IsInitiallyUnowned(cls) ? "false" : "true"; //TODO Remove?

        return $$"""
                 using System;
                 using System.Linq;
                 using System.Runtime.InteropServices;
                 using System.Runtime.Versioning;

                 #nullable enable

                 namespace {{Namespace.GetInternalName(cls.Namespace)}};

                 public partial class {{handleName}}(IntPtr handle) : {{RenderParent(cls)}}(handle);
                 """;
    }

    private static string RenderParent(GirModel.Class cls)
    {
        return cls.Parent is null
            ? throw new Exception("Class is missing parent")
            : Class.GetFullyQualifiedInternalHandleName(cls.Parent);
    }
}
