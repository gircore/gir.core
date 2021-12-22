using System;

namespace GirLoader.Output
{
    internal class AliasFactory
    {
        private readonly TypeReferenceFactory _typeReferenceFactory;

        public AliasFactory(TypeReferenceFactory typeReferenceFactory)
        {
            _typeReferenceFactory = typeReferenceFactory;
        }

        public Alias Create(Input.Alias alias, Repository repository)
        {
            if (alias.Type is null)
                throw new Exception("Alias is missing a type");

            if (alias.Name is null)
                throw new Exception("Alias is missing a name");

            if (alias.For?.Name is null)
                throw new Exception($"Alias {alias.Name} is missing target");

            return new Alias(
                cType: alias.Type,
                name: alias.Name,
                typeReference: _typeReferenceFactory.CreateResolveable(alias.For.Name, alias.For.CType),
                repository: repository
            );
        }
    }
}
