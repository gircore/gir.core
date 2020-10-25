using System.Linq;
using Gir;

namespace Generator
{
    internal static class GParameterExtension
    {
        internal static bool AreEqual(this GParameters? obj, TypeResolver resolver, GParameters? other)
        {
            if (obj is { } && other is null) return false;
            if (obj is null && other is { }) return false;
            if (obj == other) return true;
            if (obj!.Count != other!.Count) return false;

            for (var i = 0; i < obj.AllParameters.Count(); i++)
            {
                ResolvedType? resolvedPara1 = resolver.Resolve(obj.AllParameters.ElementAt(i));
                ResolvedType? resolvedPara2 = resolver.Resolve(other.AllParameters.ElementAt(i));

                if (!resolvedPara1.Equals(resolvedPara2))
                    return false;
            }

            return true;
        }
    }
}