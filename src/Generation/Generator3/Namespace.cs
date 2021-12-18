using Generator3.Converter;

namespace Generator3
{
    public static class Namespace
    {
        public static void Generate(this GirModel.Namespace @namespace)
        {
            try
            {
                @namespace.GenerateFramework();

                @namespace.Enumerations.Generate();
                @namespace.Bitfields.Generate();
                @namespace.Records.Generate();
                @namespace.Unions.Generate();
                @namespace.Callbacks.Generate();
                @namespace.Constants.Generate();
                @namespace.Functions.Generate();
                @namespace.Interfaces.Generate();
                @namespace.Classes.Generate();
            }
            catch
            {
                Log.Warning($"Repository {@namespace.GetCanonicalName()} could not be generated completely.");
                throw;
            }
        }
    }
}
