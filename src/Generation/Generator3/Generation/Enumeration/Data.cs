using System;
using System.Collections.Generic;

namespace Generator3.Generation.Enumeration
{
    public class Data
    {
        private readonly HashSet<Member> _members = new ();
        
        public string Name { get; }
        public string NamespaceName { get; }
        public IEnumerable<Member> Members => _members;

        public Data(string name, string namespaceName)
        {
            Name = name;
            NamespaceName = namespaceName;
        }

        public void Add(Member member)
        {
            if (!_members.Add(member))
                throw new Exception($"Enumeration {Name}: Can not add member {member.Name}. It is already present");
        }
    }
}
