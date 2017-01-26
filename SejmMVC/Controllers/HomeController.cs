using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SejmMVC.Models;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.Entity.Infrastructure;

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
        public ActionResult Posel(int? pId, int? cId, string action, FormCollection collection, bool? isPost)
        {
            Storage stg = new Storage();
            PosełClubViewModel svm = new PosełClubViewModel();
            svm.data = stg.getData();

            if (isPost != null)
            {
                svm.EditedPoseł.ID = int.Parse(collection["EditedPoseł.ID"]);
                svm.EditedPoseł.Imie = collection["EditedPoseł.Imie"];
                svm.EditedPoseł.Nazwisko = collection["EditedPoseł.Nazwisko"];
                svm.EditedPoseł.Stamp = Convert.FromBase64String(collection["EditedPoseł.Stamp"]);

                svm.EditedKlub.ID = int.Parse(collection["EditedKlub.ID"]);
                svm.EditedKlub.Nazwa = collection["EditedKlub.Nazwa"];
                svm.EditedKlub.Skrót = collection["EditedKlub.Skrót"];
                svm.EditedKlub.Stamp = Convert.FromBase64String(collection["EditedKlub.Stamp"]);
            }
            

            //Zapisz posła
            if (action == "Zapisz posła")
            {
                try
                {
                    svm.EditedPoseł.Klub = svm.data.Kluby.Find(c => c.ID.ToString().Equals(collection["editedPosełKlub"]));
                    stg.updatePoseł(svm.EditedPoseł);
                    svm.PosełErrMsg = "";
                }
                catch (DbUpdateConcurrencyException)
                {
                    svm.PosełErrMsg = "Exception: " + "Konflikt edycji!";
                }
                catch (Exception e)
                {
                    svm.PosełErrMsg = "Exception: " + e.Message;
                }
                
            }

            //Usuń posła
            else if(action == "Usuń posła")
            {
                try
                {
                    stg.deletePoseł(svm.EditedPoseł);
                    svm.PosełErrMsg = "";
                }
                catch (DbUpdateConcurrencyException)
                {
                    svm.PosełErrMsg = "Exception: " + "Konflikt edycji!";
                }
                catch (Exception e)
                {
                    svm.PosełErrMsg = "Exception: " + e.Message;
                }
            }

            //Nowy poseł
            else if(action == "Nowy poseł")
            {
                try
                {
                    svm.EditedPoseł.Klub = svm.data.Kluby.Find(c => c.ID.ToString().Equals(collection["editedPosełKlub"]));
                    stg.createPoseł(svm.EditedPoseł);
                    svm.PosełErrMsg = "";
                }
                catch (DbUpdateConcurrencyException)
                {
                    svm.PosełErrMsg = "Exception: " + "Konflikt edycji!";
                }
                catch (Exception e)
                {
                    svm.PosełErrMsg = "Exception: " + e.Message;
                }
            }

            //Zapisz klub
            else if(action == "Zapisz klub")
            {
                try
                {
                    stg.updateClub(svm.EditedKlub);
                    svm.KlubErrMsg = "";
                }
                catch (DbUpdateConcurrencyException)
                {
                    svm.KlubErrMsg = "Exception: " + "Konflikt edycji!";
                }
                catch (Exception e)
                {
                    svm.KlubErrMsg = "Exception: " + e.Message;
                }
            }

            //Usuń klub
            else if(action == "Usuń klub")
            {
                try
                {
                    stg.deleteClub(svm.EditedKlub);
                    svm.KlubErrMsg = "";
                }
                catch (DbUpdateConcurrencyException)
                {
                    svm.KlubErrMsg = "Exception: " + "Konflikt edycji!";
                }
                catch (Exception e)
                {
                    svm.KlubErrMsg = "Exception: " + e.Message;
                }
            }

            //Nowy klub
            else if(action == "Nowy klub")
            {
                try
                {
                    stg.createClub(svm.EditedKlub);
                    svm.KlubErrMsg = "";
                }
                catch(DbUpdateConcurrencyException)
                {
                    svm.KlubErrMsg = "Exception: " + "Konflikt edycji!";
                }
                catch (Exception e)
                {
                    svm.KlubErrMsg = "Exception: " + e.Message;
                }
            }

            svm = new PosełClubViewModel { KlubErrMsg = svm.KlubErrMsg, PosełErrMsg = svm.PosełErrMsg };
            svm.data = stg.getData();
            svm.editedPosełKlub = "";


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

            if (pId != null && action != "Usuń posła")
            {
                svm.EditedPoseł = (from p in psFiltered
                                   where p.ID == (int)pId
                                   select p).First();
                if(svm.EditedPoseł.Klub != null)
                {
                    svm.editedPosełKlub = (from c in clFiltered
                                           where c.ID == svm.EditedPoseł.Klub.ID
                                           select c).First().ID.ToString();
                }
                
            }

            if (cId != null && action != "Usuń klub")
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
        public ActionResult Vote(int? uId, int? gId, string action, FormCollection collection, bool? isPost)
        {
            Storage stg = new Storage();
            ActVoteViewModel svm = new ActVoteViewModel();
            svm.data = stg.getData();

            if(isPost != null)
            {
                svm.EditedUstawa.ID = int.Parse(collection["EditedUstawa.ID"]);
                svm.EditedUstawa.Nazwa = collection["EditedUstawa.Nazwa"];
                svm.EditedUstawa.Stamp = Convert.FromBase64String(collection["EditedUstawa.Stamp"]);

                svm.EditedGłos.ID = int.Parse(collection["EditedGłos.ID"]);
                svm.EditedGłos.Stamp = Convert.FromBase64String(collection["EditedGłos.Stamp"]);
            }

            //Zapisz ustawę
            if(action == "Zapisz ustawę")
            {
                try
                {
                    svm.EditedUstawa.Klub = svm.data.Kluby.Find(c => c.ID.ToString() == collection["editedUstawaKlub"]);
                    svm.EditedUstawa.Data = DateTime.Parse(collection["EditedUstawa.Data"]);
                    stg.updateAct(svm.EditedUstawa);
                    svm.UstawaErrMsg = "";
                }
                catch (DbUpdateConcurrencyException)
                {
                    svm.UstawaErrMsg = "Exception: " + "Konflikt edycji!";
                }
                catch (Exception e)
                {
                    svm.UstawaErrMsg = "Exception: " + e.Message;
                }
            }

            //Usuń ustawę
            else if(action == "Usuń ustawę")
            {
                try
                {
                    stg.deleteAct(svm.EditedUstawa);
                    svm.UstawaErrMsg = "";
                }
                catch (DbUpdateConcurrencyException)
                {
                    svm.UstawaErrMsg = "Exception: " + "Konflikt edycji!";
                }
                catch (Exception e)
                {
                    svm.UstawaErrMsg = "Exception: " + e.Message;
                }
            }

            //Nowa ustawa
            else if(action == "Nowa ustawa")
            {
                try
                {
                    svm.EditedUstawa.Klub = svm.data.Kluby.Find(c => c.ID.ToString() == collection["editedUstawaKlub"]);
                    svm.EditedUstawa.Data = DateTime.Parse(collection["EditedUstawa.Data"]);
                    stg.createAct(svm.EditedUstawa);
                    svm.UstawaErrMsg = "";
                }
                catch (DbUpdateConcurrencyException)
                {
                    svm.UstawaErrMsg = "Exception: " + "Konflikt edycji!";
                }
                catch (Exception e)
                {
                    svm.UstawaErrMsg = "Exception: " + e.Message;
                }
            }

            //Zapisz głos
            else if(action == "Zapisz głos")
            {
                try
                {
                    svm.EditedGłos.Poseł1 = svm.data.Posłowie.Find(p => p.ID.ToString().Equals(collection["editedVotePoseł"]));
                    svm.EditedGłos.Ustawa1 = svm.data.Ustawy.Find(u => u.ID.ToString().Equals(collection["editedVoteUstawa"]));
                    svm.EditedGłos.Głosował = 
                        collection["editedVoteGłos"].Equals("Wstrzymał się") ? 
                        (bool?)null : 
                        (collection["editedVoteGłos"].Equals("Za") ? true : false);
                    stg.updateVote(svm.EditedGłos);
                    svm.VoteErrMsg = "";
                }
                catch (DbUpdateConcurrencyException)
                {
                    svm.VoteErrMsg = "Exception: " + "Konflikt edycji!";
                }
                catch (Exception e)
                {
                    svm.VoteErrMsg = "Exception: " + e.Message;
                }
            }

            //Usuń głos
            else if(action == "Usuń głos")
            {
                try
                {
                    stg.deleteVote(svm.EditedGłos);
                    svm.VoteErrMsg = "";
                }
                catch (DbUpdateConcurrencyException)
                {
                    svm.VoteErrMsg = "Exception: " + "Konflikt edycji!";
                }
                catch (Exception e)
                {
                    svm.VoteErrMsg = "Exception: " + e.Message;
                }
            }

            //Nowy głos
            else if(action == "Nowy głos")
            {
                try
                {
                    svm.EditedGłos.Poseł1 = svm.data.Posłowie.Find(p => p.ID.ToString().Equals(collection["editedVotePoseł"]));
                    svm.EditedGłos.Ustawa1 = svm.data.Ustawy.Find(u => u.ID.ToString().Equals(collection["editedVoteUstawa"]));
                    svm.EditedGłos.Głosował =
                        collection["editedVoteGłos"].Equals("Wstrzymał się") ?
                        (bool?)null :
                        (collection["editedVoteGłos"].Equals("Za") ? true : false);
                    stg.createVote(svm.EditedGłos);
                    svm.VoteErrMsg = "";
                }
                catch (DbUpdateConcurrencyException)
                {
                    svm.VoteErrMsg = "Exception: " + "Konflikt edycji!";
                }
                catch (Exception e)
                {
                    svm.VoteErrMsg = "Exception: " + e.Message;
                }
            }

            svm = new ActVoteViewModel { UstawaErrMsg = svm.UstawaErrMsg, VoteErrMsg = svm.VoteErrMsg };
            svm.data = stg.getData();
            svm.editedVoteGłos = "";
            svm.editedVotePoseł = "";
            svm.editedUstawaKlub = "";

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

            if (uId != null && action != "Usuń ustawę")
            {
                svm.EditedUstawa = (from u in usFiltered
                                   where u.ID == (int)uId
                                   select u).First();
                svm.editedUstawaKlub = (from c in clFiltered
                                       where c.ID == svm.EditedUstawa.Klub.ID
                                       select c).First().ID.ToString();
            }

            if (gId != null && action != "Usuń głos")
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