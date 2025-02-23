namespace Cairo;

public partial class Pattern
{
    public Status Status => Internal.Pattern.Status(Handle);

    public PatternType PatternType => Internal.Pattern.GetType(Handle);

    public Extend Extend
    {
        get => Internal.Pattern.GetExtend(Handle);
        set => Internal.Pattern.SetExtend(Handle, value);
    }

    public Filter Filter
    {
        get => Internal.Pattern.GetFilter(Handle);
        set => Internal.Pattern.SetFilter(Handle, value);
    }

    public void GetMatrix(Matrix matrix)
        => Internal.Pattern.GetMatrix(Handle, matrix.Handle);

    public void SetMatrix(Matrix matrix)
        => Internal.Pattern.SetMatrix(Handle, matrix.Handle);
}
