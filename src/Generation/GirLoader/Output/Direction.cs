namespace GirLoader.Output
{
    public enum Direction
    {
        Default,
        In,
        Out,
        Ref
    }

    internal static class DirectionConverter
    {
        public static GirModel.Direction ToGirModel(this Direction direction) => direction switch
        {
            Direction.In => GirModel.Direction.In,
            Direction.Out => GirModel.Direction.Out,
            Direction.Ref => GirModel.Direction.InOut,
            _ => GirModel.Direction.In
        };
    }
}
