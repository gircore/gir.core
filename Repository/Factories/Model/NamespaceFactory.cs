using System;
using System.Collections.Generic;
using System.Linq;
using Repository.Analysis;
using Repository.Factories;
using Repository.Model;
using Repository.Xml;

namespace Repository
{
    public interface INamespaceFactory
    {
        (Namespace, IEnumerable<ISymbolReference>) CreateFromNamespaceInfo(NamespaceInfo repoinfo);
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

        public (Namespace, IEnumerable<ISymbolReference>) CreateFromNamespaceInfo(NamespaceInfo namespaceInfo)
        {
            var references = new List<ISymbolReference>();

            if (namespaceInfo.Name is null)
                throw new Exception("Namespace does not have a name");

            if (namespaceInfo.Version is null)
                throw new Exception($"Namespace {namespaceInfo.Name} does not have version");
            
            var nspace = new Namespace(
                name: namespaceInfo.Name, 
                version: namespaceInfo.Version
            );

            SetAliases(nspace, namespaceInfo.Aliases);
            SetClasses(nspace, namespaceInfo.Classes, references);
            SetCallbacks(nspace, namespaceInfo.Callbacks, references);
            SetEnumerations(nspace, namespaceInfo.Enumerations);
            SetBitfields(nspace, namespaceInfo.Bitfields);
            SetInterfaces(nspace, namespaceInfo.Interfaces);
            SetRecords(nspace, namespaceInfo.Records, references);
            SetFunctions(nspace, namespaceInfo.Functions, references);

            return (nspace, references);
        }

        private void SetAliases(Namespace nspace, IEnumerable<AliasInfo> aliases)
        {
            foreach(var alias in aliases)
                nspace.AddAlias(_aliasFactory.Create(alias));
        }

        private void SetClasses(Namespace nspace, IEnumerable<ClassInfo> classes, List<ISymbolReference> references)
        {
            foreach (var classInfo in classes)
            {
                var cls = _classFactory.Create(classInfo, nspace);
                nspace.AddClass(cls);
                
                //TODO: The following calls are somehow bad code. We need to know the type references of 
                //each object in the class structure. This should somehow work automatically
                AddReference(references, cls.Parent);
                AddReferences(references, cls.Implements);
                AddReferences(references, cls.Methods.Select(x => x.ReturnValue.SymbolReference));
                AddReferences(references, cls.Methods.SelectMany(x => x.Arguments).Select(x => x.SymbolReference));
            }
        }

        private void SetCallbacks(Namespace nspace, IEnumerable<CallbackInfo> callbacks, List<ISymbolReference> references)
        {
            foreach (CallbackInfo callbackInfo in callbacks)
            {
                var callback = _callbackFactory.Create(callbackInfo, nspace);
                nspace.AddCallback(callback);
                
                AddReference(references, callback.ReturnValue.SymbolReference);
                AddReferences(references, callback.Arguments.Select(x => x.SymbolReference));
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
            foreach(var iface in ifaces)
                nspace.AddInterface(_interfaceFactory.Create(iface, nspace));
        }

        private void SetRecords(Namespace nspace, IEnumerable<RecordInfo> records, List<ISymbolReference> references)
        {
            foreach (RecordInfo recordInfo in records)
            {
                var record = _recordFactory.Create(recordInfo, nspace);
                nspace.AddRecord(record);
                
                AddReference(references, record.GLibClassStructFor);
            }
        }

        private void SetFunctions(Namespace nspace, IEnumerable<MethodInfo> functions, List<ISymbolReference> references)
        {
            foreach (MethodInfo info in functions)
            {
                try
                {
                    var method = _methodFactory.Create(info);
                    nspace.AddFunction(method);

                    AddReference(references, method.ReturnValue.SymbolReference);
                }
                catch (ArgumentFactory.VarArgsNotSupportedException ex)
                {
                    Log.Information($"Method {info.Name} could not be created: {ex.Message}");
                }
            }
        }

        private void AddReference(List<ISymbolReference> references, ISymbolReference? reference)
        {
            if (reference is null)
                return;

            references.Add(reference);
        }

        private void AddReferences(List<ISymbolReference> referencesSet, IEnumerable<ISymbolReference> references)
        {
            foreach (var reference in references)
                referencesSet.Add(reference);
        }
    }
}
