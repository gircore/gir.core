using System;
using System.Collections.Generic;
using System.Linq;

namespace GirLoader.Output.Model
{
    internal class FieldFactory
    {
        private readonly TypeReferenceFactory _typeReferenceFactory;
        private readonly CallbackFactory _callbackFactory;
        private readonly TypeInformationFactory _typeInformationFactory;

        public FieldFactory(TypeReferenceFactory typeReferenceFactory, CallbackFactory callbackFactory, TypeInformationFactory typeInformationFactory)
        {
            _typeReferenceFactory = typeReferenceFactory;
            _callbackFactory = callbackFactory;
            _typeInformationFactory = typeInformationFactory;
        }

        public Field Create(Input.Model.Field info, Repository repository)
        {
            if (info.Name is null)
                throw new Exception("Field is missing name");

            Callback? callback = null;
            if (info.Callback is not null)
                callback = _callbackFactory.Create(info.Callback, repository);

            var name = Helper.String.EscapeIdentifier(info.Name);
            
            return new Field(
                orignalName: new SymbolName(name),
                symbolName: new SymbolName(Helper.String.ToPascalCase(name)),
                typeReference: CreateSymbolReference(info, repository.Namespace.Name),
                callback: callback,
                typeInformation: _typeInformationFactory.Create(info),
                readable: info.Readable,
                @private: info.Private
            );
        }

        public IEnumerable<Field> Create(IEnumerable<Input.Model.Field> infos, Repository repository)
            => infos.Select(x => Create(x, repository)).ToList();

        private TypeReference CreateSymbolReference(Input.Model.Field field, NamespaceName currentNamespace)
        {
            if (field.Callback is null)
                return _typeReferenceFactory.Create(field, currentNamespace);

            if (field.Callback.Name is null)
                throw new Exception($"Field {field.Name} has a callback without a name.");

            return _typeReferenceFactory.Create(field.Callback.Name, field.Callback.Type, currentNamespace);
        }
    }
}
