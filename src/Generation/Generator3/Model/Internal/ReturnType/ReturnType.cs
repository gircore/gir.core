using System;

namespace Generator3.Model.Internal
{
    public abstract class ReturnType
    {
        public GirModel.ReturnType Model { get; }

        public abstract string NullableTypeName { get; }
        public bool IsPointer => Model.IsPointer;
        public bool IsOwnedRef => Model.Transfer switch
        {
            GirModel.Transfer.None => false,
            GirModel.Transfer.Full => true,
            GirModel.Transfer.Container => true,
            _ => throw new Exception("Unknown transfer type")
        };
        

        protected ReturnType(GirModel.ReturnType model)
        {
            Model = model;
        }

        protected string GetDefaultNullable() => Model.Nullable ? "?" : "";
    }
}
