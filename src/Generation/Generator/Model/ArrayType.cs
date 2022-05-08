using System.Collections.Generic;
using System.Text;

namespace Generator.Model;

internal static class ArrayType
{
    public static string GetName(GirModel.ArrayType arrayType)
    {
        var nameParts = new List<string>();
        arrayType.FillArrayNameParts(nameParts);

        var sb = new StringBuilder(nameParts.Count + 1);

        for (var i = nameParts.Count - 1; i >= 0; i--)
            sb.Append(nameParts[i]);

        sb.Append("[]");

        return sb.ToString();
    }

    private static void FillArrayNameParts(this GirModel.ArrayType arrayType, ICollection<string> nameParts)
    {
        while (true)
        {
            if (arrayType.AnyType.TryPickT0(out var type, out var array))
            {
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
