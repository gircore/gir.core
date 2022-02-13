namespace Generator3.Converter
{
    public static class SignalNameExtension
    {
        public static string GetPublicName(this GirModel.Signal signal)
        {
            return "On" + signal.Name.ToPascalCase();
        }

        public static string GetDescriptorName(this GirModel.Signal signal)
        {
            return signal.Name.ToPascalCase() + "Signal";
        }

        public static string GetArgsClassName(this GirModel.Signal signal)
        {
            return signal.Name.ToPascalCase() + "SignalArgs";
        }
    }
}
