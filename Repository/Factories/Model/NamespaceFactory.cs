using System;
using System.Collections.Generic;
using System.Linq;
using Repository.Factories;
using Repository.Model;
using Repository.Xml;

namespace Repository
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

        public Namespace CreateFromNamespaceInfo(NamespaceInfo namespaceInfo)
        {
            if (namespaceInfo.Name is null)
                throw new Exception("Namespace does not have a name");

            if (namespaceInfo.Version is null)
                throw new Exception($"Namespace {namespaceInfo.Name} does not have version");

            var nspace = new Namespace(
                name: namespaceInfo.Name,
                version: namespaceInfo.Version,
                sharedLibrary: namespaceInfo.SharedLibrary
            );

            SetAliases(nspace, namespaceInfo.Aliases);
            SetCallbacks(nspace, namespaceInfo.Callbacks);
            SetEnumerations(nspace, namespaceInfo.Enumerations);
            SetBitfields(nspace, namespaceInfo.Bitfields);
            SetInterfaces(nspace, namespaceInfo.Interfaces);
            SetRecords(nspace, namespaceInfo.Records);
            SetClasses(nspace, namespaceInfo.Classes);
            SetFunctions(nspace, namespaceInfo.Functions);
            SetUnions(nspace, namespaceInfo.Unions);
            SetConstants(nspace, namespaceInfo.Constants);
            
            return nspace;
        }

        private void SetAliases(Namespace nspace, IEnumerable<AliasInfo> aliases)
        {
            foreach (var alias in aliases)
                nspace.AddAlias(_aliasFactory.Create(alias, nspace));
        }

        private void SetClasses(Namespace nspace, IEnumerable<ClassInfo> classes)
        {
            foreach (var classInfo in classes)
            {
                Class cls = _classFactory.Create(classInfo, nspace);
                nspace.AddClass(cls);
            }
        }

        private void SetCallbacks(Namespace nspace, IEnumerable<CallbackInfo> callbacks)
        {
            foreach (CallbackInfo callbackInfo in callbacks)
            {
                var callback = _callbackFactory.Create(callbackInfo, nspace);
                nspace.AddCallback(callback);
            }
        }

        private void SetEnumerations(Namespace nspace, IEnumerable<EnumInfo> enumerations)
        {
            foreach (var enumInfo in enumerations)
                nspace.AddEnumeration(_enumartionFactory.Create(enumInfo, nspace, false));
        }

        private void SetBitfields(Namespace nspace, IEnumerable<EnumInfo> enumerations)
        {
            foreach (var enumInfo in enumerations)
                nspace.AddBitfield(_enumartionFactory.Create(enumInfo, nspace, true));
        }

        private void SetInterfaces(Namespace nspace, IEnumerable<InterfaceInfo> ifaces)
        {
            foreach (var iface in ifaces)
                nspace.AddInterface(_interfaceFactory.Create(iface, nspace));
        }

        private void SetRecords(Namespace nspace, IEnumerable<RecordInfo> records)
        {
            foreach (RecordInfo recordInfo in records)
            {
                var record = _recordFactory.Create(recordInfo, nspace);
                nspace.AddRecord(record);
            }
        }

        private void SetUnions(Namespace nspace, IEnumerable<UnionInfo> unions)
        {
            foreach (UnionInfo unionInfo in unions)
            {
                var union = _unionFactory.Create(unionInfo, nspace);
                nspace.AddUnion(union);
            }
        }

        private void SetFunctions(Namespace nspace, IEnumerable<MethodInfo> functions)
        {
            foreach (Method method in _methodFactory.Create(functions, nspace))
                nspace.AddFunction(method);
        }

        private void SetConstants(Namespace nspace, IEnumerable<ConstantInfo> constantInfos)
        {
            foreach (var info in constantInfos)
            {
                var constant = _constantFactory.Create(info, nspace.Name);
                nspace.AddConstant(constant);
            }
        }
    }
}
