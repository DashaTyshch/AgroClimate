using Dima.Database.Entities;
using Dima.Database.Services;
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

        public void Download(Project selectedProject)
        {
            throw new NotImplementedException();
        }

        public void CreateProject(byte[] fileContent)
        {
            PostgresService.Instance.AddProject(fileContent, "Агроавіс-1");
        }

        public void Confirm(int projId)
        {
            PostgresService.Instance.ConfirmProj(projId);
        }
    }
}
