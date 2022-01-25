﻿using System.Collections.Generic;
using System.Linq;
using Generator3.Converter;
using Generator3.Model.Internal;

namespace Generator3.Generation.Class.Fundamental
{
    public class InternalStructModel
    {
        private readonly GirModel.Class _class;

        public string Name => _class.GetInternalStructName();
        public string NamespaceName => _class.Namespace.GetInternalName();
        public IEnumerable<Field> Fields { get; }

        public InternalStructModel(GirModel.Class @class)
        {
            _class = @class;

            Fields = @class
                .Fields
                .CreateInternalModels()
                .ToList();
        }
    }
}
