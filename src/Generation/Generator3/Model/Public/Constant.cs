namespace Generator3.Model.Public
{
    public class Constant
    {
        private readonly GirModel.Constant _constant;

        public string Name => _constant.Name;
        public string TypeName => _constant.Type.GetName();
        public string Value => GetValue();

        public Constant(GirModel.Constant constant)
        {
            _constant = constant;
        }

        private string GetValue()
        {
            return _constant.Type switch
            {
                GirModel.ComplexType { Name: { } name } when name.EndsWith("Flags") => $"({name}) {_constant.Value}",
                GirModel.String => "\"" + _constant.Value + "\"",
                _ => _constant.Value
            };
        }
    }
}
