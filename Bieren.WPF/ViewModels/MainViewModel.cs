using Bieren.WPF.Services;
using Bieren.WPF.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Data;

namespace Bieren.WPF.ViewModels
{
    public class MainViewModel : WorkspaceViewModel
    {
        #region Fields
                
        private ReadOnlyCollection<CommandViewModel> _commands;
        private ObservableCollection<WorkspaceViewModel> _workspaces;
        private IDialogService _dialogService;
        private IDataService _dataService;

        #endregion // Fields

        #region Constructor

        public MainViewModel()
        {
            base.DisplayName = "Bieren";
            _dialogService = new DialogService();
            _dataService = new BierenDataService();//new MockDataService();//
        }

        #endregion // Constructor

        #region Commands

        /// <summary>
        /// Returns a read-only list of commands 
        /// that the UI can display and execute.
        /// </summary>
        public ReadOnlyCollection<CommandViewModel> Commands
        {
            get
            {
                if (_commands == null)
                {
                    List<CommandViewModel> cmds = this.CreateCommands();
                    _commands = new ReadOnlyCollection<CommandViewModel>(cmds);
                }
                return _commands;
            }
        }

        List<CommandViewModel> CreateCommands()
        {
            return new List<CommandViewModel>
            {
                new CommandViewModel("Bieren", new RelayCommand(ToonBieren)),
                new CommandViewModel("Brouwers", new RelayCommand(ToonBrouwers)),
                new CommandViewModel("Soorten", new RelayCommand(ToonSoorten)),
                new CommandViewModel("Users",new RelayCommand(ToonUsers))
            };
        }



        #endregion // Commands

        #region Workspaces

        /// <summary>
        /// Returns the collection of available workspaces to display.
        /// A 'workspace' is a ViewModel that can request to be closed.
        /// </summary>
        public ObservableCollection<WorkspaceViewModel> Workspaces
        {
            get
            {
                if (_workspaces == null)
                {
                    _workspaces = new ObservableCollection<WorkspaceViewModel>();
                    _workspaces.CollectionChanged += this.OnWorkspacesChanged;
                }
                return _workspaces;
            }
        }

        void OnWorkspacesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // Update WorkspaceViewModel.RequestClose event subscriptions
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.NewItems)
                    workspace.RequestClose += this.OnWorkspaceRequestClose;

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.OldItems)
                    workspace.RequestClose -= this.OnWorkspaceRequestClose;
        }

        void OnWorkspaceRequestClose(object sender, EventArgs e)
        {
            // Dispose and remove a closed workspace
            WorkspaceViewModel workspace = sender as WorkspaceViewModel;
            //workspace.Dispose();
            this.Workspaces.Remove(workspace);
        }

        #endregion // Workspaces

        #region Private Helpers
        private void ToonUsers()
        {
            var workspace = new UsersViewModel(_dataService);
            this.Workspaces.Add(workspace);
            this.SetActiveWorkspace(workspace);
        }
        void ToonBieren()
        {
            var workspace = new BierenViewModel(_dataService, _dialogService);
            this.Workspaces.Add(workspace);
            this.SetActiveWorkspace(workspace);
        }

        void ToonSoorten()
        {
            var workspace = new SoortenViewModel(_dataService);
            this.Workspaces.Add(workspace);
            this.SetActiveWorkspace(workspace);
        }

        void ToonBrouwers()
        {
            var workspace = new BrouwersViewModel(_dataService,_dialogService );
            this.Workspaces.Add(workspace);
            this.SetActiveWorkspace(workspace);
        }

        void SetActiveWorkspace(WorkspaceViewModel workspace)
        {
           // Debug.Assert(this.Workspaces.Contains(workspace));

            ICollectionView collectionView = CollectionViewSource.GetDefaultView(this.Workspaces);
            if (collectionView != null)
                collectionView.MoveCurrentTo(workspace);
        }

        #endregion // Private Helpers
    }
}
