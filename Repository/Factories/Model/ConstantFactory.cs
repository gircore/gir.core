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
        private readonly ISymbolReferenceFactory _symbolReferenceFactory;

        public ConstantFactory(ISymbolReferenceFactory symbolReferenceFactory)
        {
            _symbolReferenceFactory = symbolReferenceFactory;
        }

        public Constant Create(ConstantInfo constantInfo)
        {
            if (constantInfo.Name is null)
                throw new Exception($"{nameof(ConstantInfo)} misses a {nameof(constantInfo.Name)}");

            if (constantInfo.Value is null)
                throw new Exception($"{nameof(ConstantInfo)} {constantInfo.Name} misses a {nameof(constantInfo.Value)}");
            
            return new Constant(
                nativeName: constantInfo.Name,
                managedName: constantInfo.Name,
                symbolReference: _symbolReferenceFactory.Create(constantInfo),
                value: constantInfo.Value
            );
        }
    }
}
