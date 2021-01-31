using System.Collections.Generic;
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
        private readonly HashSet<ITypeReference> _references;

        public NamespaceFactory(ITypeReferenceFactory typeReferenceFactory, IClassFactory classFactory)
        {
            _typeReferenceFactory = typeReferenceFactory;
            _classFactory = classFactory;
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

        private static void SetAliases(Namespace nspace, IEnumerable<AliasInfo> aliases)
        {
            nspace.Aliases = new List<Alias>();
            foreach (AliasInfo alias in aliases)
            {
                nspace.Aliases.Add(new Alias(alias.Name, alias.For!.Name));
            }
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
                nspace.Callbacks.Add(new Callback()
                {
                    Namespace = nspace,
                    NativeName = callbackInfo.Name,
                    ManagedName = callbackInfo.Name,
                    ReturnValue = new ReturnValue() { Type = CreateAndCacheReference(callbackInfo.ReturnValue) },
                    Arguments = GetArguments(callbackInfo.Parameters)
                });
            }
        }

        private List<Argument> GetArguments(ParametersInfo? parameters)
        {
            var list = new List<Argument>();

            if (parameters is null)
                return list;

            foreach (ParameterInfo arg in parameters.Parameters)
            {
                // Direction (for determining in/out/ref)
                var callerAllocates = arg.CallerAllocates;
                Direction direction = arg.Direction switch
                {
                    "in" => Direction.In,
                    "out" when callerAllocates => Direction.OutCallerAllocates,
                    "out" when !callerAllocates => Direction.OutCalleeAllocates,
                    "inout" => Direction.Ref,
                    _ => Direction.Default
                };

                // Memory Management information
                Transfer transfer = arg.TransferOwnership switch
                {
                    "none" => Transfer.None,
                    "container" => Transfer.Container,
                    "full" => Transfer.Full,
                    "floating" => Transfer.None,
                    _ => Transfer.Full // TODO: Good default value? 
                };

                list.Add(new Argument()
                {
                    Name = arg.Name,
                    Type = CreateAndCacheReference(arg),
                    Direction = direction,
                    Transfer = transfer,
                    Nullable = arg.Nullable
                });
            }

            return list;
        }

        private static void SetEnumerations(Namespace nspace, IEnumerable<EnumInfo> enumerations, bool isBitfield)
        {
            var list = new List<Enumeration>();

            foreach (EnumInfo @enum in enumerations)
            {
                list.Add(new Enumeration()
                {
                    Namespace = nspace, NativeName = @enum.Name, ManagedName = @enum.Name, HasFlags = isBitfield,
                });
            }

            if (isBitfield)
                nspace.Bitfields = list;
            else
                nspace.Enumerations = list;
        }

        private static void SetInterfaces(Namespace nspace, IEnumerable<InterfaceInfo> ifaces)
        {
            nspace.Interfaces = new List<Interface>();
            foreach (InterfaceInfo iface in ifaces)
            {
                nspace.Interfaces.Add(new Interface() {Namespace = nspace, NativeName = iface.Name, ManagedName = iface.Name});
            }
        }

        private void SetRecords(Namespace nspace, IEnumerable<RecordInfo> records)
        {
            nspace.Records = new List<Record>();
            foreach (RecordInfo @record in records)
            {
                nspace.Records.Add(new Record() {Namespace = nspace, NativeName = @record.Name, ManagedName = @record.Name, GLibClassStructFor = (record.GLibIsGTypeStructFor != null) ? CreateAndCacheReference(record.GLibIsGTypeStructFor, false) : null});
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
