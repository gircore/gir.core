using System;
using System.Collections.Generic;
using System.Linq;

namespace GirLoader.Output
{
    internal class FieldFactory
    {
        private readonly TypeReferenceFactory _typeReferenceFactory;
        private readonly CallbackFactory _callbackFactory;

        public FieldFactory(TypeReferenceFactory typeReferenceFactory, CallbackFactory callbackFactory)
        {
            _typeReferenceFactory = typeReferenceFactory;
            _callbackFactory = callbackFactory;
        }

        public Field Create(Input.Field info, Repository repository)
        {
            if (info.Name is null)
                throw new Exception("Field is missing name");

            if (info.Callback is not null)
            {
                if (info.Callback.Name is null)
                    throw new Exception($"Field {info.Name} has a callback without a name.");

                return new Field(
                    orignalName: info.Name,
                    resolveableTypeReference: _typeReferenceFactory.CreateResolveable(info.Callback.Name, info.Callback.Type),
                    callback: _callbackFactory.Create(info.Callback, repository),
                    readable: info.Readable,
                    @private: info.Private
                );
            }

            return new Field(
                orignalName: info.Name,
                typeReference: _typeReferenceFactory.Create(info),
                readable: info.Readable,
                @private: info.Private
            );
        }

        public IEnumerable<Field> Create(IEnumerable<Input.Field> infos, Repository repository)
            => infos.Select(x => Create(x, repository)).ToList();
    }
}
