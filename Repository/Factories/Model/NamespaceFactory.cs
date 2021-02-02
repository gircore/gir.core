using System.Collections.Generic;
using System.Linq;
using Repository.Analysis;
using Repository.Factories;
using Repository.Model;
using Repository.Services;
using Repository.Xml;

#nullable enable

namespace Repository
{
    public interface INamespaceFactory
    {
        (Namespace, IEnumerable<ITypeReference>) CreateFromNamespaceInfo(NamespaceInfo repoinfo);
    }

    public class NamespaceFactory : INamespaceFactory
    {
        private readonly ITypeReferenceFactory _typeReferenceFactory;
        private readonly IClassFactory _classFactory;
        private readonly IAliasFactory _aliasFactory;
        private readonly ICallbackFactory _callbackFactory;
        private readonly IEnumartionFactory _enumartionFactory;
        private readonly IInterfaceFactory _interfaceFactory;
        private readonly IRecordFactory _recordFactory;
        private readonly IMethodFactory _methodFactory;

        public NamespaceFactory(ITypeReferenceFactory typeReferenceFactory, IClassFactory classFactory, IAliasFactory aliasFactory, ICallbackFactory callbackFactory, IEnumartionFactory enumartionFactory, IInterfaceFactory interfaceFactory, IRecordFactory recordFactory, IMethodFactory methodFactory)
        {
            _typeReferenceFactory = typeReferenceFactory;
            _classFactory = classFactory;
            _aliasFactory = aliasFactory;
            _callbackFactory = callbackFactory;
            _enumartionFactory = enumartionFactory;
            _interfaceFactory = interfaceFactory;
            _recordFactory = recordFactory;
            _methodFactory = methodFactory;
        }

        public (Namespace, IEnumerable<ITypeReference>) CreateFromNamespaceInfo(NamespaceInfo namespaceInfo)
        {
            var references = new HashSet<ITypeReference>();

            var nspace = new Namespace() {Name = namespaceInfo.Name, Version  = namespaceInfo.Version};

            SetAliases(nspace, namespaceInfo.Aliases);
            SetClasses(nspace, namespaceInfo.Classes, references);
            SetCallbacks(nspace, namespaceInfo.Callbacks, references);
            SetEnumerations(nspace, namespaceInfo.Enumerations, false);
            SetEnumerations(nspace, namespaceInfo.Bitfields, true);
            SetInterfaces(nspace, namespaceInfo.Interfaces);
            SetRecords(nspace, namespaceInfo.Records, references);
            SetFunctions(nspace, namespaceInfo.Functions, references);

            return (nspace, references);
        }

        private void SetAliases(Namespace nspace, IEnumerable<AliasInfo> aliases)
        {
            nspace.Aliases = aliases.Select(alias => _aliasFactory.Create(alias)).ToList();
        }

        private void SetClasses(Namespace nspace, IEnumerable<ClassInfo> classes, HashSet<ITypeReference> references)
        {
            nspace.Classes = new List<Class>();

            foreach (var classInfo in classes)
            {
                var cls = _classFactory.Create(classInfo, nspace);
                nspace.Classes.Add(cls);
                
                AddReference(references, cls.Parent);
                AddReferences(references, cls.Implements);
            }
        }

        private void SetCallbacks(Namespace nspace, IEnumerable<CallbackInfo> callbacks, HashSet<ITypeReference> references)
        {
            nspace.Callbacks = new List<Callback>();
            foreach (CallbackInfo callbackInfo in callbacks)
            {
                var callback = _callbackFactory.Create(callbackInfo, nspace);
                nspace.Callbacks.Add(callback);
                
                AddReference(references, callback.ReturnValue.Type);
                AddReferences(references, callback.Arguments.Select(x => x.Type));
            }
        }

        private void SetEnumerations(Namespace nspace, IEnumerable<EnumInfo> enumerations, bool isBitfield)
        {
            var list = enumerations.Select(@enum => 
                _enumartionFactory.Create(@enum, nspace, isBitfield)
            ).ToList();

            if (isBitfield)
                nspace.Bitfields = list;
            else
                nspace.Enumerations = list;
        }

        private void SetInterfaces(Namespace nspace, IEnumerable<InterfaceInfo> ifaces)
        {
            nspace.Interfaces = ifaces.Select(x => _interfaceFactory.Create(x, nspace)).ToList();
        }

        private void SetRecords(Namespace nspace, IEnumerable<RecordInfo> records, HashSet<ITypeReference> references)
        {
            nspace.Records = new List<Record>();

            foreach (RecordInfo recordInfo in records)
            {
                var record = _recordFactory.Create(recordInfo, nspace);
                nspace.Records.Add(record);
                
                AddReference(references, record.GLibClassStructFor);
            }
        }

        private void SetFunctions(Namespace nspace, IEnumerable<MethodInfo> functions, HashSet<ITypeReference> references)
        {
            nspace.Functions = new List<Method>();
            foreach (MethodInfo info in functions)
            {
                var method = _methodFactory.Create(info, nspace);
                nspace.Functions.Add(method);
                
                AddReference(references, method.ReturnValue.Type);
            }
        }

        private void AddReference(HashSet<ITypeReference> references, ITypeReference? reference)
        {
            if (reference is null)
                return;

            references.Add(reference);
        }

        private void AddReferences(HashSet<ITypeReference> referencesSet, IEnumerable<ITypeReference> references)
        {
            foreach (var reference in references)
                referencesSet.Add(reference);
        }
    }
}
