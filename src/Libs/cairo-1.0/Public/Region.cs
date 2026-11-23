using System;

namespace Cairo;
public partial class Region
{
    public Region()
        : this(Internal.Region.Create())
    {
    }

    public Region(RectangleInt rectangle)
        : this(Internal.Region.CreateRectangle(rectangle.Handle))
    {
    }

    public Region Copy() => new Region(Internal.Region.Copy(Handle));

    public Status Status => Internal.Region.Status(Handle);

    public bool IsEmpty => Internal.Region.IsEmpty(Handle);
    public int NumRectangles => Internal.Region.NumRectangles(Handle);
    public void GetRectangle(int i, RectangleInt rectangle)
        => Internal.Region.GetRectangle(Handle, i, rectangle.Handle);
    public void GetExtents(RectangleInt extents)
        => Internal.Region.GetExtents(Handle, extents.Handle);

    public bool ContainsPoint(int x, int y)
        => Internal.Region.ContainsPoint(Handle, x, y);

    public RegionOverlap ContainsRectangle(RectangleInt rectangle)
        => Internal.Region.ContainsRectangle(Handle, rectangle.Handle);

    public void Translate(int x, int y)
        => Internal.Region.Translate(Handle, x, y);

    public Status Intersect(Region other)
        => Internal.Region.Intersect(Handle, other.Handle);

    public Status IntersectRectangle(RectangleInt rectangle)
        => Internal.Region.IntersectRectangle(Handle, rectangle.Handle);

    public Status Subtract(Region other)
        => Internal.Region.Subtract(Handle, other.Handle);

    public Status SubtractRectangle(RectangleInt rectangle)
        => Internal.Region.SubtractRectangle(Handle, rectangle.Handle);

    public Status Union(Region other)
        => Internal.Region.Union(Handle, other.Handle);

    public Status UnionRectangle(RectangleInt rectangle)
        => Internal.Region.UnionRectangle(Handle, rectangle.Handle);

    public Status Xor(Region other)
        => Internal.Region.Xor(Handle, other.Handle);

    public Status XorRectangle(RectangleInt rectangle)
        => Internal.Region.XorRectangle(Handle, rectangle.Handle);
}
