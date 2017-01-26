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
                                Regex.IsMatch(g.Poseł1.Nazwisko, filterVoteByName, RegexOptions.IgnoreCase) ||
                                Regex.IsMatch(g.Poseł1.Klub.Nazwa, filterVoteByName, RegexOptions.IgnoreCase) ||
                                Regex.IsMatch(g.Poseł1.Klub.Skrót, filterVoteByName, RegexOptions.IgnoreCase)
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
        public ActionResult Posel(int? pId, int? cId, string action, FormCollection collection, bool? isGet)
        {
            Storage stg = new Storage();
            PosełClubViewModel svm = new PosełClubViewModel();
            svm.data = stg.getData();

            //Zapisz posła
            if (action == "Zapisz posła")
            {
                svm.EditedPoseł.Klub = svm.data.Kluby.Find(c => c.ID.ToString().Equals(collection["editedPosełKlub"]));
                try
                {
                    stg.updatePoseł(svm.EditedPoseł);
                }
                catch(Exception e)
                {
                    svm.PosełErrMsg = e.Message;
                }
                
            }

            //Usuń posła
            else if(action == "Usuń posła")
            {
                svm.PosełErrMsg = action;
            }

            //Nowy poseł
            else if(action == "Nowy poseł")
            {
                svm.PosełErrMsg = action;
            }

            //Zapisz klub
            else if(action == "Zapisz klub")
            {
                svm.KlubErrMsg = action;
            }

            //Usuń klub
            else if(action == "Usuń klub")
            {
                svm.KlubErrMsg = action;
            }

            //Nowy klub
            else if(action == "Nowy klub")
            {
                svm.KlubErrMsg = action;
            }


            if (collection != null)
            {
                svm.filterPosełByClub = collection["filterPosełByClub"];
                svm.filterPosełByName = collection["filterPosełByName"];
                svm.filterClubByName = collection["filterClubByName"];
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

            string filterPosełByClub = collection["filterPosełByClub"];
            string filterPosełByName = collection["filterPosełByName"];
            string filterClubByName = collection["filterClubByName"];

            if (filterPosełByClub != null && filterPosełByClub != "")
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

            if (filterClubByName != null && filterClubByName != "")
            {
                clFiltered = from c in clFiltered
                             where 
                                Regex.IsMatch(c.Nazwa, filterClubByName, RegexOptions.IgnoreCase) ||
                                Regex.IsMatch(c.Skrót, filterClubByName, RegexOptions.IgnoreCase)
                             orderby c.Nazwa + c.Skrót + c.ID
                             select c;
            }

            if (pId != null)
            {
                svm.EditedPoseł = (from p in psFiltered
                                   where p.ID == (int)pId
                                   select p).First();
                svm.editedPosełKlub = (from c in clFiltered
                                       where c.ID == svm.EditedPoseł.Klub.ID
                                       select c).First().ID.ToString();
            }

            if (cId != null)
            {
                svm.EditedKlub = (from c in clFiltered
                                   where c.ID == (int)cId
                                   select c).First();
            }

            svm.data.Posłowie = psFiltered.ToList();
            svm.data.Kluby = clFiltered.ToList();
            svm.data.Ustawy = usFiltered.ToList();
            svm.data.Głosy = glFiltered.ToList();

            return View(svm);
        }

        [HttpPost] 
        public ActionResult Posel(int? pId, int? uId, string action, FormCollection collection)
        {
            return Posel(pId, uId, action, collection, true);
        }

        [HttpGet]
        public ActionResult Vote(int? uId, int? gId, string action, FormCollection collection, bool? isGet)
        {
            ActVoteViewModel svm = new ActVoteViewModel();
            svm.data = new Storage().getData();

            //Zapisz ustawę
            if(action == "Zapisz ustawę")
            {
                svm.UstawaErrMsg = action; 
            }

            //Usuń ustawę
            else if(action == "Usuń ustawę")
            {
                svm.UstawaErrMsg = action;
            }

            //Nowa ustawa
            else if(action == "Nowa ustawa")
            {
                svm.UstawaErrMsg = action;
            }

            //Zapisz głos
            else if(action == "Zapisz głos")
            {
                svm.VoteErrMsg = action;
            }

            //Usuń głos
            else if(action == "Usuń głos")
            {
                svm.VoteErrMsg = action;
            }

            //Nowy głos
            else if(action == "Nowy głos")
            {
                svm.VoteErrMsg = action;
            }

            if (collection != null)
            {
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

            string filterUstawaByClub = collection["filterUstawaByClub"];
            string filterUstawaByName = collection["filterUstawaByName"];
            string filterVoteByName = collection["filterVoteByName"];

            if (filterUstawaByClub != null && filterUstawaByClub != "")
            {
                usFiltered = from u in usFiltered
                             where u.Klub.ID.ToString().Equals(filterUstawaByClub)
                             orderby u.Data.Year + u.Data.Month + u.Data.Day + u.Nazwa + u.ID
                             select u;
            }

            if (filterUstawaByName != null && filterUstawaByName != "")
            {
                usFiltered = from u in usFiltered
                             where
                                Regex.IsMatch(u.Nazwa, filterUstawaByName, RegexOptions.IgnoreCase) ||
                                Regex.IsMatch(u.Klub.Nazwa, filterUstawaByName, RegexOptions.IgnoreCase)
                             orderby u.Data.Year + u.Data.Month + u.Data.Day + u.Nazwa + u.ID
                             select u;
            }

            if (filterVoteByName != null && filterVoteByName != "")
            {
                glFiltered = from g in glFiltered
                             where 
                                Regex.IsMatch(g.Ustawa1.Nazwa, filterVoteByName, RegexOptions.IgnoreCase) ||
                                Regex.IsMatch(g.Poseł1.Imie, filterVoteByName, RegexOptions.IgnoreCase) ||
                                Regex.IsMatch(g.Poseł1.Nazwisko, filterVoteByName, RegexOptions.IgnoreCase) ||
                                Regex.IsMatch(g.Poseł1.Klub.Nazwa, filterVoteByName, RegexOptions.IgnoreCase) ||
                                Regex.IsMatch(g.Poseł1.Klub.Skrót, filterVoteByName, RegexOptions.IgnoreCase)
                             orderby g.Ustawa1.Nazwa + g.Poseł1.Nazwisko + g.Poseł1.Imie + g.ID
                             select g;
            }

            if (uId != null)
            {
                svm.EditedUstawa = (from u in usFiltered
                                   where u.ID == (int)uId
                                   select u).First();
                svm.editedUstawaKlub = (from c in clFiltered
                                       where c.ID == svm.EditedUstawa.Klub.ID
                                       select c).First().ID.ToString();
            }

            if (gId != null)
            {
                svm.EditedGłos = (from g in glFiltered
                                   where g.ID == (int)gId
                                   select g).First();
                svm.editedVotePoseł = (from p in psFiltered
                                       where p.ID == svm.EditedGłos.Poseł1.ID
                                       select p).First().ID.ToString();

                svm.editedVoteUstawa = (from u in usFiltered
                                       where u.ID == svm.EditedGłos.Ustawa1.ID
                                       select u).First().ID.ToString();

                svm.editedVoteGłos = (from g in glFiltered
                                           where g.ID == (int)gId
                                           select g).First().Głosował == null ? "Wstrzymał się" : ((from g in glFiltered
                                                                                                    where g.ID == (int)gId
                                                                                                    select g).First().Głosował == true ? "Za" : "Przeciw");
            }


            svm.data.Posłowie = psFiltered.ToList();
            svm.data.Kluby = clFiltered.ToList();
            svm.data.Ustawy = usFiltered.ToList();
            svm.data.Głosy = glFiltered.ToList();

            return View(svm);
        }

        [HttpPost]
        public ActionResult Vote(int? pId, int? uId, string action, FormCollection collection)
        {
            return Vote(pId, uId, action, collection, true);
        }

    }
}