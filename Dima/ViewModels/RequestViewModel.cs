using Dima.Database.Entities;
using Dima.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace Dima.ViewModels
{
    class RequestViewModel : INotifyPropertyChanged
    {
        private Request _request;
        private ObservableCollection<Project> _projects;
        private readonly RequestModel _model;

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

        public ObservableCollection<Project> Projects
        {
            get => _projects;
            set
            {
                _projects = value;
                InvokePropertyChanged(nameof(Projects));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void InvokePropertyChanged(string propertyName)
        {
            var e = new PropertyChangedEventArgs(propertyName);
            PropertyChanged?.Invoke(this, e);
        }

    }
}
