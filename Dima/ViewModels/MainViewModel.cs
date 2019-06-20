using Dima.Database.Entities;
using Dima.Database.Models;
using Dima.Database.Services;
using Dima.Models;
using Dima.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace Dima.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        private ICommand _addRequestCommand;
        private ICommand _addEngineerCommand;
        private ICommand _exitCommand;

        private MainModel Model { get; }

        public MainViewModel()
        {
            Model = new MainModel();
            //PostgresService.Instance.GetRequestsInfo();

            RequestItems = new ObservableCollection<RequestsInfo>(Model.GetRequestItems());
        }

        private ObservableCollection<RequestsInfo> _reguestItems;
        public ObservableCollection<RequestsInfo> RequestItems
        {
            get => _reguestItems;
            set
            {
                _reguestItems = value;
                InvokePropertyChanged(nameof(RequestItems));
            }
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

        public ICommand AddEngineerCommand
        {
            get
            {
                if (_addEngineerCommand == null)
                {
                    _addEngineerCommand = new RelayCommand<object>(AddEngineerExecute, AddEngineerCanExecute);
                }

                return _addEngineerCommand;
            }
            set
            {
                _addEngineerCommand = value;
                InvokePropertyChanged(nameof(AddEngineerCommand));
            }
        }

        private void AddEngineerExecute(object obj)
        {
            Model.AddEngineer();
        }

        private bool AddEngineerCanExecute(object obj)
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
