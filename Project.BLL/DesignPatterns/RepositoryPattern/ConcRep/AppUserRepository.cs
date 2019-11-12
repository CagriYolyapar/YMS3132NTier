using Project.BLL.DesignPatterns.RepositoryPattern.BaseRep;
using Project.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.RepositoryPattern.ConcRep
{
    public class AppUserRepository:BaseRepository<AppUser>
    {
        public string CheckCredentials(string userName,string email,out bool varMi)
        {
            if (db.AppUsers.Any(x=>x.UserName==userName&&x.Email==email))
            {
                varMi = true;
                return "Boyle bir kullanici var";

            }
            
            varMi = false;
            return "Kullanici Eklendi";
        }
    }
}
