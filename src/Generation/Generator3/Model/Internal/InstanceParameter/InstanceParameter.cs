using Generator3.Renderer.Converter;

namespace Generator3.Model.Internal
{
    public abstract class InstanceParameter
    {
        protected readonly GirModel.InstanceParameter Model;
        public abstract string NullableTypeName { get; }

        public string Name => Model.GetInternalName();
        
        protected InstanceParameter(GirModel.InstanceParameter model)
        {
            Model = model;
        }
    }
}
