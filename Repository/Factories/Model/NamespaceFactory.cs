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

        public NamespaceFactory(ClassFactory classFactory, AliasFactory aliasFactory, CallbackFactory callbackFactory, EnumerationFactory enumartionFactory, InterfaceFactory interfaceFactory, RecordFactory recordFactory, MethodFactory methodFactory, ConstantFactory constantFactory)
        {
            _classFactory = classFactory;
            _aliasFactory = aliasFactory;
            _callbackFactory = callbackFactory;
            _enumartionFactory = enumartionFactory;
            _interfaceFactory = interfaceFactory;
            _recordFactory = recordFactory;
            _methodFactory = methodFactory;
            _constantFactory = constantFactory;
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
            SetClasses(nspace, namespaceInfo.Classes, nspace.Records);
            SetFunctions(nspace, namespaceInfo.Functions);
            SetUnions(nspace, namespaceInfo.Unions);
            SetConstants(nspace, namespaceInfo.Constants);
            
            return nspace;
        }

        private void SetAliases(Namespace nspace, IEnumerable<AliasInfo> aliases)
        {
            foreach (var alias in aliases)
                nspace.AddAlias(_aliasFactory.Create(alias));
        }

        private void SetClasses(Namespace nspace, IEnumerable<ClassInfo> classes, IEnumerable<Record> records)
        {
            foreach (var classInfo in classes)
            {
                var classStruct = records.FirstOrDefault(x => x.GLibClassStructFor?.Name == classInfo.Name);
                var cls = _classFactory.Create(classInfo, nspace, classStruct);
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

        private void SetUnions(Namespace nspace, IEnumerable<RecordInfo> unions)
        {
            foreach (RecordInfo unionInfo in unions)
            {
                var union = _recordFactory.Create(unionInfo, nspace);
                nspace.AddUnion(union);
            }
        }

        private void SetFunctions(Namespace nspace, IEnumerable<MethodInfo> functions)
        {
            foreach (MethodInfo info in functions)
            {
                try
                {
                    var method = _methodFactory.Create(info, nspace);
                    nspace.AddFunction(method);
                }
                catch (ArgumentFactory.VarArgsNotSupportedException ex)
                {
                    Log.Debug($"Method {info.Name} could not be created: {ex.Message}");
                }
            }
        }

        private void SetConstants(Namespace nspace, IEnumerable<ConstantInfo> constantInfos)
        {
            foreach (var info in constantInfos)
            {
                var constant = _constantFactory.Create(info);
                nspace.AddConstant(constant);
            }
        }
    }
}
