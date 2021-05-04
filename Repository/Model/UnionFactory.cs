using System;
using Repository.Xml;

namespace Repository.Model
{
    internal class UnionFactory
    {
        private readonly MethodFactory _methodFactory;
        private readonly FieldFactory _fieldFactory;

        public UnionFactory(MethodFactory methodFactory, FieldFactory fieldFactory)
        {
            _methodFactory = methodFactory;
            _fieldFactory = fieldFactory;
        }

        public Union Create(UnionInfo unionInfo, Namespace @namespace)
        {
            if (unionInfo.Name is null)
                throw new Exception("Union is missing a name");

            Method? getTypeFunction = unionInfo.GetTypeFunction switch
            {
                { } f => _methodFactory.CreateGetTypeMethod(f),
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
                methods: _methodFactory.Create(unionInfo.Methods, @namespace),
                functions: _methodFactory.Create(unionInfo.Functions, @namespace),
                getTypeFunction: getTypeFunction,
                fields: _fieldFactory.Create(unionInfo.Fields, @namespace),
                disguised: unionInfo.Disguised,
                constructors: _methodFactory.Create(unionInfo.Constructors, @namespace)
            );
        }
    }
}
