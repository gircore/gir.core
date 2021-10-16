using System;

namespace Generator3.Model.Native
{
    public class ClassParameter : Parameter
    {
        //Native classes are represented as IntPtr and should not be nullable
        public override string NullableTypeName => Model.AnyType.Match(
            type => type.GetName(),
            _ => throw new Exception($"{nameof(ClassParameter)} does not support array type")
        );

        public override string Direction => Model switch
        {
            { Direction: GirModel.Direction.InOut } => ParameterDirection.Ref,
            { Direction: GirModel.Direction.Out, CallerAllocates: true } => ParameterDirection.Ref,
            { Direction: GirModel.Direction.Out } => ParameterDirection.Out,
            _ => ParameterDirection.In
        };

        protected internal ClassParameter(GirModel.Parameter parameter) : base(parameter)
        {
            parameter.AnyType.VerifyType<GirModel.Class>();
        }
    }
}
