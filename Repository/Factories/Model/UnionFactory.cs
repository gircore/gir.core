using System;
using Repository.Analysis;
using Repository.Model;
using Repository.Services;
using Repository.Xml;

namespace Repository.Factories
{
    internal class UnionFactory
    {
        private readonly SymbolReferenceFactory _symbolReferenceFactory;
        private readonly MethodFactory _methodFactory;
        private readonly FieldFactory _fieldFactory;

        public UnionFactory(SymbolReferenceFactory symbolReferenceFactory, MethodFactory methodFactory, FieldFactory fieldFactory)
        {
            _symbolReferenceFactory = symbolReferenceFactory;
            _methodFactory = methodFactory;
            _fieldFactory = fieldFactory;
        }

        public Union Create(UnionInfo unionInfo, Namespace @namespace)
        {
            if (unionInfo.Name is null)
                throw new Exception("Union is missing a name");

            Method? getTypeFunction = unionInfo.GetTypeFunction switch
            {
                { } f => _methodFactory.CreateGetTypeMethod(f, @namespace),
                _ => null
            };

            CTypeName? cTypeName = null;
            if (unionInfo.CType is { })
                cTypeName = new CTypeName(unionInfo.CType);
            
            return new Union(
                @namespace: @namespace,
                cTypeName: cTypeName,
                typeName: new TypeName(unionInfo.Name),
                symbolName: new SymbolName(unionInfo.Name),
                methods:_methodFactory.Create(unionInfo.Methods, @namespace),
                functions: _methodFactory.Create(unionInfo.Functions, @namespace),
                getTypeFunction: getTypeFunction,
                fields: _fieldFactory.Create(unionInfo.Fields, @namespace),
                disguised: unionInfo.Disguised,
                constructors: _methodFactory.Create(unionInfo.Constructors, @namespace)
            );
        }
    }
}
