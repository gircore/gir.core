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

    OneOf.OneOf<GirModel.AnyType, GirModel.VarArgs> GirModel.Parameter.AnyTypeOrVarArgs => AnyTypeReferenceOrVarArgs.Match<OneOf.OneOf<GirModel.AnyType, GirModel.VarArgs>>(
        anyTypeReference => anyTypeReference.Match(
            typeReference => GirModel.AnyType.From(typeReference.GetResolvedType()),
            arrayTypeReference => GirModel.AnyType.From(arrayTypeReference)
        ),
        varargs => varargs
    );
    GirModel.Direction GirModel.Parameter.Direction => Direction.ToGirModel();
    GirModel.Transfer GirModel.Parameter.Transfer => Transfer.ToGirModel();
    GirModel.Scope? GirModel.Parameter.Scope => CallbackScope.ToGirModel();
}
