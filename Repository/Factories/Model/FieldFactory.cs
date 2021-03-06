using System;
using System.Collections.Generic;
using System.Linq;
using Repository.Factories.Model;
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
        private readonly CallbackFactory _callbackFactory;
        private readonly ArrayFactory _arrayFactory;

        public FieldFactory(SymbolReferenceFactory symbolReferenceFactory, IdentifierConverter identifierConverter, CaseConverter caseConverter, CallbackFactory callbackFactory, ArrayFactory arrayFactory)
        {
            _symbolReferenceFactory = symbolReferenceFactory;
            _identifierConverter = identifierConverter;
            _caseConverter = caseConverter;
            _callbackFactory = callbackFactory;
            _arrayFactory = arrayFactory;
        }
        
        public Field Create(FieldInfo info, Namespace @namespace)
        {
            if (info.Name is null)
                throw new Exception("Field is missing name");

            Callback ? callback = null;
            if (info.Callback is not null)
                callback = _callbackFactory.Create(info.Callback, @namespace);
            
            return new Field(
                name: _identifierConverter.Convert(info.Name),
                managedName: _caseConverter.ToPascalCase(_identifierConverter.Convert(info.Name)),
                symbolReference: _symbolReferenceFactory.CreateFromField(info),
                callback: callback,
                array: _arrayFactory.Create(info.Array)
            );
        }

        public IEnumerable<Field> Create(IEnumerable<FieldInfo> infos, Namespace @namespace)
            => infos.Select(x => Create(x, @namespace)).ToList();
    }
}
