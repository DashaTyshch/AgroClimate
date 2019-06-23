using Dima.Models;
using Dima.Tools;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Dima.ViewModels
{
    class LoginViewModel : INotifyPropertyChanged
    {
        private ICommand _loginCommand;

        private LoginModel Model { get; }

        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    InvokePropertyChanged(nameof(Name));
                }
            }
        }

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
            var btn = obj as PasswordBox;

            if (!Model.Login(Name, btn.Password))
                MessageBox.Show("Невірний логін або пароль!");
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
