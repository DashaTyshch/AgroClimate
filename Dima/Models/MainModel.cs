using Dima.Managers;
using Dima.Tools;
using System;

namespace Dima.Models
{
    class MainModel
    {
        public void AddRequest()
        {
            NavigationManager.Instance.Navigate(ModesEnum.AddRequest);
        }

        public void Exit()
        {
            NavigationManager.Instance.Navigate(ModesEnum.Login);
        }
    }
}
