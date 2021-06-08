using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Cafe.Areas.Admin.Models.DAO;
using Web_Cafe.Areas.Admin.Models.DTO;

namespace Web_Cafe.Areas.Admin.Controllers
{
    public class StatisticsController : Controller
    {
        // GET: Admin/Statistics
        public ActionResult Index(int? year, int? month)
        {
            if (year == null)
                year = DateTime.Now.Year;
            if (month == null)
                month = DateTime.Now.Month;
            OrderDAO dao = new OrderDAO();
            List<StatisticsDTO> lst = dao.StatisticsByMonth(Convert.ToInt32(year), Convert.ToInt32(month));
            List<int> ngay = new List<int>();
            List<double> doanhThu = new List<double>();
            foreach (var item in lst)
            {
                ngay.Add(item.Ngay);
                doanhThu.Add(item.DoanhThu);
            }
            ViewBag.days = ngay;
            ViewBag.revenues = doanhThu;
            ViewBag.year = dao.GetYearOrder();
            ViewBag.yearSelected = year;
            ViewBag.month = month;
            ViewBag.total = dao.GetTotal(Convert.ToInt32(year), Convert.ToInt32(month));
            return View();
        }
    }
}