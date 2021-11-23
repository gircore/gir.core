namespace Generator3.Model.Internal
{
    public class RecordInstanceParameter : InstanceParameter
    {
        private GirModel.Record Type => (GirModel.Record) Model.Type;
        public override string NullableTypeName => Type.GetFullyQualifiedInternalHandle();
        
        public RecordInstanceParameter(GirModel.InstanceParameter instanceParameter) : base(instanceParameter)
        {
        }
    }
}
