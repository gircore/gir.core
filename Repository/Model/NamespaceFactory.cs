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

        public Namespace CreateFromNamespaceInfo(Xml.Namespace @namespace)
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

            SetAliases(nspace, @namespace.Aliases);
            SetCallbacks(nspace, @namespace.Callbacks);
            SetEnumerations(nspace, @namespace.Enumerations);
            SetBitfields(nspace, @namespace.Bitfields);
            SetInterfaces(nspace, @namespace.Interfaces);
            SetRecords(nspace, @namespace.Records);
            SetClasses(nspace, @namespace.Classes);
            SetFunctions(nspace, @namespace.Functions);
            SetUnions(nspace, @namespace.Unions);
            SetConstants(nspace, @namespace.Constants);

            return nspace;
        }

        private void SetAliases(Namespace nspace, IEnumerable<Xml.Alias> aliases)
        {
            foreach (Xml.Alias alias in aliases)
                nspace.AddAlias(_aliasFactory.Create(alias, nspace));
        }

        private void SetClasses(Namespace nspace, IEnumerable<Xml.Class> classes)
        {
            foreach (Xml.Class @class in classes)
            {
                Class cls = _classFactory.Create(@class, nspace);
                nspace.AddClass(cls);
            }
        }

        private void SetCallbacks(Namespace nspace, IEnumerable<Xml.Callback> callbacks)
        {
            foreach (Xml.Callback callback in callbacks)
                nspace.AddCallback(_callbackFactory.Create(callback, nspace));
        }

        private void SetEnumerations(Namespace nspace, IEnumerable<Xml.Enum> enumerations)
        {
            foreach (Xml.Enum @enum in enumerations)
                nspace.AddEnumeration(_enumartionFactory.Create(@enum, nspace, false));
        }

        private void SetBitfields(Namespace nspace, IEnumerable<Xml.Enum> enumerations)
        {
            foreach (Xml.Enum @enum in enumerations)
                nspace.AddBitfield(_enumartionFactory.Create(@enum, nspace, true));
        }

        private void SetInterfaces(Namespace nspace, IEnumerable<Xml.Interface> interfaces)
        {
            foreach (Xml.Interface @interface in interfaces)
                nspace.AddInterface(_interfaceFactory.Create(@interface, nspace));
        }

        private void SetRecords(Namespace nspace, IEnumerable<Xml.Record> records)
        {
            foreach (Xml.Record record in records)
                nspace.AddRecord(_recordFactory.Create(record, nspace));
        }

        private void SetUnions(Namespace nspace, IEnumerable<Xml.Union> unions)
        {
            foreach (Xml.Union union in unions)
                nspace.AddUnion(_unionFactory.Create(union, nspace));
        }

        private void SetFunctions(Namespace nspace, IEnumerable<Xml.Method> functions)
        {
            foreach (Method method in _methodFactory.Create(functions, nspace))
                nspace.AddFunction(method);
        }

        private void SetConstants(Namespace nspace, IEnumerable<Xml.Constant> constants)
        {
            foreach (Xml.Constant constant in constants)
                nspace.AddConstant(_constantFactory.Create(constant, nspace.Name));
        }
    }
}
