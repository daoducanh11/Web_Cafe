using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web_Cafe.Areas.Admin.Models.DTO;
using Web_Cafe.Models.Entities;

namespace Web_Cafe.Areas.Admin.Models.DAO
{
    public class ItemDAO
    {
        Web_CafeModel db;
        public ItemDAO()
        {
            db = new Web_CafeModel();
        }
        public void InsertItem(Item i)
        {
            db.Items.Add(i);//luu tren RAM
            db.SaveChanges();//luu vao o dia
            return;
        }
        public void UpdatetItem(Item tmp)
        {
            Item i = db.Items.Find(tmp.OrderID, tmp.ProductID);
            if (i != null)
            {
                i.Quantity = tmp.Quantity;
                i.ItemPrice = tmp.ItemPrice;
                db.SaveChanges();//luu vao o dia
            }
            return;
        }
        
        public Item FindItem(int proId, int orderId)
        {
            return db.Items.Find(proId, orderId);
        }
        public int Delete(int proId, int orderId)
        {
            Item i = db.Items.Find(proId, orderId);
            if (i != null)
            {
                db.Items.Remove(i);
                return db.SaveChanges();
            }
            return -1;
        }
        public List<ItemDTO> GetListItem(int oId, string proName)
        {
            var lst = db.Database.SqlQuery<ItemDTO>(string.Format("GetListItem {0}, N'{1}'", oId, proName)
                ).ToList();
            return lst;
        }

        //userCode
        public string GetImageItem(int id)
        {
            var lst = db.Database.SqlQuery<Image>(string.Format("SELECT TOP 1 * FROM Images WHERE ProductID = {0}", id)
                ).ToList();
            return lst[0].ImageLink;
        }
    }

}