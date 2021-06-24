using System;
using System.Collections.Generic;
using System.Linq;

namespace GirLoader.Output
{
    internal class RepositoryResolver
    {
        private readonly TypeDictionary _typeDictionary = new();
        private readonly HashSet<Model.Repository> _knownRepositories = new();

        public RepositoryResolver()
        {
            AddFundamentalTypes();
        }

        private void AddFundamentalTypes()
        {
            _typeDictionary.AddType(new Model.Void("none"));
            _typeDictionary.AddType(new Model.Void("void"));

            _typeDictionary.AddType(new Model.Boolean("gboolean"));

            _typeDictionary.AddType(new Model.Float("gfloat"));
            _typeDictionary.AddType(new Model.Float("float"));

            _typeDictionary.AddType(new Model.Pointer("any"));
            _typeDictionary.AddType(new Model.Pointer("gconstpointer"));
            _typeDictionary.AddType(new Model.Pointer("va_list"));
            _typeDictionary.AddType(new Model.Pointer("gpointer"));
            _typeDictionary.AddType(new Model.Pointer("tm"));
            _typeDictionary.AddType(new Model.Pointer("FILE"));//TODO: Automatic convert to some stream?

            // TODO: Should we use UIntPtr here? Non-CLR compliant
            _typeDictionary.AddType(new Model.UnsignedPointer("guintptr"));

            _typeDictionary.AddType(new Model.UnsignedShort("guint16"));
            _typeDictionary.AddType(new Model.UnsignedShort("gunichar2"));//TOOO: UTF16 char?
            _typeDictionary.AddType(new Model.UnsignedShort("gushort"));

            _typeDictionary.AddType(new Model.Short("gint16"));
            _typeDictionary.AddType(new Model.Short("gshort"));

            _typeDictionary.AddType(new Model.Double("double"));
            _typeDictionary.AddType(new Model.Double("gdouble"));
            _typeDictionary.AddType(new Model.Double("long double"));

            _typeDictionary.AddType(new Model.Integer("int"));
            _typeDictionary.AddType(new Model.Integer("gint"));
            _typeDictionary.AddType(new Model.Integer("gatomicrefcount"));
            _typeDictionary.AddType(new Model.Integer("gint32"));
            _typeDictionary.AddType(new Model.Integer("pid_t"));
            _typeDictionary.AddType(new Model.Integer("grefcount"));

            _typeDictionary.AddType(new Model.UnsignedInteger("unsigned int"));
            _typeDictionary.AddType(new Model.UnsignedInteger("unsigned"));
            _typeDictionary.AddType(new Model.UnsignedInteger("guint"));
            _typeDictionary.AddType(new Model.UnsignedInteger("guint32"));
            _typeDictionary.AddType(new Model.UnsignedInteger("gunichar"));
            _typeDictionary.AddType(new Model.UnsignedInteger("uid_t"));

            _typeDictionary.AddType(new Model.Byte("guchar"));
            _typeDictionary.AddType(new Model.Byte("guint8"));

            _typeDictionary.AddType(new Model.SignedByte("gchar"));
            _typeDictionary.AddType(new Model.SignedByte("char"));
            _typeDictionary.AddType(new Model.SignedByte("gint8"));

            _typeDictionary.AddType(new Model.Long("glong"));
            _typeDictionary.AddType(new Model.Long("gssize"));
            _typeDictionary.AddType(new Model.Long("gint64"));
            _typeDictionary.AddType(new Model.Long("goffset"));
            _typeDictionary.AddType(new Model.Long("time_t"));

            _typeDictionary.AddType(new Model.NativeUnsignedInteger("gsize"));
            _typeDictionary.AddType(new Model.UnsignedLong("guint64"));
            _typeDictionary.AddType(new Model.UnsignedLong("gulong"));

            _typeDictionary.AddType(new Model.Utf8String());
            _typeDictionary.AddType(new Model.PlatformString());
        }


        /// <summary>
        /// Loads the given repository and all its dependencies
        /// </summary>
        public void Add(Model.Repository repository)
        {
            if (!_knownRepositories.Add(repository))
                return; //Ignore known repositories

            FillTypeDictionary(repository.Namespace);

            foreach (var depdenentRepository in repository.Includes.Select(x => x.GetResolvedRepository()))
                Add(depdenentRepository);
        }

        /// <summary>
        /// Resolves all loaded repositories
        /// </summary>
        public void Resolve()
        {
            var dependencyResolver = new Helper.DependencyResolver<Model.Repository>();
            var orderedRepositories = dependencyResolver.ResolveOrdered(_knownRepositories).Cast<Model.Repository>();

            foreach (var repository in orderedRepositories)
                ResolveTypeReferences(repository.Namespace);
        }

        private void FillTypeDictionary(Model.Namespace @namespace)
        {
            _typeDictionary.AddTypes(@namespace.Classes);
            _typeDictionary.AddTypes(@namespace.Interfaces);
            _typeDictionary.AddTypes(@namespace.Callbacks);
            _typeDictionary.AddTypes(@namespace.Enumerations);
            _typeDictionary.AddTypes(@namespace.Bitfields);
            _typeDictionary.AddTypes(@namespace.Records);
            _typeDictionary.AddTypes(@namespace.Unions);
        }

        private void ResolveTypeReferences(Model.Namespace ns)
        {
            foreach (var reference in ns.GetTypeReferences())
                ResolveTypeReference(reference);

            Log.Debug($"Resolved type references for repository {ns.Name}");
        }

        private void ResolveTypeReference(Model.TypeReference reference)
        {
            if (reference is Model.ArrayTypeReference arrayTypeReference)
            {
                // Array type references are not resolved directly. Only their type get's resolved
                // because arrays are no type themself. They only provide structure.
                ResolveTypeReference(arrayTypeReference.TypeReference);
            }
            else if(reference is Model.ResolveableTypeReference resolveableTypeReference)
            {
                if (_typeDictionary.TryLookup(resolveableTypeReference, out var type))
                    resolveableTypeReference.ResolveAs(type);
                else
                    Log.Verbose($"Could not resolve type reference {reference}");   
            }
            else
            {
                throw new Exception($"Unknown {nameof(Model.TypeReference)} {reference.GetType().Name}");
            }
        }
    }
}
