namespace Generator3.Model.Internal
{
    public class UnsignedPointerParameter : Parameter
    {
        //IntPtr can't be nullable. They can be "nulled" via IntPtr.Zero.
        public override string NullableTypeName => Model.AnyTypeReference.AnyType.AsT0.GetName();

        protected internal UnsignedPointerParameter(GirModel.Parameter parameter) : base(parameter)
        {
            parameter.AnyTypeReference.AnyType.VerifyType<GirModel.UnsignedPointer>();
        }
    }
}
