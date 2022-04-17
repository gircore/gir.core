namespace Generator3.Generation.Record
{
    public static class FreeFunctionRenderer
    {
        public static string RenderFreeFunction(this InternalOwnedHandleModel model)
        {
            if (model.FreeMethod is null)
                return "";

            return $@"[DllImport(ImportResolver.Library, EntryPoint = ""{model.FreeMethod.CIdentifier}"")]
private static extern void {model.FreeMethod.Name}(IntPtr value);";
        }
    }
}
