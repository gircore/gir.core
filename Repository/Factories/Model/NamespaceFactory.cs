using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using Repository.Analysis;
using Repository.Factories;
using Repository.Model;
using Repository.Xml;

namespace Repository
{
    public interface INamespaceFactory
    {
        Namespace CreateFromNamespaceInfo(NamespaceInfo repoinfo);
    }

    public class NamespaceFactory : INamespaceFactory
    {
        private readonly IClassFactory _classFactory;
        private readonly IAliasFactory _aliasFactory;
        private readonly ICallbackFactory _callbackFactory;
        private readonly IEnumartionFactory _enumartionFactory;
        private readonly IInterfaceFactory _interfaceFactory;
        private readonly IRecordFactory _recordFactory;
        private readonly IMethodFactory _methodFactory;

        public NamespaceFactory(IClassFactory classFactory, IAliasFactory aliasFactory, ICallbackFactory callbackFactory, IEnumartionFactory enumartionFactory, IInterfaceFactory interfaceFactory, IRecordFactory recordFactory, IMethodFactory methodFactory)
        {
            _classFactory = classFactory;
            _aliasFactory = aliasFactory;
            _callbackFactory = callbackFactory;
            _enumartionFactory = enumartionFactory;
            _interfaceFactory = interfaceFactory;
            _recordFactory = recordFactory;
            _methodFactory = methodFactory;
        }

        public Namespace CreateFromNamespaceInfo(NamespaceInfo namespaceInfo)
        {
            if (namespaceInfo.Name is null)
                throw new Exception("Namespace does not have a name");

            if (namespaceInfo.Version is null)
                throw new Exception($"Namespace {namespaceInfo.Name} does not have version");

            if (namespaceInfo.SharedLibrary is null)
                throw new Exception($"Namespace {namespaceInfo.Name} does not provide a shared libraryinfo");

            var nspace = new Namespace(
                name: namespaceInfo.Name,
                version: namespaceInfo.Version,
                sharedLibrary: namespaceInfo.SharedLibrary
            );

            SetAliases(nspace, namespaceInfo.Aliases);
            SetClasses(nspace, namespaceInfo.Classes);
            SetCallbacks(nspace, namespaceInfo.Callbacks);
            SetEnumerations(nspace, namespaceInfo.Enumerations);
            SetBitfields(nspace, namespaceInfo.Bitfields);
            SetInterfaces(nspace, namespaceInfo.Interfaces);
            SetRecords(nspace, namespaceInfo.Records);
            SetFunctions(nspace, namespaceInfo.Functions);
            SetUnions(nspace, namespaceInfo.Unions);

            return nspace;
        }

        private void SetAliases(Namespace nspace, IEnumerable<AliasInfo> aliases)
        {
            foreach (var alias in aliases)
                nspace.AddAlias(_aliasFactory.Create(alias));
        }

        private void SetClasses(Namespace nspace, IEnumerable<ClassInfo> classes)
        {
            foreach (var classInfo in classes)
            {
                var cls = _classFactory.Create(classInfo, nspace);
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
    }
}
