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

    public static string GetFullyQuallifiedArgsClassName(GirModel.Signal signal, GirModel.Namespace @namespace)
    {
        return $"{Namespace.GetPublicName(@namespace)}.{GetArgsClassName(signal)}";
    }

    public static string GetDescriptorClassName(GirModel.Signal signal, GirModel.ComplexType type)
    {
        return signal.ReturnType.AnyType.Is<GirModel.Void>()
            ? $"Signal<{GetGenericArgs(signal, type)}>"
            : $"ReturningSignal<{GetGenericReturningArgs(signal, type)}>";
    }

    public static string GetDelegateName(GirModel.Signal signal, GirModel.ComplexType type)
    {
        return signal.ReturnType.AnyType.Is<GirModel.Void>()
            ? $"SignalHandler<{GetGenericArgs(signal, type)}>"
            : $"ReturningSignalHandler<{GetGenericReturningArgs(signal, type)}>";
    }

    private static string GetGenericArgs(GirModel.Signal signal, GirModel.ComplexType type)
    {
        return signal.Parameters.Any()
            ? $"{type.Name}, {GetFullyQuallifiedArgsClassName(signal, type.Namespace)}"
            : type.Name;
    }

    private static string GetGenericReturningArgs(GirModel.Signal signal, GirModel.ComplexType type)
    {
        return signal.Parameters.Any()
            ? $"{type.Name}, {GetFullyQuallifiedArgsClassName(signal, type.Namespace)}, {Renderer.Public.ReturnTypeRenderer.Render(signal.ReturnType)}"
            : $"{type.Name}, {Renderer.Public.ReturnTypeRenderer.Render(signal.ReturnType)}";
    }
}
