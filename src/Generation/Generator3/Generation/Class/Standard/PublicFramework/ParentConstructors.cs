namespace Generator3.Generation.Class.Standard
{
    public static class ParentConstructors
    {
        public static string RenderParentConstructors(this PublicFrameworkModel model)
        {
            return !model.HasParent 
                ? string.Empty 
                : $@"protected internal { model.Name }(IntPtr ptr, bool ownedRef) : base(ptr, ownedRef) {{}}
protected internal { model.Name }(params ConstructArgument[] constructArguments) : base(constructArguments) {{}}";
        }
    }
}
