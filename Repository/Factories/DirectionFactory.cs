using Repository.Model;

namespace Repository.Services
{
    public static class DirectionFactory
    {
        public static Direction Create(string? direction)
        {
            return direction switch
            {
                "in" => Model.Direction.In,
                "out" => Model.Direction.Out,
                "inout" => Model.Direction.Ref,
                _ => Model.Direction.Default
            };
        }
    }
}
