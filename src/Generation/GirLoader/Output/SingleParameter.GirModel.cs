namespace GirLoader.Output;

public partial class SingleParameter : GirModel.Parameter
{
    string GirModel.Parameter.Name => Name;
    int? GirModel.Parameter.Closure => ClosureIndex;
    int? GirModel.Parameter.Destroy => DestroyIndex;
    bool GirModel.Parameter.IsPointer => TypeReferenceOrVarArgs.Match(
        typeReference => typeReference.CTypeReference?.IsPointer ?? false,
        varargs => false
    );
    bool GirModel.Parameter.IsConst => TypeReferenceOrVarArgs.Match(
        typeReference => typeReference.CTypeReference?.IsConst ?? false,
        varargs => false
    );
    bool GirModel.Parameter.IsVolatile => TypeReferenceOrVarArgs.Match(
        typeReference => typeReference.CTypeReference?.IsVolatile ?? false,
        varargs => false
    );

    OneOf.OneOf<GirModel.AnyType, GirModel.VarArgs> GirModel.Parameter.AnyTypeOrVarArgs => TypeReferenceOrVarArgs.Match<OneOf.OneOf<GirModel.AnyType, GirModel.VarArgs>>(
        typeReference => typeReference switch
        {
            ArrayTypeReference arrayTypeReference => GirModel.AnyType.From(arrayTypeReference),
            _ => GirModel.AnyType.From(typeReference.GetResolvedType())
        },
        varargs => varargs
    );
    GirModel.Direction GirModel.Parameter.Direction => Direction.ToGirModel();
    GirModel.Transfer GirModel.Parameter.Transfer => Transfer.ToGirModel();
    GirModel.Scope? GirModel.Parameter.Scope => CallbackScope.ToGirModel();
}
