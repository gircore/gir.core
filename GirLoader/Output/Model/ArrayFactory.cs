namespace Gir.Output.Model
{
    internal class ArrayFactory
    {
        public Array? Create(Input.Model.Array? array)
        {
            if (array is null)
                return null;

            int? length = int.TryParse(array.Length, out var l) ? l : null;
            int? fixedSize = int.TryParse(array.FixedSize, out var f) ? f : null;

            return new Array()
            {
                Length = length,
                IsZeroTerminated = array.ZeroTerminated,
                FixedSize = fixedSize
            };
        }
    }
}
