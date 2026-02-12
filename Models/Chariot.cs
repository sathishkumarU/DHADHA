using FirstControllerProject.Models.BaseClass;
using FirstControllerProject.Services;
namespace FirstControllerProject.Models
{
    public class Chariot : AuditBase
    {
        public int ChariotAutoId { get; set; }
        public int MemberAutoID { get; set; }
        public int Amount { get; set; }
        public string MemberName { get; set; }
        public DateTime? AmtReceivedDate { get; set; }
    }
}
