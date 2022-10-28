using ContactList.DAL.Interfaces;
using ContactList.Domain.Entites;
using ContactList.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ContactList.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IContactRepository _contactRepository;

        public HomeController(ILogger<HomeController> logger,IContactRepository contact )
        {
            _logger = logger;
            _contactRepository = contact;
        }

        public IActionResult Index()
        {
            var contact = _contactRepository.GetContacts();
            var contactviewmodel = new List<ContactViewModel>();

            for (int i = 0; i < contact.Count; i++)
            {
                var contactviewmodels = 
                    new ContactViewModel
                    {
                        Address = contact[i].Address,
                        PhoneNumber = contact[i].PhoneNumber,
                        FirstName = contact[i].FirstName,
                        LastName = contact[i].LastName,
                        Email = contact[i].Email,
                        Id = contact[i].Id

                    };

                contactviewmodel.Add( contactviewmodels );
            }






            return View(contactviewmodel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ContactViewModel cont)
        {

            var contact = new Contact
            { 
                Address= cont.Address,
                PhoneNumber= cont.PhoneNumber,
                FirstName= cont.FirstName,
                LastName= cont.LastName,
                Email= cont.Email,
            };
            await _contactRepository.CreateAsync(contact);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var contact = await _contactRepository.GetById(id);
            var contactViewModel = new ContactViewModel
            {
                Address = contact.Address,
                Email = contact.Email,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                PhoneNumber = contact.PhoneNumber,
               
                
            };
            return View(contactViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ContactViewModel cont)
        {
            var contact1 = new Contact
            {
                Address = cont.Address,
                Email = cont.Email,
                FirstName = cont.FirstName,
                LastName = cont.LastName,
                PhoneNumber = cont.PhoneNumber,
                Id = cont.Id
                
            };

            await _contactRepository.UpdateAsync(contact1);
            return RedirectToAction("Index");

        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _contactRepository.DeleteAsync(id);
            return RedirectToAction("Index");
        }

       

        public async Task<IActionResult> SingleContact(int id)
        {
            var contact =await  _contactRepository.GetById(id);
            var contactviewmodel = new ContactViewModel
            {
                Address = contact.Address,
                FirstName = contact.FirstName,
                LastName= contact.LastName,
                Email = contact.Email,
                PhoneNumber = contact.PhoneNumber
            };


            return View(contactviewmodel);
            
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}