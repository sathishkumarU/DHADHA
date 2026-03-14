using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FirstControllerProject.Models.BaseClass;

namespace FirstControllerProject.Models
{
    public abstract class  UserMasterBase : AuditBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]   
        public int UserMasterAutoId { get; set; }
        public string? UserId {get;set;}
        public string? Name { get; set; }
        public DateTime? DOB { get; set; }
        public DateTime? DateOfJoin{get;set;}
        public string? DHStatus { get; set; }
        public string PhoneNo { get; set; }
        public string? Address { get; set; }
        public string Password {get;set;}
        public int? ReferenceAutoId {get;set;}
        public string Email {get;set;}
    }
    public class UserMaster : UserMasterBase
    {
        
    }
    public class veUserMaster : UserMasterBase
    {
        
    }
}