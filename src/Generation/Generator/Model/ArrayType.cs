using System.Collections.Generic;
using System.Text;

namespace Generator.Model;

internal static class ArrayType
{
    public static int GetDimensions(GirModel.ArrayType arrayType)
    {
        var dimensions = 1;

        while (true)
        {
            if (arrayType.AnyType.TryPickT1(out var array, out _))
                dimensions++;
            else
                break;

            arrayType = array;
        }

        return dimensions;
    }

    public static string GetTypeName(GirModel.ArrayType arrayType, bool solveAlias = false)
    {
        while (true)
        {
            if (arrayType.AnyType.TryPickT0(out var type, out var array))
            {
                if (type is GirModel.Alias a)
                    return solveAlias ? Type.GetName(a.Type) : Type.GetName(a);

                return Type.GetName(type);
            }

            arrayType = array;
        }
    }

    public static string GetName(GirModel.ArrayType arrayType, bool solveAlias = false)
    {
        var nameParts = new List<string>();
        arrayType.FillArrayNameParts(nameParts, solveAlias);

        var sb = new StringBuilder(nameParts.Count + 1);

        for (var i = nameParts.Count - 1; i >= 0; i--)
            sb.Append(nameParts[i]);

        sb.Append("[]");

        return sb.ToString();
    }

    private static void FillArrayNameParts(this GirModel.ArrayType arrayType, ICollection<string> nameParts, bool solveAlias)
    {
        while (true)
        {
            if (arrayType.AnyType.TryPickT0(out var type, out var array))
            {
                if (type is GirModel.Alias a)
                    nameParts.Add(solveAlias ? Type.GetName(a.Type) : Type.GetName(a));
                else
                    nameParts.Add(Type.GetName(type));
            }
            else
            {
                nameParts.Add("[]");
                arrayType = array;
                continue;
            }

            break;
        }
    }
}
