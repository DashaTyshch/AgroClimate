using Dima.Managers;
using Dima.Tools;
using System;

namespace Dima.Models
{
    class LoginModel
    {
        public void Login()
        {
            NavigationManager.Instance.Navigate(ModesEnum.Main);
        }
    }
}
