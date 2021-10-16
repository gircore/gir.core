using System;

namespace Generator3.Model.Native
{
    public class PointerParameter : Parameter
    {
        //IntPtr can't be nullable. They can be "nulled" via IntPtr.Zero.
        public override string NullableTypeName => Model.AnyType.Match(
            type => type.GetName(),
            _ => throw new Exception($"{nameof(PointerParameter)} does not support array type")
        );

        public override string Direction => Model switch
        {
            { Direction: GirModel.Direction.InOut } => ParameterDirection.Ref,
            { Direction: GirModel.Direction.Out, CallerAllocates: true } => ParameterDirection.Ref,
            { Direction: GirModel.Direction.Out } => ParameterDirection.Out,
            _ => ParameterDirection.In
        };

        protected internal PointerParameter(GirModel.Parameter parameter) : base(parameter)
        {
            parameter.AnyType.VerifyType<GirModel.Pointer>();
        }
    }
}
