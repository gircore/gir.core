using System;
using System.Collections.Generic;

namespace Repository.Model
{
    internal class InfoFactory
    {
        public Info CreateFromNamespaceInfo(Xml.Namespace @namespace)
        {
            if (@namespace.Name is null || @namespace.Version is null)
                throw new Exception("Can't create info because data is missing");

            return new Info(@namespace.Name, @namespace.Version);
        }

        public IEnumerable<Info> CreateFromIncludes(IEnumerable<Xml.Include> includes)
        {
            foreach (Xml.Include include in includes)
            {
                if (include.Name is null || include.Version is null)
                    throw new Exception("Can't create info because data is missing");

                yield return new Info(include.Name, include.Version);
            }
        }
    }
}
