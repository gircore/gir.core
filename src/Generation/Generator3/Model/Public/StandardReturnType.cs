namespace Generator3.Model.Public
{
    public class StandardReturnType
    {
        private readonly GirModel.ReturnType _model;

        public string NullableTypeName => _model.AnyType.Match(
            type => type.GetName() + GetDefaultNullable(),
            arrayType => arrayType.GetName() //TODO: Consider if arrayType should be supported by this class
        );

        protected internal StandardReturnType(GirModel.ReturnType model)
        {
            _model = model;
        }
        
        protected string GetDefaultNullable() => _model.Nullable ? "?" : "";
    }
}
