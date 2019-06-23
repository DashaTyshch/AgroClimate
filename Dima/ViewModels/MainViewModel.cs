using Dima.Database.Entities;
using Dima.Database.Models;
using Dima.Database.Services;
using Dima.Models;
using Dima.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace Dima.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        #region Private Fields
        private ICommand _addRequestCommand;
        private ICommand _addEngineerCommand;
        private ICommand _cellCommand;
        private ICommand _exitCommand;
        private ICommand _addBrigadierCommand;
        private MainModel Model { get; }

        private ObservableCollection<RequestsInfo> _reguestItems;
        private DataGridCellInfo _cellInfo;
        #endregion

        public MainViewModel()
        {
            Model = new MainModel();

            RequestItems = new ObservableCollection<RequestsInfo>(Model.GetRequestItems());
        }

        public ObservableCollection<RequestsInfo> RequestItems
        {
            get => _reguestItems;
            set
            {
                _reguestItems = value;
                InvokePropertyChanged(nameof(RequestItems));
            }
        }

        public DataGridCellInfo CellInfo
        {
            get { return _cellInfo; }
            set
            {
                _cellInfo = value;
                InvokePropertyChanged(nameof(CellInfo));
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
  
        public ICommand AddBrigadierCommand
        {
            get
            {
                if (_addBrigadierCommand == null)
                {
                    _addBrigadierCommand = new RelayCommand<object>(AddBrigadierExecute, AddBrigadierCanExecute);
                }

                return _addBrigadierCommand;
            }
            set
            {
                _addBrigadierCommand = value;
                InvokePropertyChanged(nameof(AddBrigadierCommand));
            }
        }

        private void AddBrigadierExecute(object obj)
        {
            Model.AddBrigadier();
        }

        private bool AddBrigadierCanExecute(object obj)
        {
            return true;
        }

        public ICommand CellClickedCommand
        {
            get
            {
                if (_cellCommand == null)
                {
                    _cellCommand = new RelayCommand<object>(CellClickExecute, CellClickCanExecute);
                }
                return _cellCommand;
            }
            set
            {
                _cellCommand = value;
                InvokePropertyChanged(nameof(CellClickedCommand));
            }
        }

        private bool CellClickCanExecute(object obj)
        {
            return true;
        }

        private void CellClickExecute(object obj)
        {
            var selectedSell = obj as DataGridCellInfo?;
            var selectedInfo = selectedSell.Value.Item as RequestsInfo;
            var column = selectedSell.Value.Column.Header.ToString();

            Model.ProcessCellSelection(column, selectedInfo);
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
