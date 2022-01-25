using Generator3.Converter;
using Generator3.Publication;

namespace Generator3
{
    public static class Namespace
    {
        public static void Generate(this GirModel.Namespace @namespace, string outputDirectory)
        {
            try
            {
                FilePublisher.TargetFolder = outputDirectory;

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
