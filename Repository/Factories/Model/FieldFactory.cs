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

        public FieldFactory(ISymbolReferenceFactory symbolReferenceFactory)
        {
            _symbolReferenceFactory = symbolReferenceFactory;
        }
        
        public Field Create(FieldInfo info)
        {
            if (info.Name is null)
                throw new Exception("Field is missing name");

            return new Field(
                nativeName: info.Name,
                managedName: info.Name,
                symbolReference: _symbolReferenceFactory.Create(info)
            );
        }

        public IEnumerable<Field> Create(IEnumerable<FieldInfo> infos)
            => infos.Select(Create).ToList();
    }
}
