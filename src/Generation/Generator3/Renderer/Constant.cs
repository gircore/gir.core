namespace Generator3.Renderer
{
    public static class Constant
    {
        public static string Get(Model.Constant constant)
            => $"public static { constant.TypeName } { constant.Name } = { constant.Value };";
    }
}
