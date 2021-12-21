using System;
using Generator3.Model.Public;

namespace Generator3.Renderer.Public
{
    public static class Method
    {
        public static string Render(this Model.Public.Method data)
        {
            try
            {
                //We do not need to create a method for free methods. Freeing is handled by
                //the framework via a IDisposable implementation.
                return data.IsFree()
                    ? string.Empty
                    : RenderInternal(data);
            }
            catch (Exception e)
            {
                var message = $"Did not generate method '{data.ClassName}.{data.PublicName}': {e.Message}";

                if (e is NotImplementedException)
                    Log.Debug(message);
                else
                    Log.Warning(message);

                return string.Empty;
            }
        }

        private static string RenderInternal(Model.Public.Method method)
        {
            var publicReturnType = method.ReturnType.CreatePublicModel();

            return @$"
public {publicReturnType.NullableTypeName} {method.PublicName}({method.Parameters.Render()})
{{
    {MethodConvertParameterStatements.Render(method, out var parameterNames)}
    {MethodCallStatement.Render(method, parameterNames, out var resultVariableName)}
    {MethodReturnStatement.Render(method, resultVariableName)}
}}";
        }
    }
}
