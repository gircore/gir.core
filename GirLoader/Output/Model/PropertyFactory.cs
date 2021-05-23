using System;
using System.Collections.Generic;
using System.Linq;

namespace GirLoader.Output.Model
{
    internal class PropertyFactory
    {
        private readonly TransferFactory _transferFactory;
        private readonly TypeReferenceFactory _typeReferenceFactory;
        private readonly TypeInformationFactory _typeInformationFactory;

        public PropertyFactory(TransferFactory transferFactory, TypeReferenceFactory typeReferenceFactory, TypeInformationFactory typeInformationFactory)
        {
            _transferFactory = transferFactory;
            _typeReferenceFactory = typeReferenceFactory;
            _typeInformationFactory = typeInformationFactory;
        }

        private Property Create(Input.Model.Property property, NamespaceName namespaceName)
        {
            if (property.Name is null)
                throw new Exception("Property is missing a name");

            return new Property(
                elementName: new ElementName(property.Name),
                symbolName: new SymbolName(Helper.String.ToPascalCase(property.Name)),
                typeReference: _typeReferenceFactory.Create(property, namespaceName),
                writeable: property.Writeable,
                readable: property.Readable,
                typeInformation: _typeInformationFactory.Create(property),
                transfer: _transferFactory.FromText(property.TransferOwnership)
            );
        }

        public IEnumerable<Property> Create(IEnumerable<Input.Model.Property> properties, NamespaceName namespaceName)
            => properties.Select(x => Create(x, namespaceName)).ToList();
    }
}
