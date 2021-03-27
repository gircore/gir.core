using Repository.Model;
using Repository.Xml;

namespace Repository.Factories.Model
{
    public class ArrayFactory
    {
        public Array? Create(ArrayInfo? info)
        {
            if (info is null)
                return null;

            int? length = int.TryParse(info.Length, out var l) ? l : null;
            int? fixedSize = int.TryParse(info.FixedSize, out var f) ? f : null;

            return new Array()
            {
                Length = length,
                IsZeroTerminated = info.ZeroTerminated,
                FixedSize = fixedSize
            };
        }
    }
}
