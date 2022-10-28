using ContactList.DAL.DataAccess;
using ContactList.DAL.Interfaces;
using ContactList.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.DAL.Repositories
{

    public class ContactRepository : IContactRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public ContactRepository(ApplicationDBContext appdbcont)
        {
            _dbContext = appdbcont;
        }
        public async Task CreateAsync(Contact cont)
        {
            var contact = new Contact
            {
                FirstName = cont.FirstName,
                LastName = cont.LastName,
                Email = cont.Email,
                Address = cont.Address,
                PhoneNumber = cont.PhoneNumber,

            };

            _dbContext.Contacts.Add(contact);
            await _dbContext.SaveChangesAsync();

        }

        public async Task DeleteAsync(int Contactid)
        {
            var contact = _dbContext.Contacts.FirstOrDefault(c => c.Id == Contactid);
            if (contact == null)
            {
                return;
            }
            _dbContext.Contacts.Remove(contact);
            await _dbContext.SaveChangesAsync();
                

        }

        public async Task<Contact> GetById(int id)
        {
            var contact = await _dbContext.Contacts.FirstOrDefaultAsync(c => c.Id == id);
            if(contact == null)
            {
                throw new Exception();
            }
            return contact;
        }

        public List<Contact> GetContacts()
        {
            return _dbContext.Contacts.ToList();
        }

        public async Task UpdateAsync(Contact cont)
        {
            var contact = _dbContext.Contacts.FirstOrDefault(c => c.Id == cont.Id);

            if(contact == null)
            {
                return;
            }

            contact.FirstName = cont.FirstName;
            contact.LastName = cont.LastName;
            contact.Email = cont.Email;
            contact.PhoneNumber = cont.PhoneNumber;
            contact.Address = cont.Address;


            _dbContext.Contacts.Update(contact);
            await _dbContext.SaveChangesAsync();
        }
    }
}
