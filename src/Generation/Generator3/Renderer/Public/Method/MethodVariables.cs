namespace Generator3.Renderer.Public
{
    public static class MethodVariables
    {
        public static string GetMethodReturnVariable(this GirModel.ReturnType data)
        {
            return "result";
        }

        public static string GetMethodParameterVariable(this Model.Public.Parameter parameter)
        {
            return parameter.Name + "Native";
        }
    }
}
