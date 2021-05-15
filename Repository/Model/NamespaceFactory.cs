using System;
using System.Collections.Generic;

namespace Repository.Model
{
    internal class NamespaceFactory
    {
        private readonly ClassFactory _classFactory;
        private readonly AliasFactory _aliasFactory;
        private readonly CallbackFactory _callbackFactory;
        private readonly EnumerationFactory _enumartionFactory;
        private readonly InterfaceFactory _interfaceFactory;
        private readonly RecordFactory _recordFactory;
        private readonly MethodFactory _methodFactory;
        private readonly ConstantFactory _constantFactory;
        private readonly UnionFactory _unionFactory;

        public NamespaceFactory(ClassFactory classFactory, AliasFactory aliasFactory, CallbackFactory callbackFactory, EnumerationFactory enumartionFactory, InterfaceFactory interfaceFactory, RecordFactory recordFactory, MethodFactory methodFactory, ConstantFactory constantFactory, UnionFactory unionFactory)
        {
            _classFactory = classFactory;
            _aliasFactory = aliasFactory;
            _callbackFactory = callbackFactory;
            _enumartionFactory = enumartionFactory;
            _interfaceFactory = interfaceFactory;
            _recordFactory = recordFactory;
            _methodFactory = methodFactory;
            _constantFactory = constantFactory;
            _unionFactory = unionFactory;
        }

        public Namespace Create(Xml.Namespace @namespace, Repository repository)
        {
            if (@namespace.Name is null)
                throw new Exception("Namespace does not have a name");

            if (@namespace.Version is null)
                throw new Exception($"Namespace {@namespace.Name} does not have version");

            var nspace = new Namespace(
                name: @namespace.Name,
                version: @namespace.Version,
                sharedLibrary: @namespace.SharedLibrary
            );
            
            repository.SetNamespace(nspace);

            SetAliases(repository, @namespace.Aliases);
            SetCallbacks(repository, @namespace.Callbacks);
            SetEnumerations(repository, @namespace.Enumerations);
            SetBitfields(repository, @namespace.Bitfields);
            SetInterfaces(repository, @namespace.Interfaces);
            SetRecords(repository, @namespace.Records);
            SetClasses(repository, @namespace.Classes);
            SetUnions(repository, @namespace.Unions);
            
            SetFunctions(nspace, @namespace.Functions);
            SetConstants(nspace, @namespace.Constants);

            return nspace;
        }

        private void SetAliases(Repository repository, IEnumerable<Xml.Alias> aliases)
        {
            foreach (Xml.Alias alias in aliases)
                repository.Namespace.AddAlias(_aliasFactory.Create(alias, repository));
        }

        private void SetClasses(Repository repository, IEnumerable<Xml.Class> classes)
        {
            foreach (Xml.Class @class in classes)
            {
                Class cls = _classFactory.Create(@class, repository);
                repository.Namespace.AddClass(cls);
            }
        }

        private void SetCallbacks(Repository repository, IEnumerable<Xml.Callback> callbacks)
        {
            foreach (Xml.Callback callback in callbacks)
                repository.Namespace.AddCallback(_callbackFactory.Create(callback, repository));
        }

        private void SetEnumerations(Repository repository, IEnumerable<Xml.Enum> enumerations)
        {
            foreach (Xml.Enum @enum in enumerations)
                repository.Namespace.AddEnumeration(_enumartionFactory.Create(@enum, repository, false));
        }

        private void SetBitfields(Repository repository, IEnumerable<Xml.Enum> enumerations)
        {
            foreach (Xml.Enum @enum in enumerations)
                repository.Namespace.AddBitfield(_enumartionFactory.Create(@enum, repository, true));
        }

        private void SetInterfaces(Repository repository, IEnumerable<Xml.Interface> interfaces)
        {
            foreach (Xml.Interface @interface in interfaces)
                repository.Namespace.AddInterface(_interfaceFactory.Create(@interface, repository));
        }

        private void SetRecords(Repository repository, IEnumerable<Xml.Record> records)
        {
            foreach (Xml.Record record in records)
                repository.Namespace.AddRecord(_recordFactory.Create(record, repository));
        }

        private void SetUnions(Repository repository, IEnumerable<Xml.Union> unions)
        {
            foreach (Xml.Union union in unions)
                repository.Namespace.AddUnion(_unionFactory.Create(union, repository));
        }

        private void SetFunctions(Namespace nspace, IEnumerable<Xml.Method> functions)
        {
            foreach (Method method in _methodFactory.Create(functions, nspace.Name))
                nspace.AddFunction(method);
        }

        private void SetConstants(Namespace nspace, IEnumerable<Xml.Constant> constants)
        {
            foreach (Xml.Constant constant in constants)
                nspace.AddConstant(_constantFactory.Create(constant, nspace.Name));
        }
    }
}
