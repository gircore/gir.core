using System;

namespace Generator3.Model.Internal
{
    public abstract class ReturnType
    {
        public GirModel.ReturnType Model { get; }

        public abstract string NullableTypeName { get; }
        public bool IsPointer => Model.IsPointer;

        protected ReturnType(GirModel.ReturnType model)
        {
            Model = model;
        }

        protected string GetDefaultNullable() => Model.Nullable ? "?" : "";
        
        public bool IsVoid() => NullableTypeName == "void";
    }
}
