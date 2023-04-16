using BookPhone.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookPhone.WPF.DataProvider
{
    public interface IContactDataProvider
    {
        Task<IEnumerable<Contact>> LoadContacts();
    }
}