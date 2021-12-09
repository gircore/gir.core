namespace Generator3.Converter
{
    public static class CallbackNameConverter
    {
        public static string GetInternalName(this GirModel.Callback callback)
        {
            return callback.Name.ToPascalCase() + "Callback";
        }
    }
}
