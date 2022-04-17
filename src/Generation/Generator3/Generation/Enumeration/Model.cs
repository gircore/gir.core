using System.Collections.Generic;
using System.Linq;
using Generator3.Converter;
using Generator3.Model.Public;

namespace Generator3.Generation.Enumeration
{
    public class Model
    {
        private readonly GirModel.Enumeration _enumeration;

        public string Name => _enumeration.Name;
        public string NamespaceName => _enumeration.Namespace.GetPublicName();
        public GirModel.PlatformDependent? PlatformDependent => _enumeration as GirModel.PlatformDependent;
        public IEnumerable<Member> Members { get; }

        public Model(GirModel.Enumeration enumeration)
        {
            _enumeration = enumeration;

            Members = enumeration.Members
                .Select(member => new Member(member))
                .ToList();
        }
    }
}
