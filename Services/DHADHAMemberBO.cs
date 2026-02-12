using FirstControllerProject.Models;
using System;
using Microsoft.EntityFrameworkCore;
using FirstControllerProject.Data;
namespace FirstControllerProject.Services
{
    public class DHADHAMemberBO
    {
        private readonly AppDbContext _context;

        public DHADHAMemberBO(AppDbContext context)
        {
            _context = context;
        }
        public List<DHADHMembersMaster> GetAll()
        {
            List<DHADHMembersMaster> MemberData = new List<DHADHMembersMaster>();
            MemberData = _context.DHADHMembersMaster.ToList();
            return MemberData;
        }
        public DHADHMembersMaster Edit(int id)
        {
            return _context.DHADHMembersMaster.Where(x => x.MembersMasterAutoId == id).FirstOrDefault();
        }
        public string _createUpdate(List<DHADHMembersMaster> Data)
        {
            try
            {
                int lastId = _context.DHADHMembersMaster.Any() 
                            ? _context.DHADHMembersMaster.Max(a => a.MembersMasterAutoId) 
                            : 0;
                foreach (var s in Data)
                {
                    lastId++;
                    s.MembersMasterAutoId = lastId;
                    s.CreatedBy = "Admin";
                    s.LastUpdateBy ="Admin";
                    s.DateOfJoining = DateTime.SpecifyKind(s.DateOfJoining, DateTimeKind.Utc);

                }
                _context.DHADHMembersMaster.AddRange(Data);
                _context.SaveChanges();
                return "SUCCESS";
            }
            catch (Exception ex)
            {
                return "ERROR: " + ex.Message;
            }
        }
        public string Update(DHADHMembersMaster DHADHAupdate)
        {
            DHADHMembersMaster updatedChariot = new DHADHMembersMaster();
            if (updatedChariot != null)
            {
                updatedChariot = _context.DHADHMembersMaster.Where(x => x.MembersMasterAutoId == DHADHAupdate.MembersMasterAutoId).FirstOrDefault();
                updatedChariot.Name = DHADHAupdate.Name;
                updatedChariot.DateOfJoining = DHADHAupdate.DateOfJoining;
                updatedChariot.PhoneNo = DHADHAupdate.PhoneNo;
                updatedChariot.Address = DHADHAupdate.Address;
            }
            _context.DHADHMembersMaster.Update(updatedChariot);
            _context.SaveChanges();
            return "SUCCESS";
        }
        
    } 
}