using System.Collections.Generic;

namespace GObject;

public static class SListExtensions
{
    public static T ToObject<T>(this GLib.SListElement element) where T : Object, InstanceFactory, GTypeProvider
    {
        return (T) Internal.InstanceWrapper.WrapHandle<T>(element.Data, false);
    }

    public static T ToBoxed<T>(this GLib.SListElement element) where T : GLib.BoxedRecord, GTypeProvider
    {
        return (T) Internal.BoxedWrapper.WrapHandle(element.Data, false, T.GetGType());
    }

    public static IEnumerable<T> AsObjects<T>(this IEnumerable<GLib.SListElement> source) where T : Object, InstanceFactory, GTypeProvider
    {
        foreach (var item in source)
        {
            yield return item.ToObject<T>();
        }
    }

    public static IEnumerable<T> AsBoxed<T>(this IEnumerable<GLib.SListElement> source) where T : GLib.BoxedRecord, GTypeProvider
    {
        foreach (var item in source)
        {
            yield return item.ToBoxed<T>();
        }
    }
}
