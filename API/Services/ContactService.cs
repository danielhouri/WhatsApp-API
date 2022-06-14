using API.Data;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace API.Services
{
    public class ContactService : IContactService
    {
        private readonly APIContext _context;

        public ContactService (APIContext Context)
        {
            _context = Context;
        }



        public async Task<bool> Add(Contact contact, string username)
        {
            var user = _context.User.SingleOrDefault(u => u.Username == username);

            if (GetSpecific(contact.Id, username) == null)
            {
                contact.User = user;
                contact.LastDate = DateTime.Now;
                try
                {
                    await _context.Contact.AddAsync(contact);
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return false;
        }

        public async Task<bool> AddMessage(string Id, string Content, string username)
        {
            var user = _context.User.SingleOrDefault(u => u.Username == username);
            var cnt = _context.Contact.SingleOrDefault(u => (u.Id == Id) && (u.User.Username == username));
            var msg = new Message() { Contact = cnt, Created = DateTime.Now, Send = true, Content = Content };

            if ((user != null) && (cnt != null) && (msg != null))
            {
                cnt.Last = Content;
                cnt.LastDate = msg.Created;
                try
                {
                    _context.Contact.Update(cnt);
                    await _context.Message.AddAsync(msg);
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }

        public void Delete(string Id, string username)
        {
            var result = _context.Contact.SingleOrDefault(c => (c.Id == Id) && (c.User.Username == username));

            if(result != null)
            {
                var msg = GetAllMessages(Id, username);
                if (msg != null)
                {
                    foreach(var m in msg)
                    {
                        _context.Message.Remove(m);
                    }
                }
                _context.Contact.Remove(result);
                _context.SaveChanges();
            }
        }

        public void DeleteMessage(string Id, int IdMessage, string username)
        {
            var message = GetSpecificMessage(Id, IdMessage, username);
            if (message != null)
            {
                _context.Message.Remove(message);
                _context.SaveChanges();
            }
        }

        public async Task<bool> Edit(string Id, string Name, string Server, string username)
        {
            var result = _context.Contact.SingleOrDefault(c => c.Id == Id && c.User.Username == username);
            if (result != null)
            {
                result.Name = Name;
                result.Server = Server;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Contact>> GetAll(string username)
        {
            var res = _context.Contact.Where(e => string.Compare(e.User.Username, username) == 0);
            return await res.ToListAsync();
        }

        public List<Message> GetAllMessages(string Id, string username)
        {
            return (from u in _context.Message
                       where u.Contact.Id == Id && u.Contact.User.Username == username
                       select u).ToList();
        }

        public Contact? GetSpecific(string Id, string username)
        {
            return _context.Contact.FirstOrDefault(c => (c.Id == Id) && (c.User.Username == username));
        }

        public Message? GetSpecificMessage(string Id, int? IdMessage, string username)
        {
            return _context.Message.FirstOrDefault(m => (m.Id == IdMessage) && (m.Contact.Id == Id) && (m.Contact.User.Username == username));
        }

        public void EditMessage(string Id, int IdMessage, string Content, string username)
        {
            var msg = GetSpecificMessage(Id, IdMessage, username);
            if (msg != null)
            {
                msg.Content = Content;
                _context.SaveChanges();
            }
        }
    }
}
