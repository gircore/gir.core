using System.Linq;
using Generator3.Model.Public;

namespace Generator3.Renderer.Public
{
    public static class Method
    {
        public static string Render(this Model.Public.Method data)
        {
            try
            {
                return !CanRender(data) 
                    ? string.Empty 
                    : RenderInternal(data);
            }
            catch (System.Exception e)
            {
                Log.Warning($"Did not generate method '{data.ClassName}.{data.ManagedName}': {e.Message}");
                return string.Empty;
            }
        }

        private static string RenderInternal(Model.Public.Method data)
        {
            var publicReturnType = data.ReturnType.CreatePublicModel();

            return @$"
public {publicReturnType.NullableTypeName} {data.ManagedName}({data.Parameters.Render()})
{{
    {data.RenderInternalMethodCall()}
    {data.ReturnType.RenderPublicMethodReturnExpression()}
}}";
        }

        private static bool CanRender(Model.Public.Method method)
        {
            // We only support a subset of methods at the
            // moment. Determine if we can generate based on
            // the following criteria:

            if (method.Parameters.Any())
                return false;

            if (method.IsFree())
                return false;

            if (method.HasInOutRefParameter())
                return false;

            if (method.HasCallbackReturnType())
                return false;

            if (method.HasUnionParameter())
                return false;

            if (method.HasUnionReturnType())
                return false;

            if (method.HasArrayClassParameter())
                return false;

            if (method.HasArrayClassReturnType())
                return false;

            return true;
        }
    }
}
