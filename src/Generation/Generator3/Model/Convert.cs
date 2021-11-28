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

            return (from, to) switch
            {
                {from: Internal.StandardParameter, to: Public.StandardParameter} =>
                    builder.AppendLine($"{to.NullableTypeName} {to.Name}Conv = {from.Name};").ToString(),
                {from: Internal.ClassParameter, to: Public.ClassParameter} =>
                    builder.AppendLine($"// TODO").ToString(),
                {from: Internal.VoidParameter, to: Public.VoidParameter} =>
                    builder.AppendLine($"// TODO").ToString(),
                {to: Public.RecordParameter} =>
                    builder.AppendLine($"// TODO").ToString(),
                {from: Internal.BitfieldParameter, to: Public.BitfieldParameter} =>
                    builder.AppendLine($"{to.NullableTypeName} {to.Name}Conv = {from.Name};").ToString(),
                
                // Everything else
                _ => builder.AppendLine("// TODO (Unsupported)").ToString()
            };
            
            if (from.GetType() == typeof(Internal.StandardParameter) &&
                to.GetType() == typeof(Public.StandardParameter))
                return builder.AppendLine($"{to.NullableTypeName} {to.Name}Conv = {from.Name};").ToString();

            return builder.ToString();
        }
    }
}
