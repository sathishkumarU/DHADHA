using System.ComponentModel.DataAnnotations;
namespace FirstControllerProject.Models.BaseClass
{
    public class AuditBase
    {
        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        [Required]
        public string CreatedBy { get; set; }
        public DateTime LastUpdateDate { get; set; } = DateTime.UtcNow;
        public string LastUpdateBy { get; set; }
    }
}
