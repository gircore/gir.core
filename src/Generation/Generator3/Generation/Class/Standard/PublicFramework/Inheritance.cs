namespace Generator3.Generation.Class.Standard
{
    public static class Inheritance
    {
        public static string RenderInheritance(this PublicFrameworkModel model)
        {
            return model.ParentClass is null
                ? string.Empty
                : $": {model.ParentClass.Namespace.Name + "." + model.ParentClass.Name}";
        }
    }
}
