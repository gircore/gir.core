namespace Generator3.Model.Public
{
    public class ClassInstanceParameter : InstanceParameter
    {
        private GirModel.Class Type => (GirModel.Class) Model.Type;

        public override string NullableTypeName => Type.GetFullyQualified();

        protected internal ClassInstanceParameter(GirModel.InstanceParameter parameter) : base(parameter)
        {
        }
    }
}
