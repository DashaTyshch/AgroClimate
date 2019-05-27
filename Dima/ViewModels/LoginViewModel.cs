using Dima.Models;
using Dima.Tools;
using System.ComponentModel;
using System.Windows.Input;

namespace Dima.ViewModels
{
    class LoginViewModel : INotifyPropertyChanged
    {
        private ICommand _loginCommand;

        private LoginModel Model { get; }

        public LoginViewModel()
        {
            Model = new LoginModel();
        }

        public ICommand LoginCommand
        {
            get
            {
                if (_loginCommand == null)
                {
                    _loginCommand = new RelayCommand<object>(LoginExecute, LoginCanExecute);
                }

                return _loginCommand;
            }
            set
            {
                _loginCommand = value;
                InvokePropertyChanged(nameof(LoginCommand));
            }
        }
        
        private void LoginExecute(object obj)
        {
            // TODO: Add parameters
            Model.Login();
        }

        private bool LoginCanExecute(object obj)
        {
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void InvokePropertyChanged(string propertyName)
        {
            var e = new PropertyChangedEventArgs(propertyName);
            PropertyChanged?.Invoke(this, e);
        }

    }
}
