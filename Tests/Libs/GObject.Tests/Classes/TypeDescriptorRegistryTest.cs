using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GObject.Test
{
    [TestClass]
    public class TypeDescriptorRegistryTest
    {
        #region Helper

        private static IDictionary<System.Type, Object.TypeDescriptor> GetEmptyDictionary()
        {
            return new Dictionary<System.Type, Object.TypeDescriptor>();
        }
        
        private static Object.TypeDescriptor GetTypeDescriptor()
        {
            return Object.TypeDescriptor.For(
                wrapperName: "wrappername",
                getType: () => 0
            );
        }

        private static void InitializeTypeDescriptorRegistryTesterFor<T>(Object.TypeDescriptor descriptor)
        {
            var dictionary = GetEmptyDictionary();
            dictionary.Add(typeof(T), descriptor);

            TypeDescriptorRegistry.SetDictionary(dictionary);
        }
        
        private static Object.TypeDescriptor ResolveTypeWithDescriptor(Object.TypeDescriptor descriptor, IDictionary<System.Type, Object.TypeDescriptor> dictionary)
        {
            ClassWithTypeDescriptor.SetGTypeDescriptor(descriptor);
            TypeDescriptorRegistry.SetDictionary(dictionary);
            
            return TypeDescriptorRegistry.ResolveTypeDescriptorForType(typeof(ClassWithTypeDescriptor));
        }

        #endregion

        [TestMethod]
        public void ResolveTypeDescriptorForTypeReturnsDataFromCache()
        {
            Object.TypeDescriptor descriptor = GetTypeDescriptor();
            InitializeTypeDescriptorRegistryTesterFor<object>(descriptor);

            Object.TypeDescriptor result = TypeDescriptorRegistry.ResolveTypeDescriptorForType(typeof(object));

            Assert.AreSame(descriptor, result);
        }

        [TestMethod]
        public void ResolveTypeDescriptorForTypeAddsDataToCache()
        {
            Object.TypeDescriptor descriptor = GetTypeDescriptor();
            var dictionary = GetEmptyDictionary();

            _ = ResolveTypeWithDescriptor(descriptor, dictionary);

            Assert.AreSame(descriptor, dictionary[typeof(ClassWithTypeDescriptor)]);
        }

        [TestMethod]
        public void ResolveTypeDescriptorForTypeReturnsInitialTypeDescriptor()
        {
            Object.TypeDescriptor descriptor = GetTypeDescriptor();
            var dictionary = GetEmptyDictionary();

            Object.TypeDescriptor result = ResolveTypeWithDescriptor(descriptor, dictionary);

            Assert.AreSame(descriptor, result);
        }
    }

    internal class ClassWithTypeDescriptor
    {
        private static Object.TypeDescriptor? GTypeDescriptor;

        public static void SetGTypeDescriptor(Object.TypeDescriptor descriptor)
        {
            GTypeDescriptor = descriptor;
        }
    }
}
