using System.Collections.Generic;
using System.Linq;
using FirstControllerProject.Models;
using FirstControllerProject.Data;   // Namespace where DbContext exists

namespace FirstControllerProject.Services
{
    public class UserService : IUserMaster
    {
        private readonly AppDbContext _userDbContext;

        public UserService(AppDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }

        public List<UserMaster> ListAll()
        {
            return _userDbContext.UserMaster.ToList();
        }
    }
}
