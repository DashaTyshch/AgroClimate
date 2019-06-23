using Dima.Database.Services;
using Dima.Managers;
using Dima.Tools;
using System;

namespace Dima.Models
{
    class LoginModel
    {
        public bool Login(string login, string pwd)
        {
            var name = PostgresService.Instance.Auth(login, pwd);
            if (name == null)
                return false;
            NavigationManager.Instance.Navigate(ModesEnum.Main);
            return true;
        }
    }
}
