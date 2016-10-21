using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BrandCenter.DAL;
using BrandCenter.Models;
using PagedList;
using System.Data.Entity.Infrastructure;

namespace BrandCenter.Controllers
{
    public class AdminController : Controller
    {
        private BrandCenterContext db = new BrandCenterContext();
        private GroupTest dbtest = new GroupTest();

        // GET: Admin
        public ViewResult Group(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var students = from s in db.Groups
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.Name.Contains(searchString)
                                       || s.Descript.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "id_desc":
                    students = students.OrderByDescending(s => s.Id);
                    break;
                case "Name":
                    students = students.OrderBy(s => s.Name);
                    break;
                case "Name_desc":
                    students = students.OrderByDescending(s => s.Name);
                    break;
                default:  // id ascending 
                    students = students.OrderBy(s => s.Id);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            //jojo paging 샘플
            ViewBag.Page = pageNumber;

            return View(students.ToPagedList(pageNumber, pageSize));
        }

        public ViewResult GroupTest(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var students = from s in dbtest.tblGroup
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.Name.Contains(searchString)
                                       || s.Descript.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "id_desc":
                    students = students.OrderByDescending(s => s.Id);
                    break;
                case "Name":
                    students = students.OrderBy(s => s.Name);
                    break;
                case "Name_desc":
                    students = students.OrderByDescending(s => s.Name);
                    break;
                default:  // id ascending 
                    students = students.OrderBy(s => s.Id);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            //jojo paging 샘플
            ViewBag.Page = pageNumber;

            return View(students.ToPagedList(pageNumber, pageSize));
        }
    }
}