namespace Netigent_Test.Server.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Application
    {
        [Key]
        public int Id { get; set; }
        public string? AppStatus { get; set; }
        public string? ProjectRef { get; set; }
        public string? ProjectName { get; set; }
        public string? ProjectLocation { get; set; }
        public DateTime? OpenDt { get; set; }
        public DateTime? StartDt { get; set; }
        public DateTime? CompleteDt { get; set; }
        public decimal? ProjectValue { get; set; }
        public int? StatusId { get; set; }
        public string? Notes { get; set; }
        public DateTime? Modified { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
