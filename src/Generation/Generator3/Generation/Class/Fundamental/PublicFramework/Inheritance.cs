namespace Generator3.Generation.Class.Fundamental
{
    public static class Inheritance
    {
        public static string RenderInheritance(this PublicFrameworkModel model)
        {
            return model.ParentClass is null
                ? $": Fundamental"
                : $": {model.ParentClass.GetFullyQualified()}";
        }
    }
}
