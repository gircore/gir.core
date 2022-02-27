namespace Generator3.Converter
{
    public static class ConstantNameExtension
    {
        public static string GetPublicName(this GirModel.Constant constant)
        {
            return constant.Name.EscapeIdentifier();
        }
    }
}
