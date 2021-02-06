using Repository.Model;

namespace Repository.Factories
{
    public interface ITransferFactory
    {
        Transfer FromText(string? text);
    }

    public class TransferFactory : ITransferFactory
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
