namespace GirLoader.Test
{
    internal static class Helper
    {
        internal static Input.Model.Repository GetInputRepository(string namespaceName, string version)
        {
            return new Input.Model.Repository()
            {
                Namespace = new Input.Model.Namespace()
                {
                    Name = namespaceName,
                    Version = version
                }
            };
        }
    }
}
