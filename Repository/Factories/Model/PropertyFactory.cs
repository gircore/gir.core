using System;
using System.Collections.Generic;
using System.Linq;
using Repository.Factories.Model;
using Repository.Model;
using Repository.Services;
using Repository.Xml;

namespace Repository.Factories
{
    internal class PropertyFactory
    {
        private readonly TransferFactory _transferFactory;
        private readonly SymbolReferenceFactory _symbolReferenceFactory;
        private readonly CaseConverter _caseConverter;
        private readonly TypeInformationFactory _typeInformationFactory;

        public PropertyFactory(TransferFactory transferFactory, SymbolReferenceFactory symbolReferenceFactory, CaseConverter caseConverter, TypeInformationFactory typeInformationFactory)
        {
            _transferFactory = transferFactory;
            _symbolReferenceFactory = symbolReferenceFactory;
            _caseConverter = caseConverter;
            _typeInformationFactory = typeInformationFactory;
        }

        private Property Create(PropertyInfo info, NamespaceName namespaceName)
        {
            if (info.Name is null)
                throw new Exception("Property is missing a name");
            
            return new Property(
                elementName: new ElementName(info.Name),
                symbolName: new SymbolName(_caseConverter.ToPascalCase(info.Name)),
                symbolReference: _symbolReferenceFactory.Create(info, namespaceName),
                writeable: info.Writeable,
                readable: info.Readable,
                typeInformation: _typeInformationFactory.Create(info),
                transfer:  _transferFactory.FromText(info.TransferOwnership)
            );
        }

        public IEnumerable<Property> Create(IEnumerable<PropertyInfo> infos, NamespaceName namespaceName)
            => infos.Select(x => Create(x, namespaceName)).ToList();
    }
}
