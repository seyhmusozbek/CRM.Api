using Entities.Abstract;
using Microsoft.AspNetCore.Identity;

namespace Entities.Concrete.Tables
{
    public  class User:IdentityUser,IEntity
    {
        public User()
        {
            
        }
        public User(string email):base(email)
        {
            this.Email = email;
        }

        public User(string email, string userName):base(userName)
        {
            this.Email = email;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int DepartmentId { get; set; }
        
    }
}