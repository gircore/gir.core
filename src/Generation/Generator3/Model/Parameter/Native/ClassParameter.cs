using System;

namespace Generator3.Model.Native
{
    public class ClassParameter : Parameter
    {
        //Native classes are represented as IntPtr and should not be nullable
        public override string NullableTypeName => Model.AnyType.AsT0.GetName();

        public override string Direction => Model.GetDirection(
            @in: ParameterDirection.In,
            @out: ParameterDirection.Out,
            @outCallerAllocates: ParameterDirection.Ref,
            @inout: ParameterDirection.Ref
        );

        protected internal ClassParameter(GirModel.Parameter parameter) : base(parameter)
        {
            parameter.AnyType.VerifyType<GirModel.Class>();
        }
    }
}
