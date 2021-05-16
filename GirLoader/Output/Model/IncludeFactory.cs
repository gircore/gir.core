using System;
using System.Collections.Generic;

namespace Gir.Output.Model
{
    internal class IncludeFactory
    {
        //TODO: Make this method obsolete
        public Include CreateFromNamespaceInfo(Input.Model.Namespace @namespace)
        {
            if (@namespace.Name is null || @namespace.Version is null)
                throw new Exception("Can't create info because data is missing");

            return new Include(@namespace.Name, @namespace.Version);
        }

        public Include Create(Input.Model.Include include)
        {
            if (include.Name is null || include.Version is null)
                throw new Exception("Can't create include because data is missing");

            return new Include(include.Name, include.Version);
        }
    }
}
