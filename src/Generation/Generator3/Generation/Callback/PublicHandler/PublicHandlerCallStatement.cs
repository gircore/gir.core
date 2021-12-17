using System.Collections.Generic;

namespace Generator3.Generation.Callback
{
    public static class PublicHandlerCallStatement
    {
        public static string Render(PublicHandlerModel model, IEnumerable<string> parameterNames, out string resultVariableName)
        {
            resultVariableName = "managedCallbackResult";
            var call = $"managedCallback({parameterNames.Join(", ")});";
            
            if (model.InternalReturnType.Model.AnyType.Is<GirModel.Void>())
                return call;

            return $"var {resultVariableName} = " + call;
        }
    }
}
