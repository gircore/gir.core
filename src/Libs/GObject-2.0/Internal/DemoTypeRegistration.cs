using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;

namespace GObject.Internal;

internal class DemoTypeRegistration
{
    internal static void RegisterTypes()
    {
        Register<GObject.Binding>(Binding.GetGType, OSPlatform.Linux, OSPlatform.OSX, OSPlatform.Windows);
    }

    private static void Register<T>(Func<nuint> getType, params OSPlatform[] supportedPlatforms) where T : Constructable
    {
        try
        {
            if (supportedPlatforms.Any(RuntimeInformation.IsOSPlatform))
                InstanceFactory.AddFactoryForType(getType(), T.Create);
        }
        catch (System.Exception e)
        {
            Debug.WriteLine($"Could not register type '{nameof(T)}': {e.Message}");
        }
    }
}
