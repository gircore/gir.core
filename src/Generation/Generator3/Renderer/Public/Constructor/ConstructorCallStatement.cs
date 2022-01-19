using System;
using System.Collections.Generic;
using System.Text;
using GirModel;

namespace Generator3.Renderer.Public
{
    public static class ConstructorCallStatement
    {
        public static string Render(Model.Public.Constructor constructor, IEnumerable<string> parameterNames)
        {
            var variableName = "handle";
            var call = new StringBuilder();
            call.Append($"var {variableName} = Internal.{constructor.Class.Name}.{constructor.InternalName}(");
            call.Append(string.Join(", ", parameterNames));
            call.Append(");" + Environment.NewLine);

            var ownedRef = constructor.ReturnType.Transfer.IsOwnedRef();

            var statement = constructor.Class.IsFundamental
                ? $"new {constructor.Class.Name}({variableName})"
                : $"new {constructor.Class.Name}({variableName}, {ownedRef.ToString().ToLower()})";

            call.Append($"return {statement};");

            return call.ToString();
        }
    }
}
