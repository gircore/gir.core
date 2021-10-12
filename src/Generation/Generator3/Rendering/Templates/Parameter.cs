namespace Generator3.Rendering.Templates
{
    public static class Parameter
    {
        public static string Get(Generation.Model.Parameter parameter)
            => $@"{ parameter.Direction }{  parameter.NullableTypeName } { parameter.Name }";
    }
}
