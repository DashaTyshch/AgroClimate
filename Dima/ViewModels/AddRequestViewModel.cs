using Dima.Database.Entities;
using Dima.Models;
using Dima.Tools;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace Dima.ViewModels
{
    class AddRequestViewModel : INotifyPropertyChanged
    {
        private int _duration;
        private string _name;

        private List<Company> _companyList;
        private List<EngineerAgroclimate> _engineerList;
        private List<Brigadier> _brigadierList;

        private Company _selectedCompany;
        private EngineerAgroclimate _selectedEngineer;
        private Brigadier _selectedBrigadier;

        private ICommand _backCommand;
        private ICommand _addCommand;

        private AddrequestModel Model { get;  }

        public AddRequestViewModel()
        {
            Model = new AddrequestModel();

            CompanyList = Model.GetCompanyList();
            EngineerList = Model.GetEngineerList();
            BrigadierList = Model.GetBrigadierList();
        }

        public int Duration
        {
            get { return _duration; }
            set
            {
                if (value <= 0)
                    _duration = value;
                InvokePropertyChanged(nameof(Duration));
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                InvokePropertyChanged(nameof(Name));
            }
        }

        public List<Company> CompanyList
        {
            get { return _companyList; }
            set
            {
                _companyList = value;
                InvokePropertyChanged(nameof(CompanyList));
            }
        }

        public List<EngineerAgroclimate> EngineerList
        {
            get { return _engineerList; }
            set
            {
                _engineerList = value;
                InvokePropertyChanged(nameof(EngineerList));
            }
        }

        public List<Brigadier> BrigadierList
        {
            get { return _brigadierList; }
            set
            {
                _brigadierList = value;
                InvokePropertyChanged(nameof(BrigadierList));
            }
        }

        public Company SelectedCompany
        {
            get { return _selectedCompany; }
            set
            {
                _selectedCompany = value;
                InvokePropertyChanged(nameof(SelectedCompany));
            }
        }

        public EngineerAgroclimate SelectedEngineer
        {
            get { return _selectedEngineer; }
            set
            {
                _selectedEngineer = value;
                InvokePropertyChanged(nameof(SelectedEngineer));
            }
        }

        public Brigadier SelectedBrigadier
        {
            get { return _selectedBrigadier; }
            set
            {
                _selectedBrigadier = value;
                InvokePropertyChanged(nameof(SelectedBrigadier));
            }
        }

        public ICommand BackCommand
        {
            get
            {
                if (_backCommand == null)
                    _backCommand = new RelayCommand<object>(BackExecute, BackCanExecute);
                return _backCommand;
            }
            set
            {
                _backCommand = value;
                InvokePropertyChanged(nameof(BackCommand));
            }
        }

        private bool BackCanExecute(object obj)
        {
            return true;
        }

        private void BackExecute(object obj)
        {
            Model.GoToMain();
        }

        public ICommand AddCommand
        {
            get
            {
                if (_addCommand == null)
                    _addCommand = new RelayCommand<object>(AddCommandExecute, AddCommandCanExecute);
                return _addCommand;
            }
            set
            {
                _addCommand = value;
                InvokePropertyChanged(nameof(AddCommand));
            }
        }

        private bool AddCommandCanExecute(object obj)
        {
            return SelectedBrigadier != null &&
                SelectedCompany != null &&
                SelectedEngineer != null &&
                !string.IsNullOrEmpty(Duration.ToString());
        }

        private void AddCommandExecute(object obj)
        {
            Model.AddRequest(Name, Duration, SelectedCompany, SelectedEngineer, SelectedBrigadier);
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void InvokePropertyChanged(string propertyName)
        {
            var e = new PropertyChangedEventArgs(propertyName);
            PropertyChanged?.Invoke(this, e);
        }
        #endregion


    }
}
