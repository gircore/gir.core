namespace GirLoader.Output;

public partial class SingleParameter : GirModel.Parameter
{
    string GirModel.Parameter.Name => Name;
    int? GirModel.Parameter.Closure => ClosureIndex;
    int? GirModel.Parameter.Destroy => DestroyIndex;
    bool GirModel.Parameter.IsPointer => AnyTypeReferenceOrVarArgs.Match(
        anyTypeReference => anyTypeReference.CTypeReference?.IsPointer ?? false,
        varargs => false
    );
    bool GirModel.Parameter.IsConst => AnyTypeReferenceOrVarArgs.Match(
        anyTypeReference => anyTypeReference.CTypeReference?.IsConst ?? false,
        varargs => false
    );
    bool GirModel.Parameter.IsVolatile => AnyTypeReferenceOrVarArgs.Match(
        anyTypeReference => anyTypeReference.CTypeReference?.IsVolatile ?? false,
        varargs => false
    );

    OneOf.OneOf<GirModel.AnyTypeReference, GirModel.VarArgs> GirModel.Parameter.AnyTypeReferenceOrVarArgs => AnyTypeReferenceOrVarArgs.Match<OneOf.OneOf<GirModel.AnyTypeReference, GirModel.VarArgs>>(
        anyTypeReference => anyTypeReference.Match(GirModel.AnyTypeReference.From, GirModel.AnyTypeReference.From),
        varargs => varargs
    );
    GirModel.Direction GirModel.Parameter.Direction => Direction.ToGirModel();
    GirModel.Transfer GirModel.Parameter.Transfer => Transfer.ToGirModel();
    GirModel.Scope? GirModel.Parameter.Scope => CallbackScope.ToGirModel();
}
