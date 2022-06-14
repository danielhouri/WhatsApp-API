using API.Data;

namespace API.Services
{
    public class TransferService : ITransferService
    {
        private readonly APIContext _context;

        public TransferService(APIContext Context)
        {
            _context = Context;
        }

        public bool AddMessage(string from, string to, string content)
        {
            var cnt = _context.Contact.SingleOrDefault(u => (u.Id == from) && (u.User.Username == to));
            if (cnt != null)
            {
                var msg = new Message() { Send = false, Created= DateTime.Now, Content = content, Contact= cnt };
                cnt.Last = content;
                _context.Message.Add(msg);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
