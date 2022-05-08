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
}
