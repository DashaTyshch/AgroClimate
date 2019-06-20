using Dima.Database.Entities;
using Dima.Models;
using Dima.Tools;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Dima.ViewModels
{
    class AddEngineerViewModel : INotifyPropertyChanged
    {
        #region Private Fields
        private AddEngineerModel Model { get; }

        private EngineerAgroclimate _engineerAgroclimate;

        private ICommand _backCommand;
        private ICommand _addEngineer;
        #endregion

        public AddEngineerViewModel()
        {
            Model = new AddEngineerModel();

            Engineer = new EngineerAgroclimate();
        }

        public EngineerAgroclimate Engineer
        {
            get
            {
                return _engineerAgroclimate;
            }
            set
            {
                _engineerAgroclimate = value;
                InvokePropertyChanged(nameof(Engineer));
            }
        }

        #region Commands
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
            Model.GoBack();
        }

        public ICommand AddEngineer
        {
            get
            {
                if (_addEngineer == null)
                    _addEngineer = new RelayCommand<object>(AddEngineerExecute, AddEngineerCanExecute);
                return _addEngineer;
            }
            set
            {
                _addEngineer = value;
                InvokePropertyChanged(nameof(AddEngineer));
            }
        }

        private bool AddEngineerCanExecute(object obj)
        {
            return !string.IsNullOrEmpty(Engineer.First_Name) &&
                   !string.IsNullOrEmpty(Engineer.Patronym) &&
                   !string.IsNullOrEmpty(Engineer.Last_Name) &&
                   !string.IsNullOrEmpty((Engineer.Tab_Number).ToString()) &&
                   !string.IsNullOrEmpty(Engineer.Telephone_Number);
        }

        private void AddEngineerExecute(object obj)
        {
            //var res = Model.CreateNewEngineer(Engineer);
            //if (res == AddResult.Success)
            //{
            //    MessageBox.Show(res.GetDescription());
            //    Model.GoBack();
            //}
            //else MessageBox.Show(res.GetDescription());
            if (Model.CreateNewEngineer(Engineer))
            {
                MessageBox.Show("Created.");
                Model.GoBack();
            }
            else
                MessageBox.Show("Error.");
        }
        #endregion


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
