namespace Generator3.Model.Internal
{
    public abstract class InstanceParameter
    {
        protected readonly GirModel.InstanceParameter Model;
        public abstract string NullableTypeName { get; }

        public string Name => Model.Name;
        
        protected InstanceParameter(GirModel.InstanceParameter model)
        {
            Model = model;
        }
    }
}
