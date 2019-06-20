using Dima.Database.Entities;

namespace Dima.Database.Models
{
    public class RequestsInfo
    {
        public string Request_Name { get; set; }
        public string Company_Name { get; set; }
        public string Engineer_Name { get; set; }
        public string Brigadier_Name { get; set; }
        public Reqstate State { get; set; }
    }
}
