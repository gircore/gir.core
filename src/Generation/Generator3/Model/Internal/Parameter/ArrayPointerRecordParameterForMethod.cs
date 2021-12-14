using System;
using Generator3.Converter;

namespace Generator3.Model.Internal
{
    public class ArrayPointerRecordParameterForMethod : Parameter
    {
        private GirModel.ArrayType ArrayType => Model.AnyType.AsT1;

        public override string NullableTypeName => TypeNameConverter.PointerArray;

        public override string Attribute => ArrayType.Length is null
            ? string.Empty
            //We add 1 to the length because Methods contain an instance parameter which is not counted
            : MarshalAs.UnmanagedLpArray(sizeParamIndex: ArrayType.Length.Value + 1);

        protected internal ArrayPointerRecordParameterForMethod(GirModel.Parameter parameter) : base(parameter)
        {
            parameter.AnyType.VerifyArrayType<GirModel.Record>();
        }
    }
}
