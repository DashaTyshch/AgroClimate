
namespace Dima.Database.Entities
{
    public class Project
    {
        public int Id_Project { get; set; }
        public string Request_Name { get; set; }
        public bool Status { get; set; }
        public byte[] ProjFile {  get; set; }
    }
}
