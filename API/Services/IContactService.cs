namespace API.Services
{
    public interface IContactService
    {
        public Task<IEnumerable<Contact>> GetAll(string username);

        public Contact? GetSpecific(string Id, string username);

        public Task<bool> Add(Contact contact, string username);

        public Task<bool> Edit(string Id, string Name, string Server, string username);

        public void Delete(string Id, string username);

        public List<Message> GetAllMessages(string Id, string username);

        public Message? GetSpecificMessage(string Id, int? IdMessage, string username);

        public void DeleteMessage(string Id, int IdMessage, string username);

        public Task<bool> AddMessage(string Id, string Content, string username);

        public void EditMessage(string Id, int IdMessage, string Content, string username);
    }
}
