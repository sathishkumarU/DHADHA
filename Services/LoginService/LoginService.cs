using FirstControllerProject.Data;
using FirstControllerProject.Models;
using FirstControllerProject.Services;

namespace FirstControllerProject
{
    public class LoginService : ILoginService
    {
        private readonly AppDbContext _loginContext;

        public LoginService(AppDbContext appDbContext) 
        {
            _loginContext = appDbContext;
        }

        public LoginCheck ValidUser(string userId, string password)
        {
            var user = _loginContext.veUserMaster
                        .Where(a => a.UserId == userId && a.Password == password)
                        .FirstOrDefault();

            if (user != null)
            {
                return new LoginCheck
                {
                    UserId = user.UserId,
                };
            }

            return new LoginCheck
            {
            };
        }
    }
}