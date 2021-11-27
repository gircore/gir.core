using System.Text;

namespace Generator3.Model
{
    public static class Convert
    {
        public static string GetConversion(Internal.Parameter from, Public.Parameter to)
        {
            var comment = $"// from: '{from.NullableTypeName} ({from.GetType()})' to: '{to.NullableTypeName} ({to.GetType()})'";
            
            var builder = new StringBuilder();
            builder.AppendLine(comment);
            
            if (from.GetType() == typeof(Internal.StandardParameter) &&
                to.GetType() == typeof(Public.StandardParameter))
                return builder.AppendLine($"{to.NullableTypeName} {to.Name}Conv = {from.Name};").ToString();

            return builder.ToString();
        }
    }
}
