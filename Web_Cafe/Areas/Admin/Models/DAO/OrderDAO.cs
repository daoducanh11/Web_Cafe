using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web_Cafe.Areas.Admin.Models.DTO;
using Web_Cafe.Models.Entities;

namespace Web_Cafe.Areas.Admin.Models.DAO
{
    public class OrderDAO
    {
        Web_CafeModel db;
        public OrderDAO()
        {
            db = new Web_CafeModel();
        }
        public IEnumerable<Order> lstJoin(int pageNum, int pageSize)
        {
            var lst = db.Database.SqlQuery<Order>("SELECT * FROM [dbo].[Order] ORDER BY OrderID DESC"
                ).ToPagedList<Order>(pageNum, pageSize);
            return lst;
        }
        public IEnumerable<Order> lstOrderById(int id, string startTime, string endTime, int pageNum, int pageSize)
        {
            var lst = db.Database.SqlQuery<Order>(String.Format("lstOrderById {0}, '{1}', '{2}'", id, startTime, endTime)
                ).ToPagedList<Order>(pageNum, pageSize);
            return lst;
        }
        public IEnumerable<OrderDTO> GetListOrder(string proName, string fullName, string status, int pageNum, int pageSize)
        {
            var lst = db.Database.SqlQuery<OrderDTO>(string.Format("GetListOrder N'{0}', N'{1}'", fullName, status)
                ).ToPagedList<OrderDTO>(pageNum, pageSize);
            for (int item=0; item<lst.Count; item++)
            {
                User u = new UserDAO().FindUserByID(Convert.ToInt32(lst[item].UserID));
                lst[item].FullName = u.FullName;
                lst[item].Phone = u.Phone;
                List<ItemDTO> listItem = new ItemDAO().GetListItem(lst[item].OrderID, proName);
                if(listItem.Count > 0)
                {
                    foreach (var i in listItem)
                    {
                        lst[item].listItem.Add(i);
                    }
                }
                else
                {
                    lst = lst.Where(l => l.OrderID != lst[item].OrderID).ToPagedList<OrderDTO>(pageNum, pageSize);
                    item--;
                }
            }
            return lst;
        }
        public int InsertOrder(Order o)
        {
            db.Orders.Add(o);//luu tren RAM
            db.SaveChanges();//luu vao o dia
            return o.OrderID;
        }
        public int UpdatetOrder(OrderDTO o)
        {
            Order orde = db.Orders.Find(o.OrderID);
            if (orde != null)
            {
                orde.Amount = o.Amount;
                orde.Daytime = DateTime.Now;
                orde.DeliveryAddress = o.DeliveryAddress;
                orde.OrderDescription = o.OrderDescription;
                orde.OrderStatus = o.OrderStatus;
                orde.UserID = o.UserID;
                db.SaveChanges();//luu vao o dia
            }
            return o.OrderID;
        }
        public int Delete(int id)
        {
            Order o = db.Orders.Find(id);
            if (o != null)
            {
                db.Orders.Remove(o);
                return db.SaveChanges();
            }
            return -1;
        }
        public Order FindOrderByID(int id)
        {
            return db.Orders.Find(id);
        }
        public int OrderCount()
        {
            return db.Orders.Where(x => x.OrderStatus == "Mới").Count();
        }

        public List<StatisticsDTO> StatisticsByMonth(int year, int month)
        {
            var lst = db.Database.SqlQuery<StatisticsDTO>(String.Format("SELECT * FROM Func_ThongKeDoanhThu({0}, {1})", year, month)
                ).ToList();
            return lst;
        }
        public List<int> GetYearOrder()
        {
            var lst = db.Database.SqlQuery<int>("SELECT DISTINCT YEAR(Daytime) FROM [dbo].[Order] ORDER BY YEAR(Daytime) DESC"
                ).ToList();
            return lst;
        }
        public double GetTotal(int year, int month)
        {
            var t = db.Database.SqlQuery<double>(String.Format("SELECT SUM(DoanhThu) FROM Func_ThongKeDoanhThu({0}, {1})", year, month)
                ).ToList();
            return t[0];
        }
        public List<ProductDTO> lstSearchHotProduct(int year, int month)
        {
            var lst = db.Database.SqlQuery<ProductDTO>(String.Format("lstSearchHotProduct {0}, {1}", year, month)
                ).ToList();
            foreach (var item in lst)
            {
                var res = db.Database.SqlQuery<ImageDTO>(" SELECT TOP 1 ImageLink " +
                " FROM Images " +
                " WHERE ProductID = " + item.ProductID
                ).ToList<ImageDTO>();
                if (res.Count() > 0)
                    item.ImageLink = res[0].ImageLink;
                else
                    item.ImageLink = "noimage.jpg";
            }
            return lst;
        }
    }
}