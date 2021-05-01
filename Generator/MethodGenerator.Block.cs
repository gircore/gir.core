using System.Text;

namespace Generator
{
    internal partial class MethodGenerator
    {
        private record Block
        {
            public Block? Inner { get; set; }
            public string? Start { get; set; }
            public string? End { get; set; }

            public string Build()
            {
                var b = new StringBuilder();
                var builder = Build(b);
                return builder.ToString();
            }

            private StringBuilder Build(StringBuilder builder)
            {
                builder.AppendLine(Start);
                Inner?.Build(builder);
                builder.AppendLine(End);
                return builder;
            }
        }

        private class BlockStack
        {
            private Block? _root;
            private Block? _latest;

            public void Nest(Block block)
            {
                if (_latest is null)
                {
                    _root = block;
                    _latest = block;
                }
                else
                {
                    _latest.Inner = block;
                    _latest = block;
                }
            }

            public void AddWhitespace()
                => Nest(new Block());

            public string Build()
                => _root.Build();
        }
    }
}
