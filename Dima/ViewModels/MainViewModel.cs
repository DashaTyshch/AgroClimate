using Dima.Models;
using Dima.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace Dima.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        private ICommand _addRequestCommand;
        private ICommand _exitCommand;

        private MainModel Model { get; }

        public MainViewModel()
        {
            Model = new MainModel();
        }

        #region Commands
        public ICommand AddRequestCommand
        {
            get
            {
                if (_addRequestCommand == null)
                {
                    _addRequestCommand = new RelayCommand<object>(AddRequestExecute, AddRequestCanExecute);
                }

                return _addRequestCommand;
            }
            set
            {
                _addRequestCommand = value;
                InvokePropertyChanged(nameof(AddRequestCommand));
            }
        }

        private void AddRequestExecute(object obj)
        {
            Model.AddRequest();
        }

        private bool AddRequestCanExecute(object obj)
        {
            return true;
        }

        public ICommand ExitCommand
        {
            get
            {
                if (_exitCommand == null)
                {
                    _exitCommand = new RelayCommand<object>(ExitExecute, ExitCanExecute);
                }

                return _exitCommand;
            }
            set
            {
                _exitCommand = value;
                InvokePropertyChanged(nameof(ExitCommand));
            }
        }

        private void ExitExecute(object obj)
        {
            Model.Exit();
        }

        private bool ExitCanExecute(object obj)
        {
            return true;
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        private void InvokePropertyChanged(string propertyName)
        {
            var e = new PropertyChangedEventArgs(propertyName);
            PropertyChanged?.Invoke(this, e);
        }
    }
}
