using System;
using System.Collections.Generic;
using System.Linq;
using Repository.Xml;

namespace Repository.Model
{
    internal class FieldFactory
    {
        private readonly SymbolReferenceFactory _symbolReferenceFactory;
        private readonly IdentifierConverter _identifierConverter;
        private readonly CaseConverter _caseConverter;
        private readonly CallbackFactory _callbackFactory;
        private readonly TypeInformationFactory _typeInformationFactory;

        public FieldFactory(SymbolReferenceFactory symbolReferenceFactory, IdentifierConverter identifierConverter, CaseConverter caseConverter, CallbackFactory callbackFactory, TypeInformationFactory typeInformationFactory)
        {
            _symbolReferenceFactory = symbolReferenceFactory;
            _identifierConverter = identifierConverter;
            _caseConverter = caseConverter;
            _callbackFactory = callbackFactory;
            _typeInformationFactory = typeInformationFactory;
        }

        public Field Create(FieldInfo info, Namespace @namespace)
        {
            if (info.Name is null)
                throw new Exception("Field is missing name");

            Callback? callback = null;
            if (info.Callback is not null)
                callback = _callbackFactory.Create(info.Callback, @namespace);

            return new Field(
                elementName: new ElementName(_identifierConverter.EscapeIdentifier(info.Name)),
                symbolName: new SymbolName(_caseConverter.ToPascalCase(_identifierConverter.EscapeIdentifier(info.Name))),
                symbolReference: CreateSymbolReference(info, @namespace.Name),
                callback: callback,
                typeInformation: _typeInformationFactory.Create(info),
                readable: info.Readable,
                @private: info.Private
            );
        }

        public IEnumerable<Field> Create(IEnumerable<FieldInfo> infos, Namespace @namespace)
            => infos.Select(x => Create(x, @namespace)).ToList();

        private SymbolReference CreateSymbolReference(FieldInfo field, NamespaceName currentNamespace)
        {
            if (field.Callback is null)
                return _symbolReferenceFactory.Create(field, currentNamespace);

            if (field.Callback.Name is null)
                throw new Exception($"Field {field.Name} has a callback without a name.");

            return _symbolReferenceFactory.Create(field.Callback.Name, field.Callback.Type, currentNamespace);
        }
    }
}
