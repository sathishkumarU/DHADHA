using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FirstControllerProject.Models.BaseClass;

namespace FirstControllerProject.Models
{
    public class DHADHMembersMaster : AuditBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]   
        public int MembersMasterAutoId { get; set; }
        public string Name { get; set; }
        public DateTime DateOfJoining { get; set; }
        public string? Status { get; set; }
        public string PhoneNo { get; set; }
        public string? Address { get; set; }
    }
}