using System.Text;

namespace CWrapper
{
    public static class MethodExtension
    {
        public static string Write(this Method method)
        {
            var sb = new StringBuilder();

            if(!string.IsNullOrEmpty(method.Summary))
            {
                sb.AppendLine("///<summary>");
                sb.Append(method.Summary.CommentLineByLine());
                sb.AppendLine("///</summary>");
            }

            if(method.Obsolete)
            {
                var text = "";

                if(!string.IsNullOrEmpty(method.ObsoleteSummary))
                    text = $"(\"{method.ObsoleteSummary.Replace("\n", "").Replace(@"""", @"\""")}\")";

                sb.AppendLine($"[Obsolete{text}]");
            }

            var entryPoint = string.Empty;
            if(!string.IsNullOrEmpty(method.EntryPoint))
                entryPoint = $", EntryPoint = \"{method.EntryPoint}\"";

            sb.AppendLine($"[DllImport({method.Import}{entryPoint})]");
            sb.AppendLine($"public static extern {method.ReturnType} {method.Name}({method.Parameters.Write()});");

            return sb.ToString();
        }
    }
}