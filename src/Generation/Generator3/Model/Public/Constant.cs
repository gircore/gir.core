using Generator3.Converter;

namespace Generator3.Model.Public
{
    public class Constant
    {
        private readonly GirModel.Constant _constant;

        public string Name => _constant.GetPublicName();
        public string TypeName => _constant.Type.GetName();
        public string Value => GetValue();
        public GirModel.PlatformDependent? PlatformDependent => _constant as GirModel.PlatformDependent;

        public Constant(GirModel.Constant constant)
        {
            _constant = constant;
        }

        private string GetValue()
        {
            return _constant.Type switch
            {
                GirModel.Bitfield { Name: { } name } => $"({name}) {_constant.Value}",
                GirModel.String => "\"" + _constant.Value + "\"",
                _ => _constant.Value
            };
        }
    }
}
