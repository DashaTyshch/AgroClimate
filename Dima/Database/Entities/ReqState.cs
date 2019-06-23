
using System.ComponentModel;

namespace Dima.Database.Entities
{
    public enum Reqstate
    {
        [Description("Заплановано")]
        Planned,
        [Description("У процесі")]
        Ongoing,
        [Description("Завершено")]
        Completed
    }
}
