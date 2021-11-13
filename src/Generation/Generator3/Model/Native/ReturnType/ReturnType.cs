namespace Generator3.Model.Native
{
    public abstract class ReturnType
    {
        protected readonly GirModel.ReturnType Model;

        public abstract string NullableTypeName { get; }

        protected ReturnType(GirModel.ReturnType model)
        {
            Model = model;
        }

        protected string GetDefaultNullable() => Model.Nullable ? "?" : "";
    }
}
