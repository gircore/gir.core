namespace Generator3.Converter
{
    public static class ConstantNameConverter
    {
        public static string GetPublicName(this GirModel.Constant constant)
        {
            return constant.Name.EscapeIdentifier();
        }
    }
}
