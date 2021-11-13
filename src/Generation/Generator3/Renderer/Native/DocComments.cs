using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator3.Renderer.Native
{
    public static class DocComments
    {
        public static string RenderComment(Model.Native.Parameter parameter) =>
            $@"/// <param name=""{parameter.Model.Name}"">Transfer ownership: {parameter.Model.Transfer} Nullable: {parameter.Model.Nullable}</param>";
        
        public static string RenderComment(this GirModel.ReturnType returnType) =>
            $@"/// <returns>Transfer ownership: {returnType.Transfer} Nullable: {returnType.Nullable}</returns>";
        
        public static string RenderComments(this IEnumerable<Model.Native.Parameter> parameters)
        {
            return parameters
                .Select(RenderComment)
                .Join(Environment.NewLine);
        }
    }
}
