using Generator3.Renderer.Converter;

namespace Generator3.Model.Internal
{
    public abstract class Parameter
    {
        private string? _name;
        
        public GirModel.Parameter Model { get; }

        public string Name => _name ??= Model.GetInternalName();
        public abstract string NullableTypeName { get; }
        public virtual string Direction => string.Empty;
        public virtual string Attribute => string.Empty;

        protected internal Parameter(GirModel.Parameter parameter)
        {
            Model = parameter;
        }

        protected string GetDefaultNullable() => Model.Nullable ? "?" : "";
    }
}
