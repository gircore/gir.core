using System.Collections.Generic;
using Generator.Model;

namespace Generator.Renderer.Public.ReturnTypeToManagedExpressions;

internal class GLibList : ReturnTypeConverter
{
    private readonly UntypedRecord _containerOnly = new();

    public bool Supports(GirModel.AnyType type)
        => type.IsGLibList();

    public void Initialize(ReturnTypeToManagedData data, IEnumerable<ParameterToNativeData> parameters)
    {
        if (!ListType.SupportsReturnValue(data.ReturnType))
        {
            //Without a supported element type the elements can't be converted,
            //so the container itself is handed out.
            _containerOnly.Initialize(data, parameters);
            return;
        }

        data.SetExpression(fromVariableName =>
        {
            var returnType = data.ReturnType;
            ListType.TryGetElementType(returnType, out var elementType);

            var createElement = ListElement.RenderCreate(elementType!, returnType.Transfer, "data");
            var createArray = $"{fromVariableName}.ToArray(data => {createElement})";

            return returnType.Nullable
                ? $"{fromVariableName}.IsInvalid ? null : {createArray}"
                : createArray;
        });
    }
}
