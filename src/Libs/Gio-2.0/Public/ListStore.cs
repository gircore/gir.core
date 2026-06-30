namespace Gio;

public partial class ListStore
{
    /// <summary>
    /// Creates a new ListStore instance.
    /// </summary>
    /// <typeparam name="T">The type of data the ListStore should contain.</typeparam>
    /// <returns>A new ListStore.</returns>
    public static ListStore New<T>() where T : GObject.GTypeProvider
    {
        return New(T.GetGType());
    }
}
