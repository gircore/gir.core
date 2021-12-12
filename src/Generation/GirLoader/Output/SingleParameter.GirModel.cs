namespace GirLoader.Output
{
    public partial class SingleParameter : GirModel.Parameter
    {
        private GirModel.AnyTypeReference? _anyTypeReference;

        string GirModel.Parameter.Name => Name;
        int? GirModel.Parameter.Closure => ClosureIndex;
        GirModel.AnyTypeReference GirModel.Parameter.AnyTypeReference => _anyTypeReference ??= new AnyTypeReference(TypeReference);
        GirModel.Direction GirModel.Parameter.Direction => Direction.ToGirModel();
        GirModel.Transfer GirModel.Parameter.Transfer => Transfer.ToGirModel();
        GirModel.Scope GirModel.Parameter.Scope => CallbackScope.ToGirModel();
    }
}
