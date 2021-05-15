using System;

namespace Gir.Model
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

        public Union Create(Xml.Union union, Repository repository)
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
                repository: repository,
                cTypeName: cTypeName,
                typeName: new TypeName(union.Name),
                symbolName: new SymbolName(union.Name),
                methods: _methodFactory.Create(union.Methods, repository.Namespace.Name),
                functions: _methodFactory.Create(union.Functions, repository.Namespace.Name),
                getTypeFunction: getTypeFunction,
                fields: _fieldFactory.Create(union.Fields, repository),
                disguised: union.Disguised,
                constructors: _methodFactory.Create(union.Constructors, repository.Namespace.Name)
            );
        }
    }
}
