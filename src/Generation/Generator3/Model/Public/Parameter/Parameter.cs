using GirModel;

namespace Generator3.Model.Public
{
    public abstract class Parameter
    {
        public GirModel.Parameter Model { get; }

        public string Name => Model.Name;
        public abstract string NullableTypeName { get; }
        public virtual string Direction => string.Empty;
        public virtual string Attribute => string.Empty;

        public bool IsCallback => Model.AnyTypeReference.AnyType.TryPickT0(out var type, out _) && type is GirModel.Callback;
        
        public Scope Scope => Model.Scope;
        
        protected internal Parameter(GirModel.Parameter parameter)
        {
            Model = parameter;
        }

        protected string GetDefaultNullable() => Model.Nullable ? "?" : "";
    }
}
