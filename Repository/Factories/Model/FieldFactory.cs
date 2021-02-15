using System;
using System.Collections.Generic;
using System.Linq;
using Repository.Model;
using Repository.Services;
using Repository.Xml;

namespace Repository.Factories
{
    public interface IFieldFactory
    {
        Field Create(FieldInfo info);

        IEnumerable<Field> Create(IEnumerable<FieldInfo> infos);
    }

    public class FieldFactory : IFieldFactory
    {
        private readonly ISymbolReferenceFactory _symbolReferenceFactory;
        private readonly IIdentifierConverter _identifierConverter;
        private readonly ICaseConverter _caseConverter;

        public FieldFactory(ISymbolReferenceFactory symbolReferenceFactory, IIdentifierConverter identifierConverter, ICaseConverter caseConverter)
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
