using System;
using System.Linq;

namespace GirLoader.Output
{
    internal class ClassFactory
    {
        private readonly TypeReferenceFactory _typeReferenceFactory;
        private readonly MethodFactory _methodFactory;
        private readonly PropertyFactory _propertyFactory;
        private readonly FieldFactory _fieldFactory;
        private readonly SignalFactory _signalFactory;

        public ClassFactory(TypeReferenceFactory typeReferenceFactory, MethodFactory methodFactory, PropertyFactory propertyFactory, FieldFactory fieldFactory, SignalFactory signalFactory)
        {
            _typeReferenceFactory = typeReferenceFactory;
            _methodFactory = methodFactory;
            _propertyFactory = propertyFactory;
            _fieldFactory = fieldFactory;
            _signalFactory = signalFactory;
        }

        public Class Create(Input.Class cls, Repository repository)
        {
            if (cls.Name is null)
                throw new Exception("Class is missing data");

            if (cls.GetTypeFunction is null)
                throw new Exception($"Class {cls.Name} is missing a get type function");

            CType? cTypeName = null;
            if (cls.Type is { })
                cTypeName = new CType(cls.Type);

            return new Class(
                repository: repository,
                originalName: new TypeName(cls.Name),
                name: new TypeName(cls.Name),
                cType: cTypeName,
                getTypeFunction: _methodFactory.CreateGetTypeMethod(cls.GetTypeFunction)
            )
            {
                IsFundamental = cls.Fundamental,
                Parent = CreateParentTypeReference(cls.Parent, repository.Namespace),
                Implements =  _typeReferenceFactory.Create(cls.Implements),
                Constructors = _methodFactory.Create(cls.Constructors),
                Functions = _methodFactory.Create(cls.Functions),
                Methods = _methodFactory.Create(cls.Methods),
                Signals = cls.Signals.Select(_signalFactory.Create).ToList(),
                Fields = cls.Fields.Select(x => _fieldFactory.Create(x, repository)).ToList(),
                Properties = cls.Properties.Select(_propertyFactory.Create).ToList(),
            };
        }

        private TypeReference? CreateParentTypeReference(string? parentName, Namespace @namespace)
        {
            if (parentName is { })
                return CreateTypeReference(parentName, @namespace);

            return null;
        }

        private TypeReference CreateTypeReference(string name, Namespace @namespace)
        {
            var ctype = !name.Contains(".") ? @namespace.IdentifierPrefixes + name : null;

            return _typeReferenceFactory.CreateResolveable(name, ctype);
        }
    }
}
