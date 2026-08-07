using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GLib.Internal;

public abstract partial class SListHandle
{
    /// <summary>
    /// Copies the elements of this list into a managed array and releases the list.
    /// </summary>
    /// <param name="createElement">Creates the managed representation of a single element.</param>
    /// <remarks>
    /// Whether an element is owned by the runtime depends on the transfer mode of the
    /// list. <paramref name="createElement"/> is responsible for taking ownership of an
    /// owned element or for copying an unowned one.
    /// </remarks>
    public T[] ToArray<T>(Func<IntPtr, T> createElement)
    {
        var elements = new List<T>();

        //A NULL pointer is an empty list.
        var current = IsInvalid || IsClosed ? IntPtr.Zero : handle;

        while (current != IntPtr.Zero)
        {
            var node = Marshal.PtrToStructure<SListData>(current);
            elements.Add(createElement(node.Data));
            current = node.Next;
        }

        //The elements are managed by their managed representation from now on,
        //so the container is not needed anymore.
        Dispose();

        return elements.ToArray();
    }
}

/// <summary>
/// A handle of a list of which the runtime owns the container but not the elements.
/// </summary>
public class SListContainerHandle : SListHandle
{
    /// <summary>
    /// Creates a new instance of SListContainerHandle. Used automatically by PInvoke.
    /// </summary>
    private SListContainerHandle() : base(true) { }

    /// <summary>
    /// Creates a new instance of SListContainerHandle. Assumes that the given pointer
    /// is owned by the runtime.
    /// </summary>
    public SListContainerHandle(IntPtr ptr) : base(true)
    {
        SetHandle(ptr);
    }

    [DllImport(ImportResolver.Library, EntryPoint = "g_slist_free")]
    private static extern void FreeList(IntPtr list);

    protected override bool ReleaseHandle()
    {
        //Only free the list, not the elements itself
        FreeList(handle);
        return true;
    }
}
