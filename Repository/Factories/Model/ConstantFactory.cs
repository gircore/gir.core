using System;
using Repository.Model;
using Repository.Services;
using Repository.Xml;

namespace Repository.Factories
{
    public interface IConstantFactory
    {
        Constant Create(ConstantInfo constantInfo);
    }

    public class ConstantFactory : IConstantFactory
    {
        private readonly SymbolReferenceFactory _symbolReferenceFactory;
        private readonly IIdentifierConverter _identifierConverter;

        public ConstantFactory(SymbolReferenceFactory symbolReferenceFactory, IIdentifierConverter identifierConverter)
        {
            _symbolReferenceFactory = symbolReferenceFactory;
            _identifierConverter = identifierConverter;
        }

        public Constant Create(ConstantInfo constantInfo)
        {
            if (constantInfo.Name is null)
                throw new Exception($"{nameof(ConstantInfo)} misses a {nameof(constantInfo.Name)}");

            if (constantInfo.Value is null)
                throw new Exception($"{nameof(ConstantInfo)} {constantInfo.Name} misses a {nameof(constantInfo.Value)}");
            
            return new Constant(
                nativeName: _identifierConverter.Convert(constantInfo.Name),
                managedName: _identifierConverter.Convert(constantInfo.Name),
                symbolReference: _symbolReferenceFactory.Create(constantInfo),
                value: constantInfo.Value
            );
        }
    }
}
