namespace Generator3.Generation.Code
{
    public abstract class Parameter
    {
        protected readonly GirModel.Parameter _parameter;

        public string Name => _parameter.Name;
        public abstract string NullableTypeName { get; }
        public abstract string Direction { get; }
        public string Code => $@"{ Direction }{ NullableTypeName } { Name }";

        protected Parameter(GirModel.Parameter parameter)
        {
            _parameter = parameter;
        }

        protected string GetDefaultNullable() => _parameter.Nullable ? "?" : "";
    }
}
