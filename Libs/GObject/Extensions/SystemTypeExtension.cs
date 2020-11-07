using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace GObject
{
    internal static class SystemTypeExtension
    {
        #region Fields

        private const string ClassInit = "ClassInit";
        private static readonly Dictionary<System.Type, Object.TypeDescriptor?> DescriptorDict =
            new Dictionary<System.Type, Object.TypeDescriptor?>();

        #endregion

        #region Methods

        public static Type Register(this System.Type type)
            => Object.TypeRegistration.Register(type);
        
        /// <summary>
        /// Determines whether the type is a managed subclass,
        /// as opposed to wrapping an existing type.
        /// </summary>
        internal static bool IsSubclass(this System.Type type)
            => type != typeof(Object) &&
               type != typeof(InitiallyUnowned) &&
               type.GetTypeDescriptor() is null;


        private static void InvokeStaticMethod(this IReflect type, string name, params object[] parameters)
        {
            MethodInfo? method = type.GetMethod(
                name: name,
                bindingAttr:
                System.Reflection.BindingFlags.Static
                | System.Reflection.BindingFlags.DeclaredOnly
                | System.Reflection.BindingFlags.NonPublic
            );

            method?.Invoke(null, parameters);
        }

        internal static void InvokeClassInitMethod(this IReflect callingType, Type gClass, IReflect type)
            => callingType.InvokeStaticMethod(ClassInit, gClass, type, IntPtr.Zero);

        internal static ClassInitFunc? GetClassInitFunc(this IReflect type) => (gClass, classData) =>
        {
            type.InvokeStaticMethod(
                name: ClassInit,
                parameters: new object[] {gClass.GetGTypeFromTypeClass(), type, classData}
            );
        };

        internal static InstanceInitFunc GetInstanceInitFunc(this IReflect type) => (instance, gClass) =>
        {
            type.InvokeStaticMethod(
                name: "InstanceInit",
                parameters: new object[] {instance, gClass.GetGTypeFromTypeClass(), type}
            );
        };

        internal static BaseInitFunc GetBaseInitFunc(this IReflect type) => (gClass) =>
        {
            type.InvokeStaticMethod(
                name: "BaseInit",
                parameters: new object[] {gClass.GetGTypeFromTypeClass(), type}
            );
        };

        /// <summary>
        /// Returns the MethodInfo for the 'GetGType()' function
        /// if the type in question implements it (i.e. a wrapper)
        /// </summary>
        internal static Object.TypeDescriptor? GetTypeDescriptor(this System.Type type)
        {
            if (DescriptorDict.TryGetValue(type, out Object.TypeDescriptor? cachedDescriptor))
                return cachedDescriptor;

            FieldInfo? descriptorField = type.GetField(
                name: "GTypeDescriptor",
                bindingAttr:
                System.Reflection.BindingFlags.NonPublic
                | System.Reflection.BindingFlags.Static
                | System.Reflection.BindingFlags.DeclaredOnly
            );

            var descriptor = (Object.TypeDescriptor?) descriptorField?.GetValue(null);
            DescriptorDict[type] = descriptor;

            return descriptor;
        }

        #endregion
    }
}
