using GirModel;

namespace GirLoader.PlatformSupport;

public partial class Constant : GirModel.Constant
{
    GirModel.Namespace GirModel.Constant.Namespace => _constant.Namespace;
    string GirModel.Constant.Name => _constant.Name;
    string GirModel.Constant.Value => _constant.Value;
    Type GirModel.Constant.Type => _constant.Type;
    bool GirModel.Constant.Introspectable => _constant.Introspectable;
}
