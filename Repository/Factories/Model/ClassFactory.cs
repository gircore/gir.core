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
        private readonly ISymbolReferenceFactory _symbolReferenceFactory;
        private readonly IMethodFactory _methodFactory;

        public ClassFactory(ISymbolReferenceFactory symbolReferenceFactory, IMethodFactory methodFactory)
        {
            _symbolReferenceFactory = symbolReferenceFactory;
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
                parent: _symbolReferenceFactory.CreateWithNull(cls.Parent, false),
                implements: GetTypeReferences(cls.Implements),
                methods: GetMethods(cls.Methods)
            );
        }

        private IEnumerable<ISymbolReference> GetTypeReferences(IEnumerable<ImplementInfo> implements)
        {
            var list = new List<ISymbolReference>();
            
            foreach (ImplementInfo implement in implements)
            {
                if (implement.Name is null)
                    throw new Exception("Implement is missing a name");
                
                list.Add(_symbolReferenceFactory.Create(implement.Name, false));
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
