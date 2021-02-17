using System;
using System.Collections.Generic;
using System.Linq;
using Repository.Model;
using Repository.Services;
using Repository.Xml;

namespace Repository.Factories
{
    public class PropertyFactory
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

        public Property Create(PropertyInfo info)
        {
            if (info.Name is null)
                throw new Exception("Property is missing a name");
            
            return new Property(
                nativeName: info.Name,
                managedName: _caseConverter.ToPascalCase(info.Name),
                symbolReference: _symbolReferenceFactory.Create(info),
                writeable: info.Writeable,
                transfer:  _transferFactory.FromText(info.TransferOwnership)
            );
        }

        public IEnumerable<Property> Create(IEnumerable<PropertyInfo> infos)
            => infos.Select(Create).ToList();
    }
}
