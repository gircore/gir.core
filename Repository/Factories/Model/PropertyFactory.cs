using System;
using System.Collections.Generic;
using System.Linq;
using Repository.Model;
using Repository.Services;
using Repository.Xml;

namespace Repository.Factories
{
    public interface IPropertyFactory
    {
        Property Create(PropertyInfo info);
        IEnumerable<Property> Create(IEnumerable<PropertyInfo> infos);
    }

    public class PropertyFactory : IPropertyFactory
    {
        private readonly ITransferFactory _transferFactory;
        private readonly ISymbolReferenceFactory _symbolReferenceFactory;
        private readonly IPascalCaseConverter _pascalCaseConverter;

        public PropertyFactory(ITransferFactory transferFactory, ISymbolReferenceFactory symbolReferenceFactory, IPascalCaseConverter pascalCaseConverter)
        {
            _transferFactory = transferFactory;
            _symbolReferenceFactory = symbolReferenceFactory;
            _pascalCaseConverter = pascalCaseConverter;
        }

        public Property Create(PropertyInfo info)
        {
            if (info.Name is null)
                throw new Exception("Property is missing a name");
            
            return new Property(
                nativeName: info.Name,
                managedName: _pascalCaseConverter.Convert(info.Name),
                symbolReference: _symbolReferenceFactory.Create(info),
                writeable: info.Writeable,
                transfer:  _transferFactory.FromText(info.TransferOwnership)
            );
        }

        public IEnumerable<Property> Create(IEnumerable<PropertyInfo> infos)
            => infos.Select(Create).ToList();
    }
}
