namespace Generator3.Generation.Record
{
    public static class FreeFunctionRenderer
    {
        public static string RenderFreeFunction(this InternalHandleModel model)
        {
            if (model.FreeMethod is null)
                return "";

            return $@"[DllImport(""{model.NamespaceName}"", EntryPoint = ""{model.FreeMethod.CIdentifier}"")]
private static extern void {model.FreeMethod.Name}(IntPtr value);";
        }
    }
}
