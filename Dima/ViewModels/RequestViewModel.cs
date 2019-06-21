using Dima.Database.Entities;
using Dima.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Dima.ViewModels
{
    class RequestViewModel : INotifyPropertyChanged
    {
        private Request _request;
        private readonly RequestModel _model;

        public RequestViewModel()
        {
            _model = new RequestModel();
            Request = Storage.GetInstance().SelectedRequest;
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



        public event PropertyChangedEventHandler PropertyChanged;

        private void InvokePropertyChanged(string propertyName)
        {
            var e = new PropertyChangedEventArgs(propertyName);
            PropertyChanged?.Invoke(this, e);
        }

    }
}
