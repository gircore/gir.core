using System;

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

        public Union Create(Xml.Union union, Namespace @namespace)
        {
            if (union.Name is null)
                throw new Exception("Union is missing a name");

            Method? getTypeFunction = union.GetTypeFunction switch
            {
                { } f => _methodFactory.CreateGetTypeMethod(f),
                _ => null
            };

            CTypeName? cTypeName = null;
            if (union.CType is { })
                cTypeName = new CTypeName(union.CType);

            return new Union(
                @namespace: @namespace,
                cTypeName: cTypeName,
                typeName: new TypeName(union.Name),
                symbolName: new SymbolName(union.Name),
                methods: _methodFactory.Create(union.Methods, @namespace),
                functions: _methodFactory.Create(union.Functions, @namespace),
                getTypeFunction: getTypeFunction,
                fields: _fieldFactory.Create(union.Fields, @namespace),
                disguised: union.Disguised,
                constructors: _methodFactory.Create(union.Constructors, @namespace)
            );
        }
    }
}
