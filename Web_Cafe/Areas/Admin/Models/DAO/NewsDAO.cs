using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web_Cafe.Models.Entities;

namespace Web_Cafe.Areas.Admin.Models.DAO
{
    public class NewsDAO
    {
        Web_CafeModel db;
        public NewsDAO()
        {
            db = new Web_CafeModel();
        }
        public IEnumerable<News> lstNews(string title, string startTime, string endTime, int pageNum, int pageSize)
        {
            var lst = db.Database.SqlQuery<News>(string.Format("lstNews N'{0}', '{1}', '{2}'",
                title, startTime, endTime)
                ).ToPagedList(pageNum, pageSize);
            return lst;
        }
        public int InsertNews(News news)
        {
            db.News.Add(news);//luu tren RAM
            db.SaveChanges();//luu vao o dia
            return news.NewsID;
        }
        public int UpdatetNews(News tmp)
        {
            News news = db.News.Find(tmp.NewsID);
            if (news != null)
            {
                news.NewsTitle = tmp.NewsTitle;
                news.NewsDescription = tmp.NewsDescription;
                tmp.ImageLink = tmp.ImageLink;
                db.SaveChanges();//luu vao o dia
            }
            return news.NewsID;
        }
        public int Delete(int id)
        {
            News news = db.News.Find(id);
            if (news != null)
            {
                db.News.Remove(news);
                return db.SaveChanges();
            }
            return -1;
        }
        public News FindNewsByID(int id)
        {
            return db.News.Find(id);
        }
    }
}