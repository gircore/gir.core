using System;
using System.Collections.Generic;

namespace GirLoader.Output
{
    internal class NamespaceFactory
    {
        private readonly ClassFactory _classFactory;
        private readonly AliasFactory _aliasFactory;
        private readonly CallbackFactory _callbackFactory;
        private readonly EnumerationFactory _enumerationFactory;
        private readonly BitfieldFactory _bitfieldFactory;
        private readonly InterfaceFactory _interfaceFactory;
        private readonly RecordFactory _recordFactory;
        private readonly MethodFactory _methodFactory;
        private readonly ConstantFactory _constantFactory;
        private readonly UnionFactory _unionFactory;

        public NamespaceFactory(ClassFactory classFactory, AliasFactory aliasFactory, CallbackFactory callbackFactory, EnumerationFactory enumerationFactory, BitfieldFactory bitfieldFactory, InterfaceFactory interfaceFactory, RecordFactory recordFactory, MethodFactory methodFactory, ConstantFactory constantFactory, UnionFactory unionFactory)
        {
            _classFactory = classFactory;
            _aliasFactory = aliasFactory;
            _callbackFactory = callbackFactory;
            _enumerationFactory = enumerationFactory;
            _bitfieldFactory = bitfieldFactory;
            _interfaceFactory = interfaceFactory;
            _recordFactory = recordFactory;
            _methodFactory = methodFactory;
            _constantFactory = constantFactory;
            _unionFactory = unionFactory;
        }

        public Namespace Create(Input.Namespace @namespace, Repository repository)
        {
            if (@namespace.Name is null)
                throw new Exception("Namespace does not have a name");

            if (@namespace.Version is null)
                throw new Exception($"Namespace {@namespace.Name} does not have version");

            var nspace = new Namespace(
                name: @namespace.Name,
                version: @namespace.Version,
                sharedLibrary: @namespace.SharedLibrary,
                identifierPrefixes: @namespace.IdentifierPrefixes,
                symbolPrefixes: @namespace.SymbolPrefixes
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

        private void SetAliases(Repository repository, IEnumerable<Input.Alias> aliases)
        {
            foreach (Input.Alias alias in aliases)
                repository.Namespace.AddAlias(_aliasFactory.Create(alias, repository));
        }

        private void SetClasses(Repository repository, IEnumerable<Input.Class> classes)
        {
            foreach (Input.Class @class in classes)
            {
                Class cls = _classFactory.Create(@class, repository);
                repository.Namespace.AddClass(cls);
            }
        }

        private void SetCallbacks(Repository repository, IEnumerable<Input.Callback> callbacks)
        {
            foreach (Input.Callback callback in callbacks)
                repository.Namespace.AddCallback(_callbackFactory.Create(callback, repository));
        }

        private void SetEnumerations(Repository repository, IEnumerable<Input.Enum> enumerations)
        {
            foreach (Input.Enum @enum in enumerations)
                repository.Namespace.AddEnumeration(_enumerationFactory.Create(@enum, repository));
        }

        private void SetBitfields(Repository repository, IEnumerable<Input.Bitfield> bitfields)
        {
            foreach (Input.Bitfield bitfield in bitfields)
                repository.Namespace.AddBitfield(_bitfieldFactory.Create(bitfield, repository));
        }

        private void SetInterfaces(Repository repository, IEnumerable<Input.Interface> interfaces)
        {
            foreach (Input.Interface @interface in interfaces)
                repository.Namespace.AddInterface(_interfaceFactory.Create(@interface, repository));
        }

        private void SetRecords(Repository repository, IEnumerable<Input.Record> records)
        {
            foreach (Input.Record record in records)
                repository.Namespace.AddRecord(_recordFactory.Create(record, repository));
        }

        private void SetUnions(Repository repository, IEnumerable<Input.Union> unions)
        {
            foreach (Input.Union union in unions)
                repository.Namespace.AddUnion(_unionFactory.Create(union, repository));
        }

        private void SetFunctions(Namespace nspace, IEnumerable<Input.Method> functions)
        {
            foreach (Method method in _methodFactory.Create(functions))
                nspace.AddFunction(method);
        }

        private void SetConstants(Namespace nspace, IEnumerable<Input.Constant> constants)
        {
            foreach (Input.Constant constant in constants)
                nspace.AddConstant(_constantFactory.Create(constant));
        }
    }
}
