using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web_Cafe.Areas.Admin.Models.DTO;
using Web_Cafe.Models.Entities;

namespace Web_Cafe.Areas.Admin.Models.DAO
{
    public class UserDAO
    {
        Web_CafeModel db;
        public UserDAO()
        {
            db = new Web_CafeModel();
        }
        
        public IEnumerable<UserDTO> listSearchUserByName(string fullName, int pageNum, int pageSize)
        {
            var lst = db.Database.SqlQuery<UserDTO>(string.Format("listSearchUserByName N'{0}'", fullName)
                ).ToPagedList<UserDTO>(pageNum, pageSize);
            return lst;
        }
        public List<UserDTO> listSearchUserByNameInOrder(string fullName)
        {
            var lst = db.Database.SqlQuery<UserDTO>(string.Format("listSearchUserByName N'{0}'", fullName)
                ).ToList<UserDTO>();
            return lst;
        }
        public int UpdatetUser(User tmp)
        {
            User u = db.Users.Find(tmp.UserID);
            if (u != null)
            {
                u.FullName = tmp.FullName;
                u.Username = tmp.Username;
                u.Email = tmp.Email;
                u.Phone = tmp.Phone;
                u.UserAddress = tmp.UserAddress;
                db.SaveChanges();//luu vao o dia
            }
            return u.UserID;
        }
        public int ChangePassword(int id, string password)
        {
            User u = db.Users.Find(id);
            if (u != null)
            {
                u.Password = password;
                db.SaveChanges();//luu vao o dia
            }
            return u.UserID;
        }
        public int InsertUser(User u)
        {
            db.Users.Add(u);//luu tren RAM
            db.SaveChanges();//luu vao o dia
            return u.UserID;
        }
        public User FindUserByID(int id)
        {
            return db.Users.Find(id);
        }
        public int Delete(int id)
        {
            User u = db.Users.Find(id);
            if (u != null)
            {
                db.Users.Remove(u);
                return db.SaveChanges();
            }
            return -1;
        }
        
    }
}