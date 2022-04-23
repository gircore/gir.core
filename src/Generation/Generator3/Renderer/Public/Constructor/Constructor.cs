using System;
using Generator3.Converter;
using Generator3.Model.Public;

namespace Generator3.Renderer.Public
{
    public static class Constructor
    {
        public static string Render(this Model.Public.Constructor constructor)
        {
            try
            {
                var newKeyWord = constructor.HidesParentConstructor ? "new " : string.Empty;

                return @$"
public static {newKeyWord}{constructor.Class.Name} {constructor.PublicName}({constructor.Parameters.Render()})
{{
    {ParametersToNativeConverter.RenderToNative(constructor.Parameters, out var parameterNames)}
    {ConstructorCallStatement.Render(constructor, parameterNames)}
}}";
            }
            catch (Exception e)
            {
                var message = $"Did not generate constructor '{constructor.Class.Name}.{constructor.PublicName}': {e.Message}";

                if (e is NotImplementedException)
                    Log.Debug(message);
                else
                    Log.Warning(message);

                return string.Empty;
            }
        }
    }
}
