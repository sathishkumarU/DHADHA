using System.Collections.Generic;
using System.Linq;
using FirstControllerProject.Models;
using FirstControllerProject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using FirstControllerProject.Services;
using Microsoft.Build.Framework;

namespace FirstControllerProject.Services
{
    public class UserService : ICommonServicescs<UserMaster,veUserMaster>
    {
        private readonly AppDbContext _userDbContext;
        private readonly ILogger<UserService> _logger;
        public UserService(ILogger<UserService> logger,AppDbContext userDbContext)
        {
            _userDbContext = userDbContext;
            _logger = logger;

        }

        public List<veUserMaster> GetAll()
        {
            return _userDbContext.veUserMaster
                        .AsNoTracking()
                        .ToList();
         }
         
        public string _createUpdate(UserMaster Data)
        {
            bool isCreate = Data.UserMasterAutoId <= 0 ;
            try
            {
                    int lastId = _userDbContext.UserMaster.Any() 
                            ? _userDbContext.UserMaster.Max(a => a.UserMasterAutoId) + 1
                            : 0;
                    if(isCreate)
                    {
                        Data.UserMasterAutoId =lastId;
                        Data.DOB = Data.DOB?.ToUniversalTime();
                        Data.DateOfJoin = Data.DateOfJoin?.ToUniversalTime();
                        Data.DHStatus= "A";
                        _userDbContext.UserMaster.Add(Data);
                    }
                    else
                    {
                        Data.DOB = Data.DOB?.ToUniversalTime();
                        Data.DateOfJoin = Data.DateOfJoin?.ToUniversalTime();
                        _userDbContext.UserMaster.Update(Data);
                    }
                    
                    _userDbContext.SaveChanges();
                    _logger.LogInformation("UserMaster Saved Successfully");
                    return "SUCCESS";
                
                
            }
            catch (Exception ex)
            {  
                _logger.LogInformation("Error Saving UserMaster" + ex);
                return "ERROR: " + ex.Message;
            }
        }
        public veUserMaster GetById(int Id)
        {
            var userMaster = _userDbContext.veUserMaster.AsNoTracking().Where(d=> d.UserMasterAutoId == Id).FirstOrDefault();
            return userMaster;
        }
        public bool Delete(int Id)
        {
           var curData = _userDbContext.UserMaster
                    .FirstOrDefault(x => x.UserMasterAutoId == Id);
            if(curData ==null)
                return false;
           curData.DHStatus = "D";
           _userDbContext.SaveChanges();
           return true;
        }
        public IEnumerable<veUserMaster> GetPageSized(int pageSize = 5, int pageCount = 1)
        {
            return _userDbContext.veUserMaster
                .AsNoTracking()
                .OrderBy(x => x.UserMasterAutoId)
                .Skip((pageCount - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }
    }
}
