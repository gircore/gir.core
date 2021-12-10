using System;

namespace GirLoader.Output
{
    internal class ConstantFactory
    {
        private readonly TypeReferenceFactory _typeReferenceFactory;

        public ConstantFactory(TypeReferenceFactory typeReferenceFactory)
        {
            _typeReferenceFactory = typeReferenceFactory;
        }

        public Constant Create(Input.Constant constant, Repository repository)
        {
            if (constant.Name is null)
                throw new Exception($"{nameof(Input.Constant)} misses a {nameof(constant.Name)}");

            if (constant.Value is null)
                throw new Exception($"{nameof(Input.Constant)} {constant.Name} misses a {nameof(constant.Value)}");

            return new Constant(
                repository: repository,
                originalName: constant.Name,
                typeReference: _typeReferenceFactory.Create(constant),
                value: constant.Value
            );
        }
    }
}
