using FirstControllerProject.Models.BaseClass;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.ComponentModel.DataAnnotations;


namespace FirstControllerProject.Models
{
    public class StudentDetails : AuditBase
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = "";
        [Range (8,50)]
        public int Age { get; set; }
        [EmailAddress]
        public string Email { get; set; } = "";
        
    }
}
