using System.Linq;

namespace Generator.Model;

internal static class Signal
{
    public static string GetName(GirModel.Signal signal)
    {
        return "On" + signal.Name.ToPascalCase();
    }

    public static string GetDescriptorName(GirModel.Signal signal)
    {
        return signal.Name.ToPascalCase() + "Signal";
    }

    public static string GetArgsClassName(GirModel.Signal signal)
    {
        return signal.Name.ToPascalCase() + "SignalArgs";
    }

    public static string GetDescriptorClassName(GirModel.Signal signal, GirModel.Class cls)
    {
        return signal.ReturnType.AnyType.Is<GirModel.Void>()
            ? $"Signal<{GetGenericArgs(signal, cls)}>"
            : $"ReturningSignal<{GetGenericReturningArgs(signal, cls)}>";
    }

    public static string GetDelegateName(GirModel.Signal signal, GirModel.Class cls)
    {
        return signal.ReturnType.AnyType.Is<GirModel.Void>()
            ? $"SignalHandler<{GetGenericArgs(signal, cls)}>"
            : $"ReturningSignalHandler<{GetGenericReturningArgs(signal, cls)}>";
    }

    private static string GetGenericArgs(GirModel.Signal signal, GirModel.Class cls)
    {
        return signal.Parameters.Any()
            ? $"{cls.Name}, {GetArgsClassName(signal)}"
            : cls.Name;
    }

    private static string GetGenericReturningArgs(GirModel.Signal signal, GirModel.Class cls)
    {
        return signal.Parameters.Any()
            ? $"{cls.Name}, {GetArgsClassName(signal)}, {Renderer.Public.ReturnTypeRenderer.Render(signal.ReturnType)}"
            : $"{cls.Name}, {Renderer.Public.ReturnTypeRenderer.Render(signal.ReturnType)}";
    }
}
