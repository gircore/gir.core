using System;
using System.Collections.Generic;
using System.IO;
using CWrapper;
using Delegate = CWrapper.Delegate;
using Enum = CWrapper.Enum;

namespace Gir
{
    public class GirCWrapper
    {
        private readonly string outputDir;
        private readonly string import;
        private GRepository repository;
        private readonly CWrapper.Wrapper wrapper;
        private IEnumerable<GAlias> aliases;

        #region Properties
        private Func<GClass, Class>? classAdapter = default;
        public Func<GClass, Class> ClassAdapter
        {
            get => classAdapter ??= (cls) => new GirClassAdapter(cls, import, TypeResolver, IdentifierFixer);
            set => classAdapter = value;
        }

        private Func<GInterface, Class>? interfaceAdapter = default;
        public Func<GInterface, Class> InterfaceAdapter
        {
            get => interfaceAdapter ??= (iface) => new GirInterfaceAdapter(iface, import, TypeResolver, IdentifierFixer);
            set => interfaceAdapter = value;
        }

        private Func<GRecord, Struct>? structAdapter = default;
        public Func<GRecord, Struct> StructAdapter
        {
            get => structAdapter ??= (str) => new GirStructAdapter(str, import, TypeResolver, IdentifierFixer);
            set => structAdapter = value;
        }

        private Func<GEnumeration, bool, Enum>? enumAdapter;
        public Func<GEnumeration, bool, Enum> EnumAdapter
        {
            get => enumAdapter ??= (e, hasFlags) => new GirEnumAdapter(e, hasFlags, TypeResolver, IdentifierFixer);
            set => enumAdapter = value;
        }

        private Func<GCallback, Delegate>? delegateAdapter = default;
        public Func<GCallback, Delegate> DelegateAdapter
        {
            get => delegateAdapter ??= (d) => new GirDelegateAdapater(d, TypeResolver, IdentifierFixer);
            set => delegateAdapter = value;
        }

        private Func<GMethod, Method>? methodAdapter = default;
        public Func<GMethod, Method> MethodAdapter
        {
            get => methodAdapter ??= (m) => new GirMethodAdapater(m, import, TypeResolver, IdentifierFixer);
            set => methodAdapter = value;
        }

        private Func<GNamespace, Namespace>? namespaceAdapter = default;
        public Func<GNamespace, Namespace> NamespaceAdapter
        {
            get => namespaceAdapter ??= (ns) => new GirNamespaceAdapter(ns);
            set => namespaceAdapter = value;
        }

        private AliasResolver? aliasResolver = default;
        public AliasResolver AliasResolver
        {
            get => aliasResolver ??= new GAliasResolver(aliases);
            set => aliasResolver = value;
        }

        private TypeResolver? typeResolver = default;
        public TypeResolver TypeResolver
        {
            get => typeResolver ??= new GTypeResolver(AliasResolver);
            set => typeResolver = value;
        }

        private IdentifierFixer? identifierFixer = default;
        public IdentifierFixer IdentifierFixer
        {
            get => identifierFixer ??= new CSharpIdentifierFixer();
            set => identifierFixer = value;
        }
        #endregion Properties

        public GirCWrapper(string girFile, string outputDir, string import, params string[] girFilesForAliases)
        {
            this.outputDir = outputDir ?? throw new System.ArgumentNullException(nameof(outputDir));
            this.import = import ?? throw new ArgumentNullException(nameof(import));

            var reader = new GirReader();
            repository = reader.ReadRepository(girFile);

            #region Init Aliases
            var aliases = new List<GAlias>();
            aliases.AddRange(repository.Namespace.Aliases);

            foreach(var aliasGir in girFilesForAliases)
            {
                var repo = reader.ReadRepository(aliasGir);
                aliases.AddRange(repo.Namespace.Aliases);
            }

            this.aliases = aliases;
            #endregion Init Aliases

            wrapper = new CWrapper.Wrapper();
            Directory.CreateDirectory(outputDir);
        }

        public void CreateClasses()
        {
            foreach(var cls in repository.Namespace.Classes)
            {
                try
                {
                    var content = wrapper.GenerateClass(NamespaceAdapter(repository.Namespace), ClassAdapter(cls));
                    Write(cls.Type, content);
                }
                catch(Exception ex)
                {
                    Console.Error.WriteLine($"Could not create class {cls.Name}: {ex.Message}");
                }
            }
        }

        public void CreateInterfaces()
        {
            foreach(var iface in repository.Namespace.Interfaces)
            {
                try
                {
                    var content = wrapper.GenerateClass(NamespaceAdapter(repository.Namespace), InterfaceAdapter(iface));
                    Write(iface.Type, content);
                }
                catch(Exception ex)
                {
                    Console.Error.WriteLine($"Could not create class {iface.Name}: {ex.Message}");
                }
            }
        }

        public void CreateDelegates()
        {
            var data = new List<Delegate>();
            foreach(var callback in repository.Namespace.Callbacks)
            {
                data.Add(DelegateAdapter(callback));
            }

            try
            {
                var type = "Delegates";
                var content = wrapper.GenerateDelegates(NamespaceAdapter(repository.Namespace), data);
                Write(type, content);
            }
            catch(Exception ex)
            {
                Console.Error.WriteLine($"Could not create delegates: {ex.Message}");
            }
        }


        public void CreateMethods()
        {
            var data = new List<Method>();
            foreach(var method in repository.Namespace.Functions)
            {
                data.Add(MethodAdapter(method));
            }

            try
            {
                var type = "Methods";
                var content = wrapper.GenerateMethods(NamespaceAdapter(repository.Namespace), data);
                Write(type, content);
            }
            catch(Exception ex)
            {
                Console.Error.WriteLine($"Could not create methods: {ex.Message}");
            }
        }

        public void CreateStructs()
        {
            foreach(var record in repository.Namespace.Records)
            {
                try
                {
                    var content = wrapper.GenerateStruct(NamespaceAdapter(repository.Namespace), StructAdapter(record));
                    Write(record.Type, content);
                }
                catch(Exception ex)
                {
                    Console.Error.WriteLine($"Could not create struct {record.Name}: {ex.Message}");
                }
            }
        }

        public void CreateEnums()
        {
           CreateEnums(repository.Namespace.Enumerations, false);
           CreateEnums(repository.Namespace.Bitfields, true);
        }

        private void CreateEnums(IEnumerable<GEnumeration> enumerations, bool flags)
        {
            foreach(var enumaration in enumerations)
            {
                try
                {
                    var content = wrapper.GenerateEnum(NamespaceAdapter(repository.Namespace), EnumAdapter(enumaration, flags));
                    Write(enumaration.Type, content);                   
                }
                catch(Exception ex)
                {
                    Console.Error.WriteLine($"Could not create enum {enumaration.Name}: {ex.Message}");
                }
            }
        }

        private void Write(string type, string content)
        {
            var fileName = type + ".cs";
            var path = Path.Combine(outputDir, fileName);

            File.WriteAllText(path, content);
        }
    }
}