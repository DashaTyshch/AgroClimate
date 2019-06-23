using Dima.Database.Entities;
using Dima.Models;
using Dima.Tools;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Dima.ViewModels
{
    class AddBrigadierViewModel : INotifyPropertyChanged
    {
        #region Private Fields
        private AddBrigadierModel Model { get; }

        private Brigadier _brigadierAgroclimate;

        private ICommand _backCommand;
        private ICommand _addBrigadier;
        #endregion

        public AddBrigadierViewModel()
        {
            Model = new AddBrigadierModel();

            Brigadier = new Brigadier();
        }

        public Brigadier Brigadier
        {
            get
            {
                return _brigadierAgroclimate;
            }
            set
            {
                _brigadierAgroclimate = value;
                InvokePropertyChanged(nameof(Brigadier));
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

        public ICommand AddBrigadier
        {
            get
            {
                if (_addBrigadier == null)
                    _addBrigadier = new RelayCommand<object>(AddBrigadierExecute, AddBrigadierCanExecute);
                return _addBrigadier;
            }
            set
            {
                _addBrigadier = value;
                InvokePropertyChanged(nameof(AddBrigadier));
            }
        }

        private bool AddBrigadierCanExecute(object obj)
        {
            return !string.IsNullOrEmpty(Brigadier.First_Name) &&
                   !string.IsNullOrEmpty(Brigadier.Patronym) &&
                   !string.IsNullOrEmpty(Brigadier.Last_Name);
            //!string.IsNullOrEmpty((Brigadiere.Tab_Number).ToString()) &&
            //!string.IsNullOrEmpty(Brigadiere.Telephone_Number);
        }

        private void AddBrigadierExecute(object obj)
        {
            //var res = Model.CreateNewEngineer(Engineer);
            //if (res == AddResult.Success)
            //{
            //    MessageBox.Show(res.GetDescription());
            //    Model.GoBack();
            //}
            //else MessageBox.Show(res.GetDescription());
            if (Model.CreateNewBrigadier(Brigadier))
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
