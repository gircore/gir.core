using System;
using System.Collections.Generic;
using System.Linq;
using Repository.Analysis;
using Repository.Model;
using Repository.Services;
using Repository.Xml;

namespace Repository.Factories
{
    public interface IClassFactory
    {
        Class Create(ClassInfo cls, Namespace @namespace);
    }

    public class ClassFactory : IClassFactory
    {
        private readonly ITypeReferenceFactory _typeReferenceFactory;
        private readonly IMethodFactory _methodFactory;

        public ClassFactory(ITypeReferenceFactory typeReferenceFactory, IMethodFactory methodFactory)
        {
            _typeReferenceFactory = typeReferenceFactory;
            _methodFactory = methodFactory;
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
                implements: GetTypeReferences(cls.Implements),
                methods: GetMethods(cls.Methods)
            );
        }

        private IEnumerable<ITypeReference> GetTypeReferences(IEnumerable<ImplementInfo> implements)
        {
            var list = new List<ITypeReference>();
            
            foreach (ImplementInfo implement in implements)
            {
                if (implement.Name is null)
                    throw new Exception("Implement is missing a name");
                
                list.Add(_typeReferenceFactory.Create(implement.Name, false));
            }

            return list;
        }

        private IEnumerable<Method> GetMethods(IEnumerable<MethodInfo> methods)
        {
            //Call ToList() is important. If it is skipped each access to methods will create new method objects
            return methods.Select(method => _methodFactory.Create(method)).ToList();
        }
    }
}
