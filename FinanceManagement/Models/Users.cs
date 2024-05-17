using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagement.Models
{
    public class Users
    {
        public int? UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }   

        public Users(int? userID, string userName, string password,string email)
        {
            UserID = userID;
            UserName = userName;
            Password = password;
            Email = email;
        }

        public Users()
        {
            
        }

        public override string ToString()
        {
            return $"{UserID},{UserName},{Password},{Email}";
        }
    }
}
