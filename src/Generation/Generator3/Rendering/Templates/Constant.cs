namespace Generator3.Rendering.Templates
{
    public static class Constant
    {
        public static string Get(Generation.Model.Constant constant)
            => $"public static { constant.TypeName } { constant.Name } = { constant.Value };";
    }
}
