namespace GObject.Native
{
    public class StaticRegistrar : TypeRegistrar
    {
        public void RegisterSubclassStatic(System.Type type, System.Type parentType, string qualifiedName)
        {
            // Do actual registration
            RegisterSubclass(
                type,
                parentType,
                qualifiedName 
            );
        }
    }
}
