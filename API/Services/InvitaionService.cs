using API.Data;

namespace API.Services
{
    public class InvitaionService : IInvitaionService
    {
        private readonly APIContext _context;

        public InvitaionService(APIContext Context)
        {
            _context = Context;
        }

        public bool AddContact(string from, string to, string server)
        {
            var usr = _context.User.SingleOrDefault(u => u.Username == to);
            if (usr != null)
            {
                var cnt = new Contact() { Id = from, Server = server, Name=from, LastDate = DateTime.Now, User = usr, Message = new List<Message>() };
                try
                {
                    _context.Contact.Add(cnt);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    return false;
                }

                return true;
            }
            return false;
        }
    }
}
