namespace Repository.Model
{
    public enum Direction
    {
        Default,
        In,
        OutCallerAllocates,
        OutCalleeAllocates,
        Ref
    }

    public enum Transfer
    {
        Unknown,
        None,
        Container,
        Full
    }
}
