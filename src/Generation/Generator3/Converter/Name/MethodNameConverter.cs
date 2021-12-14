using System.Linq;

namespace Generator3.Converter
{
    public static class MethodNameConverter
    {
        public static string GetInternalName(this GirModel.Method method)
        {
            return method.Name.ToPascalCase().EscapeIdentifier();
        }

        public static string GetPublicName(this GirModel.Method method)
        {
            var name = method.Name.ToPascalCase().EscapeIdentifier();

            if (!method.ReturnType.AnyType.Is<GirModel.Void>() && !method.Parameters.Any() && !method.Name.ToLower().StartsWith("get"))
            {
                //This is a "getter" method. We prefix all getter methods with "Get" to avoid naming conflicts
                //with properties of the same name.
                return "Get" + name;
            }

            return name;
        }
    }
}
