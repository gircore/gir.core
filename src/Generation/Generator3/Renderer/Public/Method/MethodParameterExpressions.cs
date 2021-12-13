using System;
using System.Collections.Generic;
using System.Linq;
using Generator3.Converter;

namespace Generator3.Renderer.Public
{
    public static class MethodParameterExpressions
    {
        public static string RenderPublicParameterExpressions(this IEnumerable<Model.Public.Parameter> parameter)
        {
            return parameter
                .Select(p => p.RenderPublicParameterExpression())
                .Join(Environment.NewLine);
        }
    }
}
