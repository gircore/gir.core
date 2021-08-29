﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace GirLoader.Output
{
    internal class PropertyFactory
    {
        private readonly TransferFactory _transferFactory;
        private readonly TypeReferenceFactory _typeReferenceFactory;

        public PropertyFactory(TransferFactory transferFactory, TypeReferenceFactory typeReferenceFactory)
        {
            _transferFactory = transferFactory;
            _typeReferenceFactory = typeReferenceFactory;
        }

        public Property Create(Input.Property property)
        {
            if (property.Name is null)
                throw new Exception("Property is missing a name");

            return new Property(
                originalName: new SymbolName(property.Name),
                symbolName: new SymbolName(new Helper.String(property.Name).ToPascalCase()),
                typeReference: _typeReferenceFactory.Create(property),
                writeable: property.Writeable,
                readable: property.Readable,
                transfer: _transferFactory.FromText(property.TransferOwnership)
            );
        }
    }
}
