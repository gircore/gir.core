using System;
using System.Collections.Generic;
using System.Linq;
using Repository.Model;
using Repository.Services;
using Repository.Xml;

namespace Repository.Factories
{
    internal class FieldFactory
    {
        private readonly SymbolReferenceFactory _symbolReferenceFactory;
        private readonly IdentifierConverter _identifierConverter;
        private readonly CaseConverter _caseConverter;

        public FieldFactory(SymbolReferenceFactory symbolReferenceFactory, IdentifierConverter identifierConverter, CaseConverter caseConverter)
        {
            _symbolReferenceFactory = symbolReferenceFactory;
            _identifierConverter = identifierConverter;
            _caseConverter = caseConverter;
        }
        
        public Field Create(FieldInfo info)
        {
            if (info.Name is null)
                throw new Exception("Field is missing name");

            return new Field(
                nativeName: _identifierConverter.Convert(info.Name),
                managedName: _caseConverter.ToPascalCase(_identifierConverter.Convert(info.Name)),
                symbolReference: _symbolReferenceFactory.Create(info)
            );
        }

        public IEnumerable<Field> Create(IEnumerable<FieldInfo> infos)
            => infos.Select(Create).ToList();
    }
}
