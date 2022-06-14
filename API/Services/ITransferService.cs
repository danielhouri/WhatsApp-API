namespace API.Services
{
    public interface ITransferService
    {
        public bool AddMessage(string from, string to, string content);
    }
}
