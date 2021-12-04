﻿using System.Collections.Generic;

namespace Generator3.Model.Internal
{
    public class Constructor
    {
        private IEnumerable<Parameter>? _parameters;
        private ReturnType? _returnType;

        public GirModel.Constructor Model { get; }

        public string Name => Model.Name;
        public ReturnType ReturnType => _returnType ??= Model.ReturnType.CreateInternalModel();
        public string CIdentifier => Model.CIdentifier;

        public IEnumerable<Parameter> Parameters => _parameters ??= Model.Parameters.CreateInternalModels();

        public string NamespaceName { get; }

        public Constructor(GirModel.Constructor constructor, string namespaceName)
        {
            Model = constructor;
            NamespaceName = namespaceName;
        }
    }
}