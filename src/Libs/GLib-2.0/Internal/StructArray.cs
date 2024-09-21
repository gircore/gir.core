using System;

namespace GLib.Internal;

public static class StructArray
{
    public static T[] Copy<T>(IntPtr p, uint length) where T : unmanaged
    {
        var resultArray = new T[length];
        unsafe
        {
            var ptr = (T*) p;
            for (var i = 0; i < length; i++)
            {
                resultArray[i] = *ptr;
                ptr++;
            }
        }

        return resultArray;
    }
}
