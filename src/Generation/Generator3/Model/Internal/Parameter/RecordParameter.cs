using System;
using Generator3.Converter;
using GirModel;

namespace Generator3.Model.Internal
{
    public class RecordParameter : Parameter
    {
        private GirModel.Record Type => (GirModel.Record) Model.AnyType.AsT0;

        //Native records are represented as SafeHandles and are not nullable
        public override string NullableTypeName => Model switch
        {
            { Direction: global::GirModel.Direction.In } => Type.GetFullyQualifiedInternalHandle(),
            { CallerAllocates: true } => Type.GetFullyQualifiedInternalHandle(),
            { CallerAllocates: false, Direction: global::GirModel.Direction.InOut, Transfer: Transfer.Full } => Type.GetFullyQualifiedInternalOwnedHandle(),
            { CallerAllocates: false, Direction: global::GirModel.Direction.InOut, Transfer: Transfer.Container } => Type.GetFullyQualifiedInternalOwnedHandle(),
            { CallerAllocates: false, Direction: global::GirModel.Direction.InOut, Transfer: Transfer.None } => Type.GetFullyQualifiedInternalUnownedHandle(),
            { CallerAllocates: false, Direction: global::GirModel.Direction.Out, Transfer: Transfer.Full } => Type.GetFullyQualifiedInternalOwnedHandle(),
            { CallerAllocates: false, Direction: global::GirModel.Direction.Out, Transfer: Transfer.Container } => Type.GetFullyQualifiedInternalOwnedHandle(),
            { CallerAllocates: false, Direction: global::GirModel.Direction.Out, Transfer: Transfer.None } => Type.GetFullyQualifiedInternalUnownedHandle(),
            _ => throw new Exception($"Can't detect parameter type: CallerAllocates={Model.CallerAllocates} Direction={Model.Direction} Transfer={Model.Transfer}")
        };

        public override string Direction => Model.GetDirection(
            @in: ParameterDirection.In,
            @out: ParameterDirection.Out,
            @outCallerAllocates: ParameterDirection.In,
            @inout: ParameterDirection.In
        );

        protected internal RecordParameter(GirModel.Parameter parameter) : base(parameter)
        {
            parameter.AnyType.VerifyType<GirModel.Record>();
        }
    }
}
