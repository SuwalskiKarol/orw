using System;
using System.ComponentModel.DataAnnotations;

namespace VSMMvcTDD.Models
{
    public class ContactViewModel : IEquatable<ContactViewModel>
    {
        public bool Equals(ContactViewModel other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && string.Equals(FirstName, other.FirstName) && string.Equals(LastName, other.LastName) && string.Equals(Email, other.Email);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Id;
                hashCode = (hashCode*397) ^ (FirstName != null ? FirstName.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (LastName != null ? LastName.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Email != null ? Email.GetHashCode() : 0);
                return hashCode;
            }
        }

        public static bool operator ==(ContactViewModel left, ContactViewModel right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ContactViewModel left, ContactViewModel right)
        {
            return !Equals(left, right);
        }

        public int Id { get; set; }

        [Display(Name = "First Name")]
        [StringLength(255)]
        public String FirstName { get; set; }

        [Display(Name = "Last Name")]
//        [StringLength(255)]
       [Required]
        public String LastName { get; set; }

       // [StringLength(255)]
        public String Email { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ContactViewModel) obj);
        }
    }
}