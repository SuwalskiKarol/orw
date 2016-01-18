using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Grid.Mvc.Ajax.GridExtensions;
using VSMMvcTDD.Entities;
using VSMMvcTDD.GridExtensions;
using VSMMvcTDD.Models;
using VSMMvcTDD.Services;

namespace VSMMvcTDD.Controllers
{
    public class HomeController : Controller
    {
        private readonly IContactService _contactService;
        private readonly IAjaxGridFactory _ajaxGridFactory;
        private const int _partitionSize = 10;

        public HomeController(IContactService contactService,
            IAjaxGridFactory ajaxGridFactory)
        {
            _contactService = contactService;
            _ajaxGridFactory = ajaxGridFactory;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var contacts = _contactService.GetAllContacts();
            var model = new ContactIndexModel();
            var gridData = from contact in contacts
                           orderby contact.LastName, contact.FirstName
                           select new ContactViewModel()
                           {
                               Id = contact.Id,
                               Email = contact.Email,
                               FirstName = contact.FirstName,
                               LastName = contact.LastName
                           };
            var grid = _ajaxGridFactory.CreateAjaxGrid(gridData, 1, false, _partitionSize);
            model.Contacts = grid as AjaxGrid<ContactViewModel>;
            return View(model);
        }

        [HttpGet]
        public JsonResult ContactsGrid(int? page, bool? renderRowsOnly)
        {
            var contacts = _contactService.GetAllContacts();
            var gridData = from contact in contacts
                           orderby contact.LastName, contact.FirstName
                           select new ContactViewModel()
                           {
                               Id = contact.Id,
                               Email = contact.Email,
                               FirstName = contact.FirstName,
                               LastName = contact.LastName
                           };
            var model = _ajaxGridFactory.CreateAjaxGrid(gridData.AsQueryable(), page ?? 1,
                renderRowsOnly ?? page.HasValue, _partitionSize);
            return Json(new
            {
                Html = model.ToJson("_ContactsGrid", this),
                model.HasItems
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new ContactViewModel();
            return PartialView("_Create", model);
        }

        [HttpPost]
        public ActionResult Create(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                int id = _contactService.AddContact(new Contact()
                 {
                     FirstName = model.FirstName,
                     LastName = model.LastName,
                     Email = model.Email
                 });
                return Json(new { Success = true, Object = id }, JsonRequestBehavior.AllowGet);
            }
            return PartialView("_Create", model);
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var contact = _contactService.GetContact(id);
            var model = new ContactViewModel()
            {
                Id = contact.Id,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Email = contact.Email
            };
            return PartialView("_Edit", model);
        }


        [HttpPut]
        public ActionResult Edit(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                _contactService.EditContact(new Contact()
                {
                    Id = model.Id,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email
                });
                return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
            }
            return PartialView("_Edit", model);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            _contactService.DeleteContact(id);
            return Json(new {Success = true}, JsonRequestBehavior.AllowGet);
        }
    }
}