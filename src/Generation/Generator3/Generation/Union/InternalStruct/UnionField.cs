using Generator3.Model.Internal;

namespace Generator3.Generation.Union
{
    public class UnionField
    {
        private readonly Field _field;

        public string Name => _field.Name;
        public string NullableTypeName => _field.NullableTypeName;
        public string? Attribute => "[FieldOffset(0)]" + _field.Attribute;

        public UnionField(GirModel.Field field)
        {
            _field = field.CreateInternalModel();
        }

        public string Render()
        {
            return @$"{Attribute} public {NullableTypeName} {Name};";
        }
    }
}
