namespace GirLoader.Test
{
    internal static class Helper
    {
        internal static Input.Repository GetInputRepository(string namespaceName, string version)
        {
            return new Input.Repository()
            {
                Namespace = new Input.Namespace()
                {
                    Name = namespaceName,
                    Version = version
                }
            };
        }
    }
}
