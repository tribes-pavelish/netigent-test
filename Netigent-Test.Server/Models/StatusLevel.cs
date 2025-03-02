namespace Netigent_Test.Server.Models
{
    using System.ComponentModel.DataAnnotations;

    public class StatusLevel
    {

        [Key]
        public int Id { get; set; }
        public string StatusName { get; set; }
    }
}
