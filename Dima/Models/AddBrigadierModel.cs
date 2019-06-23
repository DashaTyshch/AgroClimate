using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dima.Database.Entities;
using Dima.Database.Services;
using Dima.Managers;
using Dima.Tools;

namespace Dima.Models
{
    public class AddBrigadierModel
    {

        public void GoBack()
        {
            NavigationManager.Instance.Navigate(ModesEnum.Main);
        }

        public bool CreateNewBrigadier(Brigadier Brigadiere)
        {
            PostgresService.Instance.AddBrigadier(Brigadiere);
            return true;
        }
    }
}
