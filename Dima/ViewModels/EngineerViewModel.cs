﻿using Dima.Database.Entities;
using Dima.Models;
using Dima.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
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
        private ICommand _reportCommand;
        private ICommand _saveCommand;
        private ICommand _backCommand;

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

        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                    _saveCommand = new RelayCommand<object>(SaveCommandExecute, SaveCommandCanExecute);
                return _saveCommand;
            }
            set
            {
                _saveCommand = value;
                InvokePropertyChanged(nameof(SaveCommand));
            }
        }

        public ICommand ReportCommand
        {
            get
            {
                if (_reportCommand == null)
                    _reportCommand = new RelayCommand<object>(
                        (object _) => new EngineerReportModel().Report(Engineer.Tab_Number, Engineer.Last_Name),
                        (object _) => true
                   );
                return _reportCommand;
            }
            set
            {
                _reportCommand = value;
                InvokePropertyChanged(nameof(ReportCommand));
            }
        }

        private bool SaveCommandCanExecute(object obj)
        {
            return !string.IsNullOrEmpty(Engineer.First_Name) &&
                   !string.IsNullOrEmpty(Engineer.Patronym) &&
                   !string.IsNullOrEmpty(Engineer.Last_Name) &&
                   !string.IsNullOrEmpty(Engineer.Telephone_Number);
        }

        private void SaveCommandExecute(object obj)
        {
            Model.UpdateEngineer(Engineer);

            IsViewing = true;
            IsEditing = false;
        }

        public ICommand BackCommand
        {
            get
            {
                if (_backCommand == null)
                    _backCommand = new RelayCommand<object>(BackCommandExecute, BackCommandCanExecute);
                return _backCommand;
            }
            set
            {
                _backCommand = value;
                InvokePropertyChanged(nameof(BackCommand));
            }
        }

        private bool BackCommandCanExecute(object obj)
        {
            return true;
        }

        private void BackCommandExecute(object obj)
        {
            if (IsEditing)
            {
                MessageBoxResult result = MessageBox.Show("Ви зберегли зміни? Вийти?", "Увага!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        Model.GoBack();
                        break;
                    case MessageBoxResult.No:
                        break;
                }
            }

            Model.GoBack();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void InvokePropertyChanged(string propertyName)
        {
            var e = new PropertyChangedEventArgs(propertyName);
            PropertyChanged?.Invoke(this, e);
        }
    }
}
