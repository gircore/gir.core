using System.Collections.Generic;
using System.Text;

namespace Generator.Model;

internal static class ArrayType
{
    public static int GetDimensions(GirModel.ArrayTypeReference arrayTypeReference)
    {
        var dimensions = 1;

        while (true)
        {
            if (arrayTypeReference.AnyTypeReference.TryPickT1(out var array, out _))
                dimensions++;
            else
                break;

            arrayTypeReference = array;
        }

        return dimensions;
    }

    public static string GetTypeName(GirModel.ArrayTypeReference arrayTypeReference, bool solveAlias = false)
    {
        while (true)
        {
            if (arrayTypeReference.AnyTypeReference.TryPickT0(out var typeReference, out var array))
            {
                if (typeReference.Type is GirModel.Alias a)
                    return solveAlias ? Type.GetName(a.Type) : Type.GetName(a);

                return Type.GetName(typeReference.Type);
            }

            arrayTypeReference = array;
        }
    }

    public static string GetName(GirModel.ArrayTypeReference arrayTypeReference, bool solveAlias = false)
    {
        var nameParts = new List<string>();
        arrayTypeReference.FillArrayNameParts(nameParts, solveAlias);

        var sb = new StringBuilder(nameParts.Count + 1);

        for (var i = nameParts.Count - 1; i >= 0; i--)
            sb.Append(nameParts[i]);

        sb.Append("[]");

        return sb.ToString();
    }

    private static void FillArrayNameParts(this GirModel.ArrayTypeReference arrayTypeReference, ICollection<string> nameParts, bool solveAlias)
    {
        while (true)
        {
            if (arrayTypeReference.AnyTypeReference.TryPickT0(out var typeReference, out var array))
            {
                if (typeReference.Type is GirModel.Alias a)
                    nameParts.Add(solveAlias ? Type.GetName(a.Type) : Type.GetName(a));
                else
                    nameParts.Add(Type.GetName(typeReference.Type));
            }
            else
            {
                nameParts.Add("[]");
                arrayTypeReference = array;
                continue;
            }

            break;
        }
    }
}
