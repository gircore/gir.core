using System;

namespace Generator3.Model
{
    public abstract class Parameter
    {
        public GirModel.Parameter Model { get; }

        public string Name => Model.Name;
        public abstract string NullableTypeName { get; }
        public abstract string Direction { get; }
        public virtual string Attribute => string.Empty;

        protected internal Parameter(GirModel.Parameter parameter)
        {
            Model = parameter;
        }

        protected string GetDefaultNullable() => Model.Nullable ? "?" : "";
    }
}
