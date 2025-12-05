namespace Gtk;

public partial class CustomSorter
{
    /// <summary>
    /// Creates a CustomSorter instance.
    /// </summary>
    /// <remarks>This is a convenience function not available in the native API to allow comparing <see cref="GObject.Object"/> instead of <see cref="System.IntPtr"/>.</remarks>
    /// <param name="func">A generic variant of <see cref="GLib.CompareDataFunc"/></param>
    /// <typeparam name="T">The <see cref="GObject.Object"/> type which should be compared.</typeparam>
    /// <returns>The new CustomSorter instance.</returns>
    public static CustomSorter New<T>(GObject.CompareDataFuncT<T> func) where T : GObject.NativeObject
    {
        return New((a, b) =>
        {
            var objectA = (T) GObject.Internal.InstanceWrapper.WrapHandle<GObject.Object>(a, false);
            var objectB = (T) GObject.Internal.InstanceWrapper.WrapHandle<GObject.Object>(b, false);

            return func(objectA, objectB);
        });
    }
}
