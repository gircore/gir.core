using System;
using System.Collections.Generic;
using System.Linq;
using Repository.Analysis;
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

        public PropertyFactory(TransferFactory transferFactory, SymbolReferenceFactory symbolReferenceFactory, CaseConverter caseConverter)
        {
            _transferFactory = transferFactory;
            _symbolReferenceFactory = symbolReferenceFactory;
            _caseConverter = caseConverter;
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
                transfer:  _transferFactory.FromText(info.TransferOwnership)
            );
        }

        public IEnumerable<Property> Create(IEnumerable<PropertyInfo> infos, NamespaceName namespaceName)
            => infos.Select(x => Create(x, namespaceName)).ToList();
    }
}
