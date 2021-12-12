using System;
using System.Collections.Generic;
using System.Linq;

namespace GirLoader.Output
{
    internal class PropertyFactory
    {
        private readonly TransferFactory _transferFactory;
        private readonly TypeReferenceFactory _typeReferenceFactory;

        public PropertyFactory(TransferFactory transferFactory, TypeReferenceFactory typeReferenceFactory)
        {
            _transferFactory = transferFactory;
            _typeReferenceFactory = typeReferenceFactory;
        }

        private Property Create(Input.Property property)
        {
            if (property.Name is null)
                throw new Exception("Property is missing a name");

            return new Property(
                name: property.Name,
                typeReference: _typeReferenceFactory.Create(property),
                writeable: property.Writeable,
                readable: property.Readable,
                transfer: _transferFactory.FromText(property.TransferOwnership)
            );
        }

        public IEnumerable<Property> Create(IEnumerable<Input.Property> properties)
            => properties.Select(Create).ToList();
    }
}
