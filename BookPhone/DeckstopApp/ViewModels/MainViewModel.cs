using BookPhone.Models;
using BookPhone.WPF.DataProvider;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace BookPhone.WPF.ViewModels
{
    public class MainViewModel : ViewModelBase 
    {
        private readonly IContactDataProvider _contactDataProvider;
        private Contact _selectedContact;
        public ObservableCollection<Contact> Contacts { get; } = new();

        public MainViewModel(IContactDataProvider contactDataProvider)
        {
            _contactDataProvider = contactDataProvider;
        }

        public async Task Load()
        {
            Contacts.Clear();
            var contacts = await _contactDataProvider.LoadContacts();
            foreach (var contact in contacts)
            {
                Contacts.Add(contact);
            }

            SelectedContact = Contacts.FirstOrDefault();
        }


        public Contact SelectedContact
        {
            get { return _selectedContact; }
            set
            {
                if (_selectedContact != value)
                {
                    _selectedContact = value;
                    RaisePropertyChanged();
                }

            }
        }
    }
}
