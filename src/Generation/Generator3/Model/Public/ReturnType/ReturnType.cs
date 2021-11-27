namespace Generator3.Model.Public
{
    public abstract class ReturnType
    {
        protected readonly GirModel.ReturnType Model;

        public abstract string NullableTypeName { get; }

        protected ReturnType(GirModel.ReturnType model)
        {
            Model = model;
        }

        public bool IsVoid() => Model.AnyType.TryPickT0(out var type, out _) && type is GirModel.Void;
        
        protected string GetDefaultNullable() => Model.Nullable ? "?" : "";
    }
}
