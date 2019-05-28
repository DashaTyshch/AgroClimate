using System;

namespace Dima.Database.Entities
{
    public class Request
    {
        public string Request_Name { get; set; }
        public int Tab_Number { get; set; }
        public DateTime Creation_Date { get; set; }
        public DateTime Finish_Date { get; set; }
        public int? Approximate_Duration { get; set; }
        public int? Real_Duration { get; set; }
        public int Code_edrpou { get; set; }
        public int Telephone_Number_Of_Brigadier { get; set; }
        public Reqstate StateReq { get; set; }
    }
}
