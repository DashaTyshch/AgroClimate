using System;
using Dima.Database.Entities;
using Dima.Database.Services;
using Dima.Managers;

namespace Dima.Models
{
    public class EngineerModel
    {
        public void UpdateEngineer(EngineerAgroclimate engineer)
        {
            PostgresService.Instance.UpdateEngineer(engineer);
        }

        public void GoBack()
        {
            NavigationManager.Instance.Navigate(Tools.ModesEnum.Main);
        }
    }
}
