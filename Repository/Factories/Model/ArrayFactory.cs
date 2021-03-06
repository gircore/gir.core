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

            int? length = null;

            if (int.TryParse(info.Length, out var l))
                length = l;

            return new Array()
            {
                Length = length
            };
        }
    }
}
