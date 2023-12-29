using Law_Firm_Management_System_API.Authentication;
using Law_Firm_Management_System_API.Models;
using System.Security.Claims;

namespace Law_Firm_Management_System_API.Service
{
    public class UserService
    {
        Law_Firm_Management_System_DBContext context;

        public UserService(Law_Firm_Management_System_DBContext context)
        {
            this.context = context;
        }

        public bool CheckPassword(User user, string password)
        {
            return user.Password == AppStatic.Encrypt(password);
        }

        public User GetUser(string username)
        {
            var user = context.Users.Where(a => a.Username == username).FirstOrDefault();
            if (user == null)
                throw new Exception("User not found.");
            return user;
        }

        public User GetUser(int id)
        {
            var user = context.Users.Where(a => a.Id == id).FirstOrDefault();
            if (user == null)
                throw new Exception("User not found.");
            return user;
        }

        public User GetUserNoThrow(int id)
        {
            var user = context.Users.Where(a => a.Id == id).FirstOrDefault();
            return user;
        }

        public User GetUser(ClaimsPrincipal claimsPrincipal)
        {
            //var user = context.Users.FirstOrDefault();
            //if (user == null)
            //    throw new Exception("User not found");
            //return user;

            if (claimsPrincipal.Identity == null)
                throw new Exception("Invalid token");

            var identity = (ClaimsIdentity)claimsPrincipal.Identity;
            var claim = identity.FindFirst(ClaimTypes.Name);
            if (claim != null)
            {
                var user = GetUser(claim.Value);
                if (user != null)
                    return user;
            }
            throw new Exception("Invalid user");
        }

        public User Create(User user, string password)
        {
            user.Password = AppStatic.Encrypt(password);
            context.Users.Add(user);
            context.SaveChanges();
            return user;
        }
    }
}
