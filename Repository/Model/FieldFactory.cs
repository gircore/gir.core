using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Model
{
    internal class FieldFactory
    {
        private readonly TypeReferenceFactory _typeReferenceFactory;
        private readonly IdentifierConverter _identifierConverter;
        private readonly CaseConverter _caseConverter;
        private readonly CallbackFactory _callbackFactory;
        private readonly TypeInformationFactory _typeInformationFactory;

        public FieldFactory(TypeReferenceFactory typeReferenceFactory, IdentifierConverter identifierConverter, CaseConverter caseConverter, CallbackFactory callbackFactory, TypeInformationFactory typeInformationFactory)
        {
            _typeReferenceFactory = typeReferenceFactory;
            _identifierConverter = identifierConverter;
            _caseConverter = caseConverter;
            _callbackFactory = callbackFactory;
            _typeInformationFactory = typeInformationFactory;
        }

        public Field Create(Xml.Field info, Repository repository)
        {
            if (info.Name is null)
                throw new Exception("Field is missing name");

            Callback? callback = null;
            if (info.Callback is not null)
                callback = _callbackFactory.Create(info.Callback, repository);

            return new Field(
                elementName: new ElementName(_identifierConverter.EscapeIdentifier(info.Name)),
                symbolName: new SymbolName(_caseConverter.ToPascalCase(_identifierConverter.EscapeIdentifier(info.Name))),
                typeReference: CreateSymbolReference(info, repository.Namespace.Name),
                callback: callback,
                typeInformation: _typeInformationFactory.Create(info),
                readable: info.Readable,
                @private: info.Private
            );
        }

        public IEnumerable<Field> Create(IEnumerable<Xml.Field> infos, Repository repository)
            => infos.Select(x => Create(x, repository)).ToList();

        private TypeReference CreateSymbolReference(Xml.Field field, NamespaceName currentNamespace)
        {
            if (field.Callback is null)
                return _typeReferenceFactory.Create(field, currentNamespace);

            if (field.Callback.Name is null)
                throw new Exception($"Field {field.Name} has a callback without a name.");

            return _typeReferenceFactory.Create(field.Callback.Name, field.Callback.Type, currentNamespace);
        }
    }
}
