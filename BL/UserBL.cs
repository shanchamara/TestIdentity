using Microsoft.AspNetCore.Identity;

namespace TestIdentity.BL
{
    public class UserBl
    {


        public IdentityUser GetUserByEmail(string email)
        {
            using (var con = new DTO.AppDBContext())
            {
                return con.Users.SingleOrDefault(d => d.Email.ToLower() == email.ToLower());
            };
        }


    }
}