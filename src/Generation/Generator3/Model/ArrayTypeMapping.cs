using System.Collections.Generic;
using System.Text;
using Generator3.Converter;

namespace Generator3.Model
{
    internal static class ArrayTypeMapping
    {
        public static string GetName(this GirModel.ArrayType arrayType)
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
                if (arrayType.AnyTypeReference.AnyType.TryPickT0(out var type, out var array))
                {
                    nameParts.Add(type.GetName());
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
}
