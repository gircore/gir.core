using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Repository.Model
{
    internal class RepositoryFactory
    {
        private readonly NamespaceFactory _namespaceFactory;
        private readonly IncludeFactory _includeFactory;
        private readonly Xml.Loader _xmlLoader;

        public RepositoryFactory(NamespaceFactory namespaceFactory, IncludeFactory includeFactory, Xml.Loader xmlLoader)
        {
            _namespaceFactory = namespaceFactory;
            _includeFactory = includeFactory;
            _xmlLoader = xmlLoader;
        }
        
        public Repository Create(FileInfo girFile)
        {
            Xml.Repository repository = _xmlLoader.LoadRepository(girFile);
            return Create(repository);
        }
        
        private Repository Create(Xml.Repository repository)
        {
            if (repository.Namespace is null)
                throw new Exception($"Repository does not include any {nameof(repository.Namespace)}.");
            
            var includes = repository.Includes.Select(_includeFactory.Create);
            var dependencies = includes.Select(Create);
            
            var @namespace = _namespaceFactory.CreateFromNamespace(repository.Namespace);
            
            //TODO: "Include class" is a reference to a different repository.
            //Add the Reference of this repository to the "incldue class", which makes
            //the dependencies property on the namespace obsolete.
            @namespace.SetDependencies(FlattenDependencies(dependencies));
            
            return new Repository(@namespace, includes);
        }

        private Repository Create(Include include)
        {
            return default!;
        }

        private IEnumerable<Namespace> FlattenDependencies(IEnumerable<Repository> repositories)
        {
            var data = new HashSet<Namespace>();
            
            foreach (var repository in repositories)
            {
                foreach (var ns in GetDependencies(repository.Namespace))
                    data.Add(ns);
            }

            return data;
        }
        
        private IEnumerable<Namespace> GetDependencies(Namespace nsa)
        {
            foreach (var ns in nsa.Dependencies)
                foreach (var n in GetDependencies(ns))
                    yield return n;
        }
    }
}
