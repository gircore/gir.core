using System;

namespace Generator3
{
    public static class Namespace
    {
        public static void Generate(this GirModel.Namespace @namespace)
        {
            if (@namespace.SharedLibrary is null)
                throw new Exception($"Shared library is not set for project {@namespace.GetCanonicalName()}");
            
            try
            {
                Framework.Generate(@namespace.GetCanonicalName(), @namespace.SharedLibrary, @namespace.Name);

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
