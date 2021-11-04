namespace Generator3.Model
{
    public abstract class ReturnType
    {
        protected readonly GirModel.ReturnType _returnValue;

        public abstract string NullableTypeName { get; }

        protected ReturnType(GirModel.ReturnType returnValue)
        {
            _returnValue = returnValue;
        }

        protected string GetDefaultNullable() => _returnValue.Nullable ? "?" : "";
    }
}
