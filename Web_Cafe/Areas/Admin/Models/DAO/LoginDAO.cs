using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web_Cafe.Models.Entities;

namespace Web_Cafe.Areas.Admin.Models.DAO
{
    public class LoginDAO
    {
        Web_CafeModel db;
        public LoginDAO()
        {
            db = new Web_CafeModel();
        }
        public bool Login(string username, string password)
        {
            var res = (from s in db.Admins where s.Username == username && s.Password == password select s);
            if (res.Count() > 0)
                return true;
            return false;
        }
    }
}