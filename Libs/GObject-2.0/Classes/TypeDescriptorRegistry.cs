using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace GObject
{
    internal class TypeDescriptorRegistryException : Exception
    {
        public System.Type? Type { get; }

        public TypeDescriptorRegistryException(string message, System.Type? type) : base(message)
        {
            Type = type;
        }

        public TypeDescriptorRegistryException(string message, System.Type? type, Exception inner) : base(message, inner)
        {
            Type = type;
        }
    }

    internal static class TypeDescriptorRegistry
    {
        #region Fields

        private const string Gtypedescriptor = "GTypeDescriptor";
        private static IDictionary<System.Type, TypeDescriptor> DescriptorDict;

        #endregion

        #region Constructors

        static TypeDescriptorRegistry()
        {
            DescriptorDict = new Dictionary<System.Type, TypeDescriptor>();
        }

        #endregion

        #region Methods

        internal static void SetDictionary(IDictionary<System.Type, TypeDescriptor> dict)
        {
            DescriptorDict = dict;
        }

        public static bool TryResolveTypeDescriptorForType(System.Type type, [MaybeNullWhen(false)] out TypeDescriptor descriptor)
        {
            try
            {
                descriptor = ResolveTypeDescriptorForType(type);
                return true;
            }
            catch
            {
                descriptor = null;
                return false;
            }
        }

        /// <summary>
        /// Returns the TypeDescriptor for a given System.Type. The type must define a static,
        /// non public field named "GTypeDescriptor". Throws an exception in case the TypeDescriptor
        /// can not be resolved. This method caches results for performance.
        /// </summary>
        /// <param name="type">The System.Type to check for a TypeDescriptor.</param>
        /// <returns>Returns the TypeDescriptor for the given System.Type</returns>
        /// <exception cref="TypeDescriptorRegistryException">Thrown in case of an error.</exception>
        public static TypeDescriptor ResolveTypeDescriptorForType(System.Type type)
        {
            if (TryGetTypeDescriptor(type, out TypeDescriptor? cachedDescriptor))
                return cachedDescriptor;

            TypeDescriptor descriptor = FindTypeDescriptor(type);
            RegisterTypeDescriptor(type, descriptor);

            return descriptor;
        }

        private static bool TryGetTypeDescriptor(System.Type type, [MaybeNullWhen(false)] out TypeDescriptor descriptor)
        {
            return DescriptorDict.TryGetValue(type, out descriptor);
        }

        private static TypeDescriptor FindTypeDescriptor(System.Type type)
        {
            FieldInfo? info = GetTypeDescriptorFieldInfo(type);
            return GetTypeDescriptorFromFieldInfo(info);
        }

        private static FieldInfo GetTypeDescriptorFieldInfo(System.Type type)
        {
            const System.Reflection.BindingFlags flags =
                System.Reflection.BindingFlags.NonPublic
                | System.Reflection.BindingFlags.Static
                | System.Reflection.BindingFlags.DeclaredOnly;

            FieldInfo? descriptorField = type.GetField(Gtypedescriptor, flags);

            if (descriptorField is null)
            {
                var message =
                    $"No field named '{Gtypedescriptor}' with " +
                    $"{nameof(System.Reflection.BindingFlags)} of {flags} found for type {type.FullName}.";

                throw new TypeDescriptorRegistryException(message, type);
            }

            return descriptorField;
        }

        private static TypeDescriptor GetTypeDescriptorFromFieldInfo(FieldInfo info)
        {
            TypeDescriptor? descriptor;

            try
            {
                descriptor = info.GetValue(null) as TypeDescriptor;
            }
            catch (Exception ex)
            {
                var message =
                    $"Could not get value from field {Gtypedescriptor} for type {info.DeclaringType?.FullName}";

                throw new TypeDescriptorRegistryException(message, info.DeclaringType, ex);
            }

            if (descriptor is null)
            {
                var message =
                    $"The field {Gtypedescriptor} does not return an object of type " +
                    $"{nameof(TypeDescriptor)} for type {info.DeclaringType?.FullName}";

                throw new TypeDescriptorRegistryException(message, info.DeclaringType);
            }

            return descriptor;
        }

        private static void RegisterTypeDescriptor(System.Type type, TypeDescriptor descriptor)
        {
            DescriptorDict[type] = descriptor;
        }

        #endregion
    }
}
