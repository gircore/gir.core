namespace GirLoader.Output
{
    public abstract class Symbol
    {
        public string OriginalName { get; }

        protected Symbol(string originalName)
        {
            OriginalName = originalName;
        }
    }
}
