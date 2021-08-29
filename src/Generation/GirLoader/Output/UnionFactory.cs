﻿using System;
using System.Linq;

namespace GirLoader.Output
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

        public Union Create(Input.Union union, Repository repository)
        {
            if (union.Name is null)
                throw new Exception("Union is missing a name");

            Method? getTypeFunction = union.GetTypeFunction switch
            {
                { } f => _methodFactory.CreateGetTypeMethod(f),
                _ => null
            };

            CType? cTypeName = null;
            if (union.CType is { })
                cTypeName = new CType(union.CType);

            return new Union(
                repository: repository,
                cType: cTypeName,
                originalName: new TypeName(union.Name),
                name: new TypeName(union.Name),
                methods: _methodFactory.Create(union.Methods),
                functions: _methodFactory.Create(union.Functions),
                getTypeFunction: getTypeFunction,
                fields: union.Fields.Select(x => _fieldFactory.Create(x, repository)).ToList(),
                disguised: union.Disguised,
                constructors: _methodFactory.Create(union.Constructors)
            );
        }
    }
}
