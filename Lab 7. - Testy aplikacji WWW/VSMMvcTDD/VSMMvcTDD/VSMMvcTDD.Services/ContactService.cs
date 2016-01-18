using System;
using System.Collections.Generic;
using System.Linq;
using VSMMvcTDD.Entities;

namespace VSMMvcTDD.Services
{
    public class ContactService : IContactService
    {
        public IQueryable<Contact> GetAllContacts()
        {
            throw new NotImplementedException();
        }

        public int AddContact(Contact contact)
        {
            throw new NotImplementedException();
        }

        public Contact GetContact(int id)
        {
            throw new NotImplementedException();
        }

        public void EditContact(Contact contact)
        {
            throw new NotImplementedException();
        }

        public void DeleteContact(int id)
        {
            throw new NotImplementedException();
        }
    }
}