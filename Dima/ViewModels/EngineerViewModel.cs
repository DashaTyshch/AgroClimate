using Dima.Database.Entities;
using Dima.Models;
using Dima.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace Dima.ViewModels
{
    class EngineerViewModel : INotifyPropertyChanged
    {
        #region Private Fields
        private EngineerAgroclimate _engineer;

        private bool _isViewing;
        private bool _isEditing;

        private ICommand _editCommand;

        private EngineerModel Model { get; }
        #endregion

        public EngineerViewModel()
        {
            Model = new EngineerModel();
            Engineer = Storage.GetInstance().SelectedEngineer;

            IsEditing = false;
            IsViewing = true;
        }

        public EngineerAgroclimate Engineer
        {
            get => _engineer;
            set
            {
                _engineer = value;
                InvokePropertyChanged(nameof(Engineer));
            }
        }

        #region Modes Properties
        public bool IsViewing
        {
            get { return _isViewing; }
            set
            {
                _isViewing = value;
                InvokePropertyChanged(nameof(IsViewing));
            }
        }

        public bool IsEditing
        {
            get { return _isEditing; }
            set
            {
                _isEditing = value;
                InvokePropertyChanged(nameof(IsEditing));
            }
        }
        #endregion

        public ICommand EditCommand
        {
            get
            {
                if (_editCommand == null)
                    _editCommand = new RelayCommand<object>(EditCommandExecute, EditCommandCanExecute);
                return _editCommand;
            }
            set
            {
                _editCommand = value;
                InvokePropertyChanged(nameof(EditCommand));
            }
        }

        private bool EditCommandCanExecute(object obj)
        {
            return true;
        }

        private void EditCommandExecute(object obj)
        {
            IsViewing = false;
            IsEditing = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void InvokePropertyChanged(string propertyName)
        {
            var e = new PropertyChangedEventArgs(propertyName);
            PropertyChanged?.Invoke(this, e);
        }
    }
}
