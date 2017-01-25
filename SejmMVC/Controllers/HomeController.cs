using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SejmMVC.Models;
using System.Text.RegularExpressions;

namespace SejmMVC.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index(int? pId, int? uId, FormCollection collection, bool? isGet)
        {
            SearchViewModel svm = new SearchViewModel();
            svm.data = new Storage().getData();
            if(collection != null)
            {
                svm.filterPosełByClub = collection["filterPosełByClub"];
                svm.filterPosełByName = collection["filterPosełByName"];
                svm.filterUstawaByClub = collection["filterUstawaByClub"];
                svm.filterUstawaByName = collection["filterUstawaByName"];
                svm.filterVoteByName = collection["filterVoteByName"];
            }

            var psFiltered = from p in svm.data.Posłowie
                             orderby p.Nazwisko + p.Imie + p.ID
                             select p;

            var clFiltered = from c in svm.data.Kluby
                             orderby c.Nazwa + c.ID
                             select c;

            var usFiltered = from u in svm.data.Ustawy
                             orderby u.Data.Year + u.Data.Month + u.Data.Day + u.Nazwa + u.ID
                             select u;

            var glFiltered = from g in svm.data.Głosy
                             orderby g.ID
                             select g;

            if(pId == null && uId == null)
            {
                glFiltered = from g in glFiltered
                             where false
                             orderby g.ID
                             select g;
            }

            string filterPosełByClub = collection["filterPosełByClub"];
            string filterPosełByName = collection["filterPosełByName"];
            string filterUstawaByClub = collection["filterUstawaByClub"];
            string filterUstawaByName = collection["filterUstawaByName"];
            string filterVoteByName = collection["filterVoteByName"];

            if(filterPosełByClub != null && filterPosełByClub != "")
            {
                psFiltered = from p in psFiltered
                             where p.Klub.ID.ToString().Equals(filterPosełByClub)
                             orderby p.Nazwisko + p.Imie + p.ID
                             select p;
            }

            if (filterPosełByName != null && filterPosełByName != "")
            {
                psFiltered = from p in psFiltered
                             where 
                                Regex.IsMatch(p.Imie, filterPosełByName, RegexOptions.IgnoreCase) || 
                                Regex.IsMatch(p.Nazwisko, filterPosełByName, RegexOptions.IgnoreCase)
                             orderby p.Nazwisko + p.Imie + p.ID
                             select p;
            }

            if(filterUstawaByClub != null && filterUstawaByClub != "")
            {
                usFiltered = from u in usFiltered
                             where u.Klub.ID.ToString().Equals(filterUstawaByClub)
                             orderby u.Data.Year + u.Data.Month + u.Data.Day + u.Nazwa + u.ID
                             select u;
            }

            if(filterUstawaByName != null && filterUstawaByName != "")
            {
                usFiltered = from u in usFiltered
                             where Regex.IsMatch(u.Nazwa, filterUstawaByName, RegexOptions.IgnoreCase)
                             orderby u.Data.Year + u.Data.Month + u.Data.Day + u.Nazwa + u.ID
                             select u;
            }

            if (pId != null)
            {
                glFiltered = from g in glFiltered
                             where g.Poseł1.ID == (int)pId
                             orderby g.ID
                             select g;
            }

            if(uId != null)
            {
                glFiltered = from g in glFiltered
                             where g.Ustawa1.ID == (int)uId
                             orderby g.ID
                             select g;
            }

            if (filterVoteByName != null && filterVoteByName != "")
            {
                glFiltered = from g in glFiltered
                             where
                                Regex.IsMatch(g.Ustawa1.Nazwa, filterVoteByName, RegexOptions.IgnoreCase) ||
                                Regex.IsMatch(g.Poseł1.Imie, filterVoteByName, RegexOptions.IgnoreCase) ||
                                Regex.IsMatch(g.Poseł1.Nazwisko, filterVoteByName, RegexOptions.IgnoreCase)
                             orderby g.ID
                             select g;
            }

            

            svm.data.Posłowie = psFiltered.ToList();
            svm.data.Kluby = clFiltered.ToList();
            svm.data.Ustawy = usFiltered.ToList();
            svm.data.Głosy = glFiltered.ToList();

            return View(svm);
        }

        [HttpPost]
        public ActionResult Index(int? pId, int? uId, FormCollection collection)
        {
            return Index(pId, uId, collection, true);
        }

        [HttpGet]
        public ActionResult Posel()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Club()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Vote()
        {
            return View();
        }

    }
}