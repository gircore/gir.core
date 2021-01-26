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
        None,
        Container,
        Full
    }
}
