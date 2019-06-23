using Dima.Database.Entities;
using Dima.Database.Services;
using Dima.Managers;
using Dima.Tools;
using System.Collections.Generic;

namespace Dima.Models
{
    class AddrequestModel
    {
        public List<Company> GetCompanyList()
        {
            return PostgresService.Instance.GetAllCompanies();
        }

        public List<EngineerAgroclimate> GetEngineerList()
        {
            return PostgresService.Instance.GetAllEngineers();
        }

        public List<Brigadier> GetBrigadierList()
        {
            return PostgresService.Instance.GetAllBrigadiers();
        }

        public void GoToMain()
        {
            NavigationManager.Instance.Navigate(ModesEnum.Main);
        }

        internal void AddRequest(string name, int duration, Company selectedCompany, EngineerAgroclimate selectedEngineer, Brigadier selectedBrigadier)
        {
            PostgresService.Instance.AddRequest(name, duration, selectedCompany, selectedEngineer, selectedBrigadier);
            GoToMain();
        }
    }
}
