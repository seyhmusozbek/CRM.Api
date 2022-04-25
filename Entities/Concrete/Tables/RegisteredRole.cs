using Entities.Abstract;
using Microsoft.AspNetCore.Identity;

namespace Entities.Concrete.Tables
{
    public class RegisteredRole:IdentityRole,IEntity
    {
        public RegisteredRole():base()
        {
            
        }
        public RegisteredRole(string role):base(role)
        {

        }
    }
}