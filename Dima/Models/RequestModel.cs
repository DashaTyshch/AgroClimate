using Dima.Database.Entities;
using Dima.Database.Services;
using Dima.Managers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dima.Models
{
    class RequestModel
    {

        public List<Project> GetProjects(string id)
        {
            return PostgresService.Instance.GetProjectsByReqName(id).OrderBy(_ => _.Id_Project).ToList();
        }

        public void CreateProject(byte[] fileContent, string name)
        {
            PostgresService.Instance.AddProject(fileContent, name);
        }

        public void Confirm(int projId)
        {
            PostgresService.Instance.ConfirmProj(projId);
        }

        public void GoBack()
        {
            NavigationManager.Instance.Navigate(Tools.ModesEnum.Main);
        }
    }
}
