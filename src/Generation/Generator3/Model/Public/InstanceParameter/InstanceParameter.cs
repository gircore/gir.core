using Generator3.Renderer.Converter;

namespace Generator3.Model.Public
{
    public abstract class InstanceParameter
    {
        protected readonly GirModel.InstanceParameter Model;
        public abstract string NullableTypeName { get; }

        public string Name => Model.GetPublicName();
        
        protected InstanceParameter(GirModel.InstanceParameter model)
        {
            Model = model;
        }
    }
}
