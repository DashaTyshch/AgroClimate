using Dima.Database.Entities;
using Dima.Models;
using Dima.Tools;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Input;

namespace Dima.ViewModels
{
    class RequestViewModel : INotifyPropertyChanged
    {
        private Request _request;
        private Project _selectedProject;
        private ObservableCollection<Project> _projects;
        private readonly RequestModel _model;

        private ICommand _saveFileCommand;
        private ICommand _addProjectCommand;

        public RequestViewModel()
        {
            _model = new RequestModel();
            Request = Storage.GetInstance().SelectedRequest;
            Projects = new ObservableCollection<Project>(_model.GetProjects(Request.Request_Name));
        }

        public Request Request
        {
            get => _request;
            set
            {
                _request = value;
                InvokePropertyChanged(nameof(Request));
            }
        }

        public Project SelectedProject
        {
            get => _selectedProject;
            set
            {
                _selectedProject = value;
                InvokePropertyChanged(nameof(SelectedProject));
            }
        }

        public ObservableCollection<Project> Projects
        {
            get => _projects;
            set
            {
                _projects = value;
                InvokePropertyChanged(nameof(Projects));
            }
        }

        #region Commands

        public ICommand AddProjectCommand
        {
            get
            {
                if (_addProjectCommand == null)
                    _addProjectCommand = new RelayCommand<object>(AddProjectCommandExecute, AddProjectCommandCanExecute);
                return _addProjectCommand;
            }
            set
            {
                _addProjectCommand = value;
                InvokePropertyChanged(nameof(AddProjectCommand));
            }
        }

        private bool AddProjectCommandCanExecute(object obj)
        {
            return true;
        }

        private void AddProjectCommandExecute(object obj)
        {
            byte[] fileContent;
            var filePath = string.Empty;

            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                InitialDirectory = @"C:\",
                RestoreDirectory = true,
                Title = "Виберіть файл для завантаження",
                Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*",
                CheckFileExists = true,
                CheckPathExists = true,

            };

            if (openFileDialog.ShowDialog() == true)
            {
                filePath = openFileDialog.FileName;
                fileContent = File.ReadAllBytes(filePath);

                _model.CreateProject(fileContent);
            }

        }

        public ICommand SaveFileCommand
        {
            get
            {
                if (_saveFileCommand == null)
                    _saveFileCommand = new RelayCommand<object>(SaveFileExecute, SaveFileCanExecute);
                return _saveFileCommand;
            }
            set
            {
                _saveFileCommand = value;
                InvokePropertyChanged(nameof(SaveFileCommand));
            }
        }

        private bool SaveFileCanExecute(object obj)
        {
            return SelectedProject != null;
        }

        private void SaveFileExecute(object obj)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                //Filter = "Files (*" + fileExtension + ")|*" + fileExtension,
                Title = "Зберегти файл як",
                CheckPathExists = true,
                FileName = Request.Request_Name
            };

            if (saveFileDialog.ShowDialog() == false)
                return;

            File.WriteAllBytes(saveFileDialog.FileName, SelectedProject.ProjFile);
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
