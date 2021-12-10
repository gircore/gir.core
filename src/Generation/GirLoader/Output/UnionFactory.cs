using System;

namespace GirLoader.Output
{
    internal class UnionFactory
    {
        private readonly MethodFactory _methodFactory;
        private readonly FieldFactory _fieldFactory;
        private readonly ConstructorFactory _constructorFactory;
        private readonly FunctionFactory _functionFactory;

        public UnionFactory(MethodFactory methodFactory, FieldFactory fieldFactory, ConstructorFactory constructorFactory, FunctionFactory functionFactory)
        {
            _methodFactory = methodFactory;
            _fieldFactory = fieldFactory;
            _constructorFactory = constructorFactory;
            _functionFactory = functionFactory;
        }

        public Union Create(Input.Union union, Repository repository)
        {
            if (union.Name is null)
                throw new Exception("Union is missing a name");

            Function? getTypeFunction = union.GetTypeFunction switch
            {
                { } f => _functionFactory.CreateGetTypeFunction(f, repository),
                _ => null
            };

            CType? cTypeName = null;
            if (union.CType is { })
                cTypeName = new CType(union.CType);

            return new Union(
                repository: repository,
                cType: cTypeName,
                originalName: new TypeName(union.Name),
                methods: _methodFactory.Create(union.Methods),
                functions: _functionFactory.Create(union.Functions, repository),
                getTypeFunction: getTypeFunction,
                fields: _fieldFactory.Create(union.Fields, repository),
                disguised: union.Disguised,
                constructors: _constructorFactory.Create(union.Constructors)
            );
        }
    }
}
