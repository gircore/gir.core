namespace Generator3.Renderer.Public
{
    // We use a system of 'Blocks' to make sure resources are allocated and
    // deallocated in the correct order. Blocks are nested inside each other,
    // wrapping the inner blocks with a given 'start' and 'end' statement. This
    // enforces a system of FILO (first in, last out), which makes sure that
    // resources are not accidentally deleted while in-use.
    
    internal class BlockStack
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
