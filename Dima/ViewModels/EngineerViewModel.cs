using Dima.Database.Entities;
using Dima.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dima.ViewModels
{
    class EngineerViewModel : INotifyPropertyChanged
    {
        #region Private Fields
        private EngineerAgroclimate _engineer;

        private EngineerModel Model { get; }
        #endregion

        public EngineerViewModel()
        {
            Model = new EngineerModel();
            Engineer = Storage.GetInstance().SelectedEngineer;
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




        public event PropertyChangedEventHandler PropertyChanged;

        private void InvokePropertyChanged(string propertyName)
        {
            var e = new PropertyChangedEventArgs(propertyName);
            PropertyChanged?.Invoke(this, e);
        }
    }
}
