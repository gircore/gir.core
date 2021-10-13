using System;
using System.Collections.Generic;

namespace Generator3.Model
{
    public class EnumerationUnit
    {
        private readonly GirModel.Enumeration _enumeration;
        private readonly HashSet<Member> _members = new();

        public string Name => _enumeration.Name;
        public string NamespaceName => _enumeration.NamespaceName;
        public IEnumerable<Model.Member> Members => _members;

        public EnumerationUnit(GirModel.Enumeration enumeration)
        {
            _enumeration = enumeration;
            foreach (var member in enumeration.Members)
                Add(new Model.Member(member));
        }

        private void Add(Model.Member member)
        {
            if (!_members.Add(member))
                throw new Exception($"Enumeration {Name}: Can not add member {member.Name}. It is already present");
        }

    }
}
