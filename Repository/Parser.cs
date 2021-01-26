using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

using Repository.Analysis;
using Repository.Xml;
using Repository.Model;

#nullable enable

namespace Repository
{
    public class Parser
    {
        private readonly RepositoryInfo _repoInfo;
        private readonly FileInfo _girFile;
        
        private readonly List<TypeReference> _references;
        private readonly Namespace _nspace;
        
        public Parser(FileInfo girFile)
        {
            _repoInfo = Deserialize(girFile);
            _girFile = girFile;
            _references = new List<TypeReference>();
            _nspace = new Namespace();
        }
        
        public IEnumerable<(string, string)> GetDependencies()
        {
            foreach (IncludeInfo includeInfo in _repoInfo.Includes)
            {
                yield return (includeInfo.Name!, includeInfo.Version!);
            }
        }

        public (Namespace, IEnumerable<TypeReference>) Parse()
        {
            if (_repoInfo.Namespace == null)
                throw new InvalidDataException($"File '{_girFile} does not define a namespace.");

            NamespaceInfo nspaceInfo = _repoInfo.Namespace;

            // Basic Info
            _nspace.Name = nspaceInfo.Name;
            _nspace.Version = nspaceInfo.Version;
            
            // Aliases
            _nspace.Aliases = ParseAliases(nspaceInfo, nspaceInfo.Aliases).ToList();
            
            // Symbols
            _nspace.Classes = ParseClasses(nspaceInfo, nspaceInfo.Classes).ToList();
            _nspace.Callbacks = ParseCallbacks(nspaceInfo, nspaceInfo.Callbacks).ToList();
            _nspace.Enumerations = ParseEnumerations(nspaceInfo, nspaceInfo.Enumerations, false).ToList();
            _nspace.Bitfields = ParseEnumerations(nspaceInfo, nspaceInfo.Bitfields, true).ToList();
            _nspace.Interfaces = ParseInterfaces(nspaceInfo, nspaceInfo.Interfaces).ToList();
            _nspace.Records = ParseRecords(nspaceInfo, nspaceInfo.Records).ToList();
            
            // Misc
            _nspace.Functions = ParseFunctions(nspaceInfo, nspaceInfo.Functions).ToList();

            return (_nspace, _references);
        }

        private IEnumerable<Alias> ParseAliases(NamespaceInfo nspace, IEnumerable<AliasInfo> aliases)
        {
            foreach (AliasInfo alias in aliases)
            {
                yield return new Alias(alias.Name, alias.For!.Name);
            }
        }
        
        private IEnumerable<Class> ParseClasses(NamespaceInfo nspace, IEnumerable<ClassInfo> classes)
        {
            foreach (ClassInfo cls in classes)
            {
                yield return new Class()
                {
                    Namespace = _nspace,
                    NativeName = cls.Name,
                    ManagedName = cls.Name,
                    
                    CType = cls.TypeName,
                    Parent = (cls.Parent != null) ? CreateReference(cls.Parent, false) : null,
                    Implements = ParseImplements(cls.Implements).ToList(),
                };
            }
        }

        private IEnumerable<TypeReference> ParseImplements(IEnumerable<ImplementInfo> implements)
        {
            foreach (ImplementInfo impl in implements)
                yield return CreateReference(impl.Name!, false);
        }
        
        private IEnumerable<Callback> ParseCallbacks(NamespaceInfo nspace, IEnumerable<CallbackInfo> callbacks)
        {
            foreach (CallbackInfo callback in callbacks)
            {
                yield return new Callback()
                {
                    Namespace = _nspace,
                    NativeName = callback.Name,
                    ManagedName = callback.Name,
                    
                    ReturnValue = new ReturnValue() { Type = ParseTypeOrArray(callback.ReturnValue) },
                    Arguments = (callback.Parameters != null)
                        ? ParseArguments(nspace, callback.Parameters!).ToList()
                        : new List<Argument>(),
                };
            }
        }

        private IEnumerable<Argument> ParseArguments(NamespaceInfo nspace, ParametersInfo parameters)
        {
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
                
                yield return new Argument()
                {
                    Name = arg.Name,
                    Type = ParseTypeOrArray(arg),
                    Direction = direction,
                    Transfer = transfer,
                    Nullable = arg.Nullable
                };
            }
        }
        
        private IEnumerable<Enumeration> ParseEnumerations(NamespaceInfo nspace, IEnumerable<EnumInfo> enumerations, bool isBitfield)
        {
            foreach (EnumInfo @enum in enumerations)
            {
                yield return new Enumeration()
                {
                    Namespace = _nspace,
                    NativeName = @enum.Name,
                    ManagedName = @enum.Name,
                    
                    HasFlags = isBitfield,
                };
            }
        }
        
        private IEnumerable<Interface> ParseInterfaces(NamespaceInfo nspace, IEnumerable<InterfaceInfo> ifaces)
        {
            foreach (InterfaceInfo iface in ifaces)
            {
                yield return new Interface()
                {
                    Namespace = _nspace,
                    NativeName = iface.Name,
                    ManagedName = iface.Name
                };
            }
        }
        
        private IEnumerable<Record> ParseRecords(NamespaceInfo nspace, IEnumerable<RecordInfo> records)
        {
            foreach (RecordInfo @record in records)
            {
                yield return new Record()
                {
                    Namespace = _nspace,
                    NativeName = @record.Name,
                    ManagedName = @record.Name,
                    
                    GLibClassStructFor = (record.GLibIsGTypeStructFor != null) ? CreateReference(record.GLibIsGTypeStructFor, false) : null
                };
            }
        }

        private IEnumerable<Method> ParseFunctions(NamespaceInfo nspace, IEnumerable<MethodInfo> functions)
        {
            foreach (MethodInfo info in functions)
            {
                var returnVal = new ReturnValue() {Type = ParseTypeOrArray(info.ReturnValue)};
                
                yield return new Method()
                {
                    ReturnValue = returnVal,
                };
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
        
        private static RepositoryInfo Deserialize(FileInfo girFile)
        {
            var serializer = new XmlSerializer(
                type: typeof(RepositoryInfo),
                defaultNamespace: "http://www.gtk.org/introspection/core/1.0");

            using FileStream fs = girFile.OpenRead();
            
            return (RepositoryInfo)serializer.Deserialize(fs);
        }
    }
}
