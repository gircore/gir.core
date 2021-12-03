namespace Generator3.Model.Public
{
    public class PointerReturnType : ReturnType
    {
        public override string NullableTypeName => TypeMapping.Pointer;

        protected internal PointerReturnType(GirModel.ReturnType model) : base(model)
        {
        }
    }
}
