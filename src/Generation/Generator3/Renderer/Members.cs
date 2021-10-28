using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator3.Renderer
{
    public static class Members
    {
        public static string Render(this IEnumerable<Model.Member> members)
        {
            return members
                .Select(member => member.Render())
                .Join(Environment.NewLine);
        }
    }
}
