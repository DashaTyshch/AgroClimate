using Dima.Database.Entities;
using Dima.Database.Services;
using Dima.Managers;
using Dima.Tools;
using System;

namespace Dima.Models
{
    public class AddEngineerModel
    {

        public void GoBack()
        {
            NavigationManager.Instance.Navigate(ModesEnum.Main);
        }

        public bool CreateNewEngineer(EngineerAgroclimate engineer)
        {
            PostgresService.Instance.AddEngineer(engineer);
            return true;
        }
    }
}
