using System.Reflection;

namespace GObject
{
    internal static class TypeExtension
    {
        public static ClassInitFunc? GetClassInitFunc(this System.Type type) => (gClass, classData) =>
        {
            MethodInfo? method = type.GetMethod(
                name: "ClassInit",
                bindingAttr:
                System.Reflection.BindingFlags.Static
                | System.Reflection.BindingFlags.DeclaredOnly
                | System.Reflection.BindingFlags.NonPublic
            );

            if (method is null)
                return;

            method.Invoke(null, new object[] {gClass, classData});
        };

        public static InstanceInitFunc GetInstanceInitFunc(this System.Type type) => (instance, gClass) =>
        {
            MethodInfo? method = type.GetMethod(
                name: "InstanceInit",
                bindingAttr:
                System.Reflection.BindingFlags.Static
                | System.Reflection.BindingFlags.DeclaredOnly
                | System.Reflection.BindingFlags.NonPublic
            );
            
            if (method is null)
                return;
            
            method.Invoke(null, new object[] {instance, gClass});
        };
    }
}
