using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VSMMvcTDD.Entities;

namespace VSMMvcTDD.Services
{
    public interface IContactService
    {
        IQueryable<Contact> GetAllContacts();
        int AddContact(Contact contact);
        Contact GetContact(int id);
        void EditContact(Contact contact);
        void DeleteContact(int id);
    }
}
