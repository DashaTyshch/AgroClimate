using Dima.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Dima.Views
{
    /// <summary>
    /// Логика взаимодействия для LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        private LoginViewModel _loginViewModel;

        public LoginView()
        {
            InitializeComponent();
            _loginViewModel = new LoginViewModel();
            DataContext = _loginViewModel;
        }
    }
}
