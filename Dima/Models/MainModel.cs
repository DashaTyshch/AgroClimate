using Dima.Database.Entities;
using Dima.Database.Models;
using Dima.Database.Services;
using Dima.Managers;
using Dima.Tools;
using System.Collections.Generic;

namespace Dima.Models
{
    class MainModel
    {
        public List<RequestsInfo> GetRequestItems()
        {
            return PostgresService.Instance.GetRequestsInfo();
        }

        public void AddRequest()
        {
            NavigationManager.Instance.Navigate(ModesEnum.AddRequest);
        }

        public void Exit()
        {
            NavigationManager.Instance.Navigate(ModesEnum.Login);
        }

        public void AddEngineer()
        {
            NavigationManager.Instance.Navigate(ModesEnum.AddEngineer);
        }
    }
}
