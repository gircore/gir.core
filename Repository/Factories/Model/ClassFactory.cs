using System;
using System.Collections.Generic;
using System.Linq;
using Repository.Analysis;
using Repository.Model;
using Repository.Services;
using Repository.Xml;

#nullable enable

namespace Repository.Factories
{
    public interface IClassFactory
    {
        Class Create(ClassInfo cls, Namespace @namespace);
    }

    public class ClassFactory : IClassFactory
    {
        private readonly ITypeReferenceFactory _typeReferenceFactory;

        public ClassFactory(ITypeReferenceFactory typeReferenceFactory)
        {
            _typeReferenceFactory = typeReferenceFactory;
        }
        
        public Class Create(ClassInfo cls, Namespace @namespace)
        {
            if (cls.Name is null || cls.TypeName is null)
                throw new Exception("Class is missing data");
            
            return new Class(
                @namespace: @namespace,
                nativeName: cls.Name,
                managedName: cls.Name,
                ctype: cls.TypeName,
                parent: _typeReferenceFactory.CreateWithNull(cls.Parent, false),
                implements: GetTypeReferences(cls.Implements).ToList()
            );
        }

        private IEnumerable<ITypeReference> GetTypeReferences(IEnumerable<ImplementInfo> implements)
        {
            foreach (ImplementInfo implement in implements)
            {
                if (implement.Name is null)
                    throw new Exception("Implement is missing a name");
                
                yield return _typeReferenceFactory.Create(implement.Name, false);
            }
        }
    }
}
