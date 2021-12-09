namespace Generator3.Converter
{
    public static class InstanceParameterNameConverter
    {
        public static string GetInternalName(this GirModel.InstanceParameter instanceParameter)
        {
            return instanceParameter.Name.ToCamelCase().EscapeIdentifier();
        }
        
        public static string GetPublicName(this GirModel.InstanceParameter instanceParameter)
        {
            return instanceParameter.Name.ToCamelCase().EscapeIdentifier();
        }
    }
}
