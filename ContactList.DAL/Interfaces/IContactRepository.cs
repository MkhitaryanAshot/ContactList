using ContactList.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.DAL.Interfaces
{
    public interface IContactRepository
    {
        Task CreateAsync(Contact cont);
        Task DeleteAsync(int Contactid);
        Task UpdateAsync(Contact cont);

        List<Contact> GetContacts();

        Task<Contact> GetById(int id);
    }
}
