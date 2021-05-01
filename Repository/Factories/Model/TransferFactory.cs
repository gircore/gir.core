using Repository.Model;

namespace Repository.Factories
{
    internal class TransferFactory
    {
        public Transfer FromText(string? text)
        {
            return text switch
            {
                "none" => Transfer.None,
                "container" => Transfer.Container,
                "full" => Transfer.Full,
                "floating" => Transfer.None,
                _ => Transfer.Unknown
            };
        }
    }
}
