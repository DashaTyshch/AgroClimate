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

        public void ProcessCellSelection(string column, RequestsInfo info)
        {
            switch (column)
            {
                case "Назва":
                    var res = PostgresService.Instance.GetRequestById(info.Request_Name);
                    Storage.GetInstance().ChangeSelectedRequest(res);
                    NavigationManager.Instance.Navigate(ModesEnum.Request);
                    break;
                case "Замовник":
                    //NavigationManager.Instance.Navigate(ModesEnum.Request);
                    break;
                case "Інженер":
                    //NavigationManager.Instance.Navigate(ModesEnum.Request);
                    break;
                case "Бригадир":
                    //NavigationManager.Instance.Navigate(ModesEnum.Request);
                    break;
                default:
                    break;
            }
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
