using GObject.Internal;

namespace GObject
{
    public partial class Object
    {
        private Type GetGTypeOrRegister(System.Type type)
        {
            if (TypeDictionary.ContainsSystemType(type))
                return TypeDictionary.GetGType(type);

            // We are not in the type dictionary, which means we are
            // an unregistered managed subclass type. There are two ways
            // to register subclasses: Dynamically (reflection) and
            // Statically (by source generation).

            // Static registration happens on application startup in the
            // module initialiser if source generator support is present
            // at build time (note: not implemented as of 29/04/21)

            // Therefore, we can assume static registration did not go ahead
            // and we should resort to the fallback reflection-based registration
            // implemented below. We should therefore register ourselves
            // and every type we inherit from that has also not been registered.

            FallbackRegistrationStrategy.RegisterSubclassRecursive(type);

            return TypeDictionary.GetGType(type);
        }

        private static class FallbackRegistrationStrategy
        {
            private static string QualifyName(System.Type type)
                => type.FullName?.Replace(".", "") ?? type.Name;

            public static void RegisterSubclassRecursive(System.Type type)
            {
                System.Type baseType = type.BaseType!;
                if (!TypeDictionary.ContainsSystemType(baseType))
                    RegisterSubclassRecursive(baseType);

                // Do actual registration
                Type gtype = TypeRegistrar.RegisterGType(
                    qualifiedName: QualifyName(type),
                    parentType: TypeDictionary.GetGType(baseType)
                );

                TypeDictionary.Add(type, gtype);
            }
        }
    }
}
