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
        private readonly HashSet<ITypeReference> _references;

        public NamespaceFactory(ITypeReferenceFactory typeReferenceFactory, IClassFactory classFactory, IAliasFactory aliasFactory, ICallbackFactory callbackFactory, IEnumartionFactory enumartionFactory, IInterfaceFactory interfaceFactory, IRecordFactory recordFactory)
        {
            _typeReferenceFactory = typeReferenceFactory;
            _classFactory = classFactory;
            _aliasFactory = aliasFactory;
            _callbackFactory = callbackFactory;
            _enumartionFactory = enumartionFactory;
            _interfaceFactory = interfaceFactory;
            _recordFactory = recordFactory;
            _references = new HashSet<ITypeReference>();
        }

        public (Namespace, IEnumerable<ITypeReference>) CreateFromNamespaceInfo(NamespaceInfo namespaceInfo)
        {
            _references.Clear();

            var nspace = new Namespace() {Name = namespaceInfo.Name, Version  = namespaceInfo.Version};

            SetAliases(nspace, namespaceInfo.Aliases);
            SetClasses(nspace, namespaceInfo.Classes);
            SetCallbacks(nspace, namespaceInfo.Callbacks);
            SetEnumerations(nspace, namespaceInfo.Enumerations, false);
            SetEnumerations(nspace, namespaceInfo.Bitfields, true);
            SetInterfaces(nspace, namespaceInfo.Interfaces);
            SetRecords(nspace, namespaceInfo.Records);
            SetFunctions(nspace, namespaceInfo.Functions);

            return (nspace, _references);
        }

        private void SetAliases(Namespace nspace, IEnumerable<AliasInfo> aliases)
        {
            nspace.Aliases = aliases.Select(alias => _aliasFactory.Create(alias)).ToList();
        }

        private void SetClasses(Namespace nspace, IEnumerable<ClassInfo> classes)
        {
            nspace.Classes = new List<Class>();

            foreach (var classInfo in classes)
            {
                var cls = _classFactory.Create(classInfo, nspace);
                nspace.Classes.Add(cls);
                
                AddReference(cls.Parent);
                AddReferences(cls.Implements);
            }
        }

        private void SetCallbacks(Namespace nspace, IEnumerable<CallbackInfo> callbacks)
        {
            nspace.Callbacks = new List<Callback>();
            foreach (CallbackInfo callbackInfo in callbacks)
            {
                var callback = _callbackFactory.Create(callbackInfo, nspace);
                nspace.Callbacks.Add(callback);
                
                AddReference(callback.ReturnValue.Type);
                AddReferences(callback.Arguments.Select(x => x.Type));
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

        private void SetRecords(Namespace nspace, IEnumerable<RecordInfo> records)
        {
            nspace.Records = new List<Record>();

            foreach (RecordInfo recordInfo in records)
            {
                var record = _recordFactory.Create(recordInfo, nspace);
                nspace.Records.Add(record);
                
                AddReference(record.GLibClassStructFor);
            }
        }

        private void SetFunctions(Namespace nspace, IEnumerable<MethodInfo> functions)
        {
            nspace.Functions = new List<Method>();
            foreach (MethodInfo info in functions)
            {
                var returnVal = new ReturnValue() {Type = CreateAndCacheReference(info.ReturnValue)};

                nspace.Functions.Add(new Method() {ReturnValue = returnVal,});
            }
        }

        private ITypeReference CreateAndCacheReference(ITypeOrArray? typeOrArray)
        {
            var reference = _typeReferenceFactory.Create(typeOrArray);
            _references.Add(reference);

            return reference;
        }
        
        private ITypeReference CreateAndCacheReference(string type, bool isArray)
        {
            var reference = _typeReferenceFactory.Create(type, isArray);
            _references.Add(reference);

            return reference;
        }

        private void AddReference(ITypeReference? reference)
        {
            if (reference is null)
                return;

            _references.Add(reference);
        }

        private void AddReferences(IEnumerable<ITypeReference> references)
        {
            foreach (var reference in references)
                _references.Add(reference);
        }
    }
}
