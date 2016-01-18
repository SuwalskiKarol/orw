using System.Data.Entity;

namespace VSMMvcTDD.Entities
{
    public class ContactContext : DbContext
    {
        public virtual DbSet<Contact> Contacts { get; set; }
    }
}
