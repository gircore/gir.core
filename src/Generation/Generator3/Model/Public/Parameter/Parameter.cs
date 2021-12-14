using Generator3.Converter;
using GirModel;

namespace Generator3.Model.Public
{
    public abstract class Parameter
    {
        private string? _name;
        
        public GirModel.Parameter Model { get; }

        public string Name => _name ??= Model.GetPublicName();
        public abstract string NullableTypeName { get; }
        public virtual string Direction => string.Empty;
        public virtual string Attribute => string.Empty;

        public bool IsCallback => Model.AnyType.Is<Callback>();
        
        public Scope Scope => Model.Scope;
        
        protected internal Parameter(GirModel.Parameter parameter)
        {
            Model = parameter;
        }

        protected string GetDefaultNullable() => Model.Nullable ? "?" : "";
    }
}
