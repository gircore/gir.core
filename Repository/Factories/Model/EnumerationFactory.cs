using System;
using Repository.Model;
using Repository.Xml;

namespace Repository.Factories
{
    public interface IEnumartionFactory
    {
        Enumeration Create(EnumInfo @enum, Namespace @namespace, bool hasFlags);
    }

    public class EnumerationFactory : IEnumartionFactory
    {
        public Enumeration Create(EnumInfo @enum, Namespace @namespace, bool hasFlags)
        {
            if (@enum.Name is null)
                throw new Exception("Enum has no name");

            return new Enumeration(
                @namespace: @namespace,
                nativeName: @enum.Name,
                managedName: @enum.Name,
                hasFlags: hasFlags
            );
        }
    }
}
