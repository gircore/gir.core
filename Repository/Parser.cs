using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Repository.Analysis;
using Repository.Services;
using Repository.Model;
using Repository.Xml;

#nullable enable

namespace Repository
{
    public class Parser
    {
        private readonly IXmlService _xmlService;
        private readonly HashSet<TypeReference> _references;

        public Parser(IXmlService xmlService)
        {
            _xmlService = xmlService ?? throw new ArgumentNullException(nameof(xmlService));
            _references = new HashSet<TypeReference>();
        }
        
        public (Namespace, IEnumerable<TypeReference>) Parse(FileInfo girFile)
        {
            _references.Clear();
            var repoInfo = _xmlService.Deserialize<RepositoryInfo>(girFile);
            
            if (repoInfo.Namespace == null)
                throw new InvalidDataException($"File '{girFile} does not define a namespace.");

            NamespaceInfo nspaceInfo = repoInfo.Namespace;
            var nspace = new Namespace()
            {
                Name = nspaceInfo.Name, 
                Version  = nspaceInfo.Version
            };

            SetAliases(nspace, nspaceInfo.Aliases);
            SetClasses(nspace, nspaceInfo.Classes);
            SetCallbacks(nspace, nspaceInfo.Callbacks);
            SetEnumerations(nspace, nspaceInfo.Enumerations, false);
            SetEnumerations(nspace, nspaceInfo.Bitfields, true);
            SetInterfaces(nspace, nspaceInfo.Interfaces);
            SetRecords(nspace, nspaceInfo.Records);
            SetFunctions(nspace, nspaceInfo.Functions);

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
            
            foreach (ClassInfo cls in classes)
            {
                nspace.Classes.Add(new Class()
                {
                    Namespace = nspace,
                    NativeName = cls.Name,
                    ManagedName = cls.Name,
                    CType = cls.TypeName,
                    Parent = (cls.Parent != null) ? CreateReference(cls.Parent, false) : null,
                    Implements = ParseImplements(cls.Implements).ToList(),
                });
            }
        }

        private IEnumerable<TypeReference> ParseImplements(IEnumerable<ImplementInfo> implements)
        {
            foreach (ImplementInfo impl in implements)
                yield return CreateReference(impl.Name!, false);
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
                    ReturnValue = new ReturnValue() { Type = ParseTypeOrArray(callbackInfo.ReturnValue) },
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
                    Type = ParseTypeOrArray(arg),
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
                nspace.Records.Add(new Record()
                {
                    Namespace = nspace, 
                    NativeName = @record.Name, 
                    ManagedName = @record.Name, 
                    GLibClassStructFor = (record.GLibIsGTypeStructFor != null) ? CreateReference(record.GLibIsGTypeStructFor, false) : null
                });
            }
        }

        private void SetFunctions(Namespace nspace, IEnumerable<MethodInfo> functions)
        {
            nspace.Functions = new List<Method>();
            foreach (MethodInfo info in functions)
            {
                var returnVal = new ReturnValue()
                {
                    Type = ParseTypeOrArray(info.ReturnValue)
                };

                nspace.Functions.Add(new Method()
                {
                    ReturnValue = returnVal,
                });
            }
        }

        private TypeReference CreateReference(string type, bool isArray)
        {
            var reference = new TypeReference(type, false);
            _references.Add(reference);
            return reference;
        }

        private TypeReference ParseTypeOrArray(ITypeOrArray? typeOrArray)
        {
            // Check for Type
            var type = typeOrArray?.Type?.Name ?? null;
            if (type != null)
                return CreateReference(type, false);

            // Check for Array
            var array = typeOrArray?.Array?.Type?.Name ?? null;
            if (array != null)
                return CreateReference(array, true);

            // No Type (i.e. void)
            return CreateReference("none", false);
        }
    }
}
