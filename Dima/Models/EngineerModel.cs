using Dima.Database.Entities;
using Dima.Database.Services;

namespace Dima.Models
{
    public class EngineerModel
    {
        public void UpdateEngineer(EngineerAgroclimate engineer)
        {
            PostgresService.Instance.UpdateEngineer(engineer);
        }
    }
}
