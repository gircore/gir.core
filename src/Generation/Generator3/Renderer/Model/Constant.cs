namespace Generator3.Renderer
{
    public static class Constant
    {
        public static string Render(this Model.Constant constant)
            => $"public static { constant.TypeName } { constant.Name } = { constant.Value };";
    }
}
