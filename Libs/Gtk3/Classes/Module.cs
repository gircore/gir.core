using System.Runtime.CompilerServices;
using GObject;

namespace Gtk
{
    internal partial class Module
    {
        [ModuleInitializer]
        internal static void Initialize()
        {
            RegisterTypes();
        }

        static void RegisterTypes()
        {
            Object.TypeDictionary.Register(typeof(Button), Button.GTypeDescriptor.GType);
        }
    }
}
