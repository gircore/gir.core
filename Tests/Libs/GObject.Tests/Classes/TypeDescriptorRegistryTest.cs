using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GObject.Test
{
    [TestClass]
    public class TypeDescriptorRegistryTest
    {
        #region Helper

        private static IDictionary<System.Type, TypeDescriptor> GetEmptyDictionary()
        {
            return new Dictionary<System.Type, TypeDescriptor>();
        }

        private static TypeDescriptor GetTypeDescriptor()
        {
            return TypeDescriptor.For(
                wrapperName: "wrappername",
                getType: () => 0
            );
        }

        private static void InitializeTypeDescriptorRegistryTesterFor<T>(TypeDescriptor descriptor)
        {
            var dictionary = GetEmptyDictionary();
            dictionary.Add(typeof(T), descriptor);

            TypeDescriptorRegistry.SetDictionary(dictionary);
        }

        private static TypeDescriptor ResolveTypeWithDescriptor(TypeDescriptor descriptor, IDictionary<System.Type, TypeDescriptor> dictionary)
        {
            ClassWithTypeDescriptor.SetGTypeDescriptor(descriptor);
            TypeDescriptorRegistry.SetDictionary(dictionary);

            return TypeDescriptorRegistry.ResolveTypeDescriptorForType(typeof(ClassWithTypeDescriptor));
        }

        #endregion

        [TestMethod]
        public void ResolveTypeDescriptorForTypeReturnsDataFromCache()
        {
            TypeDescriptor descriptor = GetTypeDescriptor();
            InitializeTypeDescriptorRegistryTesterFor<object>(descriptor);

            TypeDescriptor result = TypeDescriptorRegistry.ResolveTypeDescriptorForType(typeof(object));

            Assert.AreSame(descriptor, result);
        }

        [TestMethod]
        public void ResolveTypeDescriptorForTypeAddsDataToCache()
        {
            TypeDescriptor descriptor = GetTypeDescriptor();
            var dictionary = GetEmptyDictionary();

            _ = ResolveTypeWithDescriptor(descriptor, dictionary);

            Assert.AreSame(descriptor, dictionary[typeof(ClassWithTypeDescriptor)]);
        }

        [TestMethod]
        public void ResolveTypeDescriptorForTypeReturnsInitialTypeDescriptor()
        {
            TypeDescriptor descriptor = GetTypeDescriptor();
            var dictionary = GetEmptyDictionary();

            TypeDescriptor result = ResolveTypeWithDescriptor(descriptor, dictionary);

            Assert.AreSame(descriptor, result);
        }
    }

    internal class ClassWithTypeDescriptor
    {
        private static TypeDescriptor? GTypeDescriptor;

        public static void SetGTypeDescriptor(TypeDescriptor descriptor)
        {
            GTypeDescriptor = descriptor;
        }
    }
}
