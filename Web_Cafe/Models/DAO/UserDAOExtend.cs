using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web_Cafe.Models.Entities;

namespace Web_Cafe.Models.DAO
{
    public class UserDAOExtend
    {
        Web_CafeModel db;
        public UserDAOExtend()
        {
            db = new Web_CafeModel();
        }
        public User Login(string username, string password)
        {
            var res = (from s in db.Users where s.Username == username && s.Password == password select s);
            if (res.Count() > 0)
                return res.FirstOrDefault();
            return null;
        }
    }
}