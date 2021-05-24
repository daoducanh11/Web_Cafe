using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web_Cafe.Areas.Admin.Models.DAO;
using Web_Cafe.Models.Entities;

namespace Web_Cafe.Areas.Admin.Models.DTO
{
    public class OrderDTO
    {
        public int OrderID { get; set; }
        public double? Amount { get; set; }
        public DateTime? Daytime { get; set; }
        public string DeliveryAddress { get; set; }
        public string OrderDescription { get; set; }
        public string OrderStatus { get; set; }
        public int? UserID { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public List<ItemDTO> listItem = new List<ItemDTO>();

        public OrderDTO()
        {
            
        }
        public void addItem(ItemDTO tmp)
        {
            bool co = false;
            foreach (ItemDTO i in listItem)
                if (i.ProductID == tmp.ProductID)
                {
                    i.Quantity += tmp.Quantity;
                    co = true;
                    break;
                }
            if (!co)
                listItem.Add(tmp);
        }
        public void updateItem(int id, int quantity)
        {
            foreach (ItemDTO i in listItem)
                if (i.ProductID == id)
                {
                    i.Quantity = quantity;
                    return;
                }
        }
        public void deleteItem(int id)
        {
            for(int i=0; i<listItem.Count; i++)
            {
                if (listItem[i].ProductID == id)
                {
                    listItem.Remove(listItem[i]);
                    i--;
                }
            }
        }
        public double getSum(int id, int quantity)
        {
            Product pro = new ProductDAO().FindProductByID(id);
            return Convert.ToDouble(pro.Price * quantity);
        }
        public double getTotalMoney()
        {
            double tong = 0;
            foreach (ItemDTO i in listItem)
                tong += Convert.ToDouble(i.Quantity * i.Price);
            return tong;
        }
        public OrderDTO FindOrderByID(int id)
        {
            Order o = new OrderDAO().FindOrderByID(id);
            OrderDTO oRes = new OrderDTO();
            oRes.OrderID = id;
            oRes.Amount = o.Amount;
            oRes.Daytime = o.Daytime;
            oRes.DeliveryAddress = o.DeliveryAddress;
            oRes.OrderDescription = o.OrderDescription;
            oRes.OrderStatus = o.OrderStatus;
            oRes.UserID = o.UserID;
            User u = new UserDAO().FindUserByID(Convert.ToInt32(oRes.UserID));
            oRes.Phone = u.Phone;
            oRes.FullName = u.FullName;
            oRes.Email = u.Email;
            oRes.listItem = new ItemDAO().GetListItem(oRes.OrderID, "");
            return oRes;
        }
    }
}