using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web_Cafe.Models.Entities;

namespace Web_Cafe.Areas.Admin.Models.DAO
{
    public class AccountDAO
    {
        Web_CafeModel db;
        public AccountDAO()
        {
            db = new Web_CafeModel();
        }

        public IEnumerable<Web_Cafe.Models.Entities.Admin> listSearchAccount(string username, string adminType, int pageNum, int pageSize)
        {
            var lst = (from s in db.Admins 
                       where (username == "" || s.Username.Contains(username))
                                && (adminType == "" || s.AdminType == adminType) 
                       orderby s.AdminID descending select s)
                .ToPagedList(pageNum, pageSize);
            return lst;
        }
        
        public int UpdatetAccount(int id, string password, string adminType)
        {
            Web_Cafe.Models.Entities.Admin u = db.Admins.Find(id);
            if (u != null)
            {
                u.AdminType = adminType;
                if(password != "")
                    u.Password = password;
                db.SaveChanges();//luu vao o dia
            }
            return u.AdminID;
        }

        public bool UpdatetAccountProfile(int id, string username, string passwordNew)
        {
            Web_Cafe.Models.Entities.Admin u = db.Admins.Find(id);
            if (u != null)
            {
                u.Username = username;
                if (passwordNew != "")
                    u.Password = passwordNew;
                db.SaveChanges();//luu vao o dia
                return true;
            }
            return false;
        }

        public bool CheckPass(int id, string password)
        {
            Web_Cafe.Models.Entities.Admin u = db.Admins.Find(id);
            if (u != null && u.Password == password)
                return true;
            return false;
        }

        public int InsertAccount(Web_Cafe.Models.Entities.Admin u)
        {
            db.Admins.Add(u);//luu tren RAM
            db.SaveChanges();//luu vao o dia
            return u.AdminID;
        }
        public Web_Cafe.Models.Entities.Admin FindAccountByID(int id)
        {
            return db.Admins.Find(id);
        }
        public int Delete(int id)
        {
            Web_Cafe.Models.Entities.Admin u = db.Admins.Find(id);
            if (u != null)
            {
                db.Admins.Remove(u);
                return db.SaveChanges();
            }
            return -1;
        }
    }
}