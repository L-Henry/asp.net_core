using ContactenLijst.Database;
using ContactenLijst.Domain;
using ContactenLijst.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ContactenLijst.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactDatabase _contactDabase;
        public ContactController(IContactDatabase contactDatabase)
        {
            _contactDabase = contactDatabase;
        }


        public IActionResult Index() {
            List<ContactListViewModel> contacts = new List<ContactListViewModel>();
            IEnumerable<Contact> contactsFromDatabase = _contactDabase.GetContacts();

            foreach (var contact in contactsFromDatabase)
            {
                contacts.Add(new ContactListViewModel()
                {
                    Id = contact.Id,
                    Naam = contact.Voornaam + " " + contact.Naam
                });
            }


            return View(contacts);
        }

        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ContactCreateViewModel model) {
            if (!TryValidateModel(model))
            {
                return View();
            }

            Contact contact = new Contact()
            {
                Voornaam = model.Voornaam,
                Naam = model.Naam,
                Geslacht = model.Geslacht,
                Geboortedatum = model.Geboortedatum,
                Adres = model.Adres,
                Beschrijving = model.Beschrijving,
                Email = model.Email,
                TelefoonNr = model.TelefoonNr,
          
        };

            using (var memoryStream = new MemoryStream())
            {
                await model.Avatar.CopyToAsync(memoryStream);
                contact.Avatar = memoryStream.ToArray();
            }

            _contactDabase.Insert(contact);

            return RedirectToAction("Detail", new { id = contact.Id });
        }

        public IActionResult Detail(int id) {
            Contact contactFromDb = _contactDabase.GetContact(id);

            ContactDetailViewModel contact = new ContactDetailViewModel()
            {
                Id = id,
                Voornaam = contactFromDb.Voornaam,
                Naam = contactFromDb.Naam,
                Geslacht = contactFromDb.Geslacht,
                Geboortedatum = contactFromDb.Geboortedatum,
                TelefoonNr = contactFromDb.TelefoonNr,
                Email = contactFromDb.Email,
                Beschrijving = contactFromDb.Beschrijving,
                Adres = contactFromDb.Adres,
                Avatar = contactFromDb.Avatar
            };

            return View(contact);
        }


        public IActionResult Update(int id) {
            Contact contactFromDb = _contactDabase.GetContact(id);
            ContactUpdateViewModel contactToUpdate = new ContactUpdateViewModel() {
                Voornaam = contactFromDb.Voornaam,
                Naam = contactFromDb.Naam,
                Geslacht = contactFromDb.Geslacht,
                Geboortedatum = contactFromDb.Geboortedatum,
                TelefoonNr = contactFromDb.TelefoonNr,
                Email = contactFromDb.Email,
                Beschrijving = contactFromDb.Beschrijving,
                Adres = contactFromDb.Adres
            };
            return View(contactToUpdate);
        }

        [HttpPost]
        public IActionResult Update(int id, ContactUpdateViewModel model) {
            if (!TryValidateModel(model))
            {
                return View();
            }

            Contact contactToUpdate = new Contact()
            {
                Id = id,
                Voornaam = model.Voornaam,
                Naam = model.Naam,
                Geslacht = model.Geslacht,
                Geboortedatum = model.Geboortedatum,
                TelefoonNr = model.TelefoonNr,
                Email = model.Email,
                Beschrijving = model.Beschrijving,
                Adres = model.Adres
            };
            _contactDabase.Update(id, contactToUpdate);

            return RedirectToAction("Detail", new { Id = id });
        }

        public IActionResult Delete(int id) {
            Contact contact = _contactDabase.GetContact(id);
            ContactDeleteViewModel contactToDelete = new ContactDeleteViewModel()
            {
                Id = id,
                Naam = contact.Voornaam + " " + contact.Naam
            };
            return View(contactToDelete);
        }

        [HttpPost]
        public IActionResult ConfirmDelete(int id) {
            _contactDabase.Delete(id);
            return RedirectToAction("Index");
        }

        public IActionResult AvatarUpdate(int id) {
            Contact contactFromDb = _contactDabase.GetContact(id);
            ContactAvatarUpdateViewModel mv = new ContactAvatarUpdateViewModel()
            {
                Avatar = contactFromDb.Avatar
            };

            return View(mv);
        }

        [HttpPost]
        public async Task<IActionResult> AvatarUpdate(int id, ContactAvatarUpdateViewModel model)
        {
            byte[] newAvatarImage = null;
            using (var memoryStream = new MemoryStream())
            {
                await model.NewAvatar.CopyToAsync(memoryStream);
                newAvatarImage = memoryStream.ToArray();
            }

            _contactDabase.AvatarUpdate(id, newAvatarImage);

            return RedirectToAction("Detail", new { Id = id });
        }

    }
}

