using System;

namespace Generator3.Model.Native
{
    public class RecordReturnType : ReturnType
    {
        public override string NullableTypeName => _returnValue.AnyType.Match(
            type => type.GetName() + ".Handle",
            _ => throw new Exception($"{nameof(RecordReturnType)} does not support array type")
        );

        protected internal RecordReturnType(GirModel.ReturnType returnValue) : base(returnValue)
        {
            returnValue.AnyType.VerifyType<GirModel.Record>();   
        }
    }
}
