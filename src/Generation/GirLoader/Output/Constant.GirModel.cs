namespace GirLoader.Output;

public partial class Constant : GirModel.Constant
{
    GirModel.Namespace GirModel.Constant.Namespace => _repository.Namespace;
    string GirModel.Constant.Name => Name;
    string GirModel.Constant.Value => Value;
    GirModel.Type GirModel.Constant.Type => TypeReference.GetResolvedType();
    bool GirModel.Constant.Introspectable => Introspectable;
}
