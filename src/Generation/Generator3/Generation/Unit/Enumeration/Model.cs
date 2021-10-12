using System;
using System.Collections.Generic;

namespace Generator3.Generation.Unit.Enumeration
{
    public class Model
    {
        private readonly HashSet<Generation.Model.Member> _members = new();

        public string Name { get; }
        public string NamespaceName { get; }
        public IEnumerable<Generation.Model.Member> Members => _members;

        public Model(string name, string namespaceName)
        {
            Name = name;
            NamespaceName = namespaceName;
        }

        public void Add(Generation.Model.Member member)
        {
            if (!_members.Add(member))
                throw new Exception($"Enumeration {Name}: Can not add member {member.Name}. It is already present");
        }
    }
}
