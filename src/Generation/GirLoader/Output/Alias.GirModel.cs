namespace GirLoader.Output;

public partial class Alias : GirModel.Alias
{
    GirModel.Namespace GirModel.Alias.Namespace => Repository.Namespace;
    string GirModel.Alias.Name => Name;
    GirModel.Type GirModel.Alias.Type => TypeReference.GetResolvedType();
}
