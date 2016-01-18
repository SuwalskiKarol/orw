using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VSMMvcTDD.Entities;
using System.Data.Entity;

namespace VSMMvcTDD.Services.Tests
{
    [TestClass]
    public class ContactServiceTests
    {
        private Mock<ContactContext> _mockContactContext;
        private Mock<DbSet<Contact>> _mockContacts;
        private ContactService _contactService;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockContactContext = new Mock<ContactContext>();
            _mockContacts = new Mock<DbSet<Contact>>();
            _mockContactContext.Setup(x => x.Contacts).Returns(_mockContacts.Object);
            _contactService = new ContactService(_mockContactContext.Object);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _mockContactContext.VerifyAll();
        }

        [TestMethod]
        public void GetAllContacts_ExpectAllContactsReturned()
        {
            var stubData = (new List<Contact>
            {
                new Contact()
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe"
                },
                new Contact()
                {
                    Id = 2,
                    FirstName = "Jane",
                    LastName = "Doe"
                }
            }).AsQueryable();
            SetupTestData(stubData, _mockContacts);

            var actual = _contactService.GetAllContacts();

            CollectionAssert.AreEqual(stubData.ToList(), actual.ToList());
        }

        [TestMethod]
        public void AddContact_Given_contact_ExpectContactAdded()
        {
            var contact = new Contact()
            {
                FirstName = "John",
                LastName = "Doe"
            };
            const int expectedId = 1;
            _mockContactContext.Setup(x => x.SaveChanges()).Callback(() => contact.Id = expectedId);

            int id = _contactService.AddContact(contact);

            _mockContacts.Verify(x => x.Add(contact), Times.Once);
            _mockContactContext.Verify(x => x.SaveChanges(), Times.Once);
            Assert.AreEqual(expectedId, id);
        }

        [TestMethod]
        public void GetContact_Given_id_ExpectContactReturned()
        {
            int id = 2;
            var stubData = (new List<Contact>
            {
                new Contact()
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe"
                },
                new Contact()
                {
                    Id = 2,
                    FirstName = "Jane",
                    LastName = "Doe"
                }
            }).AsQueryable();
            SetupTestData(stubData, _mockContacts);

            var actual = _contactService.GetContact(id);

            Assert.AreEqual(stubData.ToList()[1], actual);
        }

        [TestMethod]
        public void EditContact_Given_contact_ExpectExistingContactUpdated()
        {
            var stubData = (new List<Contact>
            {
                new Contact()
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe"
                },
                new Contact()
                {
                    Id = 2,
                    FirstName = "Jane",
                    LastName = "Doe"
                }
            }).AsQueryable();
            SetupTestData(stubData, _mockContacts);
            var contact = new Contact()
            {
                Id = 1,
                FirstName = "Ted",
                LastName = "Smith",
                Email = "test@gmail.com"
            };

            _contactService.EditContact(contact);
            var actualContact = _mockContacts.Object.First();
            
            Assert.AreEqual(contact.Id, actualContact.Id);
            Assert.AreEqual(contact.FirstName, actualContact.FirstName);
            Assert.AreEqual(contact.LastName, actualContact.LastName);
            Assert.AreEqual(contact.Email, actualContact.Email);
            _mockContactContext.Verify(x => x.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void DeleteContact_Given_id_ExpectContactDeleted()
        {
            var stubData = (new List<Contact>
            {
                new Contact()
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe"
                },
                new Contact()
                {
                    Id = 2,
                    FirstName = "Jane",
                    LastName = "Doe"
                }
            }).AsQueryable();
            SetupTestData(stubData, _mockContacts);
            var contact = stubData.First();

            _contactService.DeleteContact(1);

            _mockContacts.Verify(x => x.Remove(contact), Times.Once);
            _mockContactContext.Verify(x => x.SaveChanges(), Times.Once);
        }

        private void SetupTestData<T>(IQueryable<T> testData, Mock<DbSet<T>> mockDbSet) where T : class
        {
            mockDbSet.As<IQueryable<Contact>>().Setup(m => m.Provider).Returns(testData.Provider);
            mockDbSet.As<IQueryable<Contact>>().Setup(m => m.Expression).Returns(testData.Expression);
            mockDbSet.As<IQueryable<Contact>>().Setup(m => m.ElementType).Returns(testData.ElementType);
            mockDbSet.As<IQueryable<Contact>>().Setup(m => m.GetEnumerator()).Returns((IEnumerator<Contact>) testData.GetEnumerator());
        }
    }
}
