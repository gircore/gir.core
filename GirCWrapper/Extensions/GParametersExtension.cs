using System.Collections.Generic;

namespace Gir
{
    public static class GirParametersExtension
    {
        public static IEnumerable<GParameter> GetParameters(this GParameters parameters)
        {
            if(parameters is null)
                yield break;
                
            if(parameters.InstanceParameter is {})
                yield return parameters.InstanceParameter;

            foreach(var parameter in parameters.Parameters)
                yield return parameter;
        }
    }
}