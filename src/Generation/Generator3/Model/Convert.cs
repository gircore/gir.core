namespace Generator3.Model
{
    public static class Convert
    {
        public static string GetConversion(Internal.Parameter from, Public.Parameter to)
        {
            return $"// from: '{from.NullableTypeName} ({from.GetType()})' to: '{to.NullableTypeName} ({to.GetType()})'";
        }
    }
}
