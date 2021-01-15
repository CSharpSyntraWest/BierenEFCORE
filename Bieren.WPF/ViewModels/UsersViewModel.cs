using Bieren.WPF.Models;
using Bieren.WPF.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Bieren.WPF.ViewModels
{
    public class UsersViewModel : WorkspaceViewModel
    {
        private IDataService _dataService;
        private User _selectedUser;
        private ObservableCollection<User> _users;

        public UsersViewModel(IDataService dataService)
        {
            _dataService = dataService;
            _users = new ObservableCollection<User>(_dataService.GeefAlleUsers());
        }

        public User SelectedUser
        {
            get { return _selectedUser; }
            set { OnPropertyChanged(ref _selectedUser, value); }
        }
        public ObservableCollection<User> Users
        {
            get { return _users; }
        }
    }
}
