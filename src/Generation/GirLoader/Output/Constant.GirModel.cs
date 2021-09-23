namespace GirLoader.Output
{
    public partial class Constant : GirModel.Constant
    {
        string GirModel.Constant.NamespaceName => _repository.Namespace.Name;
        string GirModel.Constant.Name => Name;
        string GirModel.Constant.Value => Value;
        GirModel.Type GirModel.Constant.Type => TypeReference.Type;
    }
}
