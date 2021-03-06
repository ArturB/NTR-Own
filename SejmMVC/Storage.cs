﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using System.Text.RegularExpressions;
using System.Data.Entity;

namespace SejmMVC
{
    public class InvalidPosełFirstName : Exception {
        public InvalidPosełFirstName() : base("Niepoprawne imię!") { }
    }

    public class InvalidPosełLastName : Exception {
        public InvalidPosełLastName() : base("Niepoprawne nazwisko!") { }
    }

    public class PosełUpdateError : Exception {
        public PosełUpdateError() : base("Posła nie ma już w bazie!") { }
    }

    public class PosełDeleteError : Exception {
        public PosełDeleteError() : base("Posła nie ma już w bazie!") { }
    }

    
    public class InvalidClubName : Exception {
        public InvalidClubName() : base("Niepoprawna nazwa!") { }
    }

    public class InvalidClubShorthand : Exception {
        public InvalidClubShorthand() : base("Niepoprawny skrót!") { }
    }

    public class ClubNameNonUnique : Exception {
        public ClubNameNonUnique() : base("Klub o takiej nazwie już istnieje!") { }
    }

    public class ClubShorthandNonUnique : Exception {
        public ClubShorthandNonUnique() : base("Klub o takim skrócie już istnieje!") { }
    }

    public class ClubUpdateError : Exception {
        public ClubUpdateError() : base("Klubu nie ma już w bazie!") { }
    }

    public class ClubDeleteError : Exception {
        public ClubDeleteError() : base("Klubu nie ma już w bazie!") { }
    }


    public class InvalidActName : Exception {
        public InvalidActName() : base("Niepoprawna nazwa!") { }
    }

    public class InvalidActDate : Exception {
        public InvalidActDate() : base("Niepoprawna data!") { }
    }

    public class ActNameNonUnique : Exception {
        public ActNameNonUnique() : base("Ustawa o takiej nazwie już istnieje!") { }
    }

    public class NoActClub : Exception {
        public NoActClub() : base("Nie wybrano klubu!") { }
    }

    public class ActUpdateError : Exception {
        public ActUpdateError() : base("Ustawy nie ma w bazie!") { }
    }

    public class ActDeleteError : Exception {
        public ActDeleteError() : base("Ustawy nie ma w bazie!") { }
    }


    public class NoVotePoseł : Exception {
        public NoVotePoseł() : base("Nie wybrano posła!") { }
    }

    public class NoVoteAct : Exception {
        public NoVoteAct() : base("Nie wybrano ustawy!") { }
    }

    public class VoteUpdateError : Exception {
        public VoteUpdateError() : base("Głosu nie ma w bazie!") { }
    }

    public class VoteDeleteError : Exception {
        public VoteDeleteError() : base("Głosu nie ma w bazie!") { }
    }

    public class Storage
    {
        string ValidNameRegex      = @"^[A-ZĄĆĘŁÓŃŚŻŹ'.][A-ZĄĆĘŁÓŃŚŻŹ'. a-ząćęłóńśżź0-9\-]+$";

        bool validString(string s, int size)
        {
            return s != null && s != "" && s.Length <= size && Regex.IsMatch(s, ValidNameRegex);
        }

        private static readonly ILog log = LogManager.GetLogger(typeof(Storage));

        public Storage()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        // download data from database
        public SejmData getData()
        {
            using (var db = new StorageContext())
            {
                SejmData res = new SejmData();
                res.Posłowie = db.Poseł.ToList();
                res.Kluby = db.Klub.ToList();
                res.Ustawy = db.Ustawa.ToList();
                res.Głosy = db.Głos.ToList();
                return res;
            }
        } // getData

        //create new poseł in db
        public void createPoseł(Poseł st)
        {
            using (var db = new StorageContext())
            {
                if (!validString(st.Imie, 32))
                {
                    //log.Error(st.FirstName + " " + st.LastName + " (" + st.IndexNo + ") - Create: Invalid first or last name!");
                    throw new InvalidPosełFirstName();
                }
                else if (!validString(st.Nazwisko, 32))
                {
                    //log.Error(st.FirstName + " " + st.LastName + " (" + st.IndexNo + ") - Create: Invalid born city!");
                    throw new InvalidPosełLastName();
                }
                else
                {
                    if(st.Klub != null)
                    {
                        var kl = db.Klub.Find(st.Klub.ID);
                        st.Klub = kl;
                    }
                    
                    db.Poseł.Add(st);
                    db.SaveChanges();
                }
            }
        } // createPoseł

        //update poseł in db
        public void updatePoseł(Poseł st)
        {
            using (var db = new StorageContext())
            {
                if (!validString(st.Imie, 32))
                {
                    //log.Error(st.FirstName + " " + st.LastName + " (" + st.IndexNo + ") - Create: Invalid first or last name!");
                    throw new InvalidPosełFirstName();
                }
                else if (!validString(st.Nazwisko, 32))
                {
                    //log.Error(st.FirstName + " " + st.LastName + " (" + st.IndexNo + ") - Create: Invalid born city!");
                    throw new InvalidPosełLastName();
                }
                else
                {
                    var original = db.Poseł.Find(st.ID);
                    var kl = (Klub)null;
                    if(st.Klub != null)
                    {
                        kl = db.Klub.Find(st.Klub.ID);
                    }
                    else
                    {
                        original.Klub = null;
                        original.Klub_parlamentarny = null;
                    }
                    

                    if (original != null)
                    {
                        original.Imie = st.Imie;
                        original.Nazwisko = st.Nazwisko;
                        original.Klub = kl;

                        db.Entry(original).State = EntityState.Modified;
                        db.Entry(original).OriginalValues["Stamp"] = st.Stamp;
                        db.SaveChanges();
                    }
                    else
                    {
                        //log.Error(st.FirstName + " " + st.LastName + " (" + st.IndexNo + ") - Update: No such student in db!");
                        throw new PosełUpdateError();
                    }
                }


            }
        } // updatePoseł


        //delete poseł from db
        public void deletePoseł(Poseł st)
        {
            using (var db = new StorageContext())
            {
                var original = db.Poseł.Find(st.ID);
                if (original != null)
                {
                    db.Entry(original).State = EntityState.Modified;
                    db.Entry(original).OriginalValues["Stamp"] = st.Stamp;
                    db.Poseł.Remove(original);
                    db.SaveChanges();
                }
                else
                {
                    //log.Error(st.FirstName + " " + st.LastName + " (" + st.IndexNo + ") - Delete: No such student in db!");
                    throw new PosełDeleteError();
                }
            }

        } // deletePoseł

        //create new club in db
        public void createClub(Klub gr)
        {
            using (var db = new StorageContext())
            {
                if (db.Klub.Where(s => s.Nazwa == gr.Nazwa).Count() > 0)
                {
                    //log.Error(gr.Name + " - Create: Name not unique!");
                    throw new ClubNameNonUnique();
                }
                else if (!validString(gr.Nazwa, 256))
                {
                    //log.Error(gr.Name + " - Create: Name invalid!");
                    throw new InvalidClubName();
                }
                if (db.Klub.Where(s => s.Skrót == gr.Skrót).Count() > 0)
                {
                    //log.Error(gr.Name + " - Create: Name not unique!");
                    throw new ClubShorthandNonUnique();
                }
                else if (!validString(gr.Skrót, 10))
                {
                    //log.Error(gr.Name + " - Create: Name invalid!");
                    throw new InvalidClubShorthand();
                }
                else
                {
                    db.Klub.Add(gr);
                    db.SaveChanges();
                }
            }
        } // createClub

        //update group in db
        public void updateClub(Klub gr)
        {
            using (var db = new StorageContext())
            {
                if (!validString(gr.Nazwa, 256))
                {
                    //log.Error(gr.Name + " - Create: Name invalid!");
                    throw new InvalidClubName();
                }
                else if (!validString(gr.Skrót, 10))
                {
                    //log.Error(gr.Name + " - Create: Name invalid!");
                    throw new InvalidClubShorthand();
                }
                else
                {
                    var original = db.Klub.Find(gr.ID);

                    if (original != null)
                    {
                        original.Nazwa = gr.Nazwa;
                        original.Skrót = gr.Skrót;

                        db.Entry(original).State = EntityState.Modified;
                        db.Entry(original).OriginalValues["Stamp"] = gr.Stamp;
                        db.SaveChanges();
                    }
                    else
                    {
                        //log.Error(gr.Name + " - Update: No such group in db!");
                        throw new ClubUpdateError();
                    }
                }
            }
        } // updateClub

        //delete club from db
        public void deleteClub(Klub gr)
        {
            using (var db = new StorageContext())
            {
                var original = db.Klub.Find(gr.ID);
                if (original != null)
                {
                    db.Entry(original).State = EntityState.Modified;
                    db.Entry(original).OriginalValues["Stamp"] = gr.Stamp;
                    db.Klub.Remove(original);
                    db.SaveChanges();
                }
                else
                {
                    //log.Error(gr.Name + " - Delete: No such group in db!");
                    throw new ClubDeleteError();
                }
            }

        } // deleteClub


        //create new act in db
        public void createAct(Ustawa gr)
        {
            using (var db = new StorageContext())
            {
                if (db.Klub.Where(s => s.Nazwa == gr.Nazwa).Count() > 0)
                {
                    //log.Error(gr.Name + " - Create: Name not unique!");
                    throw new ActNameNonUnique();
                }
                else if (!validString(gr.Nazwa, 256))
                {
                    //log.Error(gr.Name + " - Create: Name invalid!");
                    throw new InvalidActName();
                }
                else if(gr.Klub == null)
                {
                    throw new NoActClub();
                }
                else
                {
                    var kl = db.Klub.Find(gr.Klub.ID);
                    gr.Klub = kl;
                    db.Ustawa.Add(gr);
                    db.SaveChanges();
                }
            }
        } // createAct

        //update act in db
        public void updateAct(Ustawa gr)
        {
            using (var db = new StorageContext())
            {
                if (!validString(gr.Nazwa, 32))
                {
                    //log.Error(gr.Name + " - Create: Name invalid!");
                    throw new InvalidActName();
                }
                else if(gr.Klub == null)
                {
                    throw new NoActClub();
                }
                else
                {
                    var original = db.Ustawa.Find(gr.ID);
                    var kl = db.Klub.Find(gr.Klub.ID);

                    if (original != null)
                    {
                        original.Nazwa = gr.Nazwa;
                        original.Klub = kl;

                        db.Entry(original).State = EntityState.Modified;
                        db.Entry(original).OriginalValues["Stamp"] = gr.Stamp;
                        db.SaveChanges();
                    }
                    else
                    {
                        //log.Error(gr.Name + " - Update: No such group in db!");
                        throw new ActUpdateError();
                    }
                }
            }
        } // updateAct

        //delete club from db
        public void deleteAct(Ustawa gr)
        {
            using (var db = new StorageContext())
            {
                var original = db.Ustawa.Find(gr.ID);
                if (original != null)
                {
                    db.Entry(original).State = EntityState.Modified;
                    db.Entry(original).OriginalValues["Stamp"] = gr.Stamp;
                    db.Ustawa.Remove(original);
                    db.SaveChanges();
                }
                else
                {
                    //log.Error(gr.Name + " - Delete: No such group in db!");
                    throw new ActDeleteError();
                }
            }

        } // deleteAct

        //create new vote in db
        public void createVote(Głos gr)
        {
            using (var db = new StorageContext())
            {
                if (gr.Poseł1 == null)
                {
                    //log.Error(gr.Name + " - Create: Name not unique!");
                    throw new NoVotePoseł();
                }
                else if (gr.Ustawa1 == null)
                {
                    //log.Error(gr.Name + " - Create: Name invalid!");
                    throw new NoVoteAct();
                }
                else
                {
                    var pos = db.Poseł.Find(gr.Poseł1.ID);
                    var ust = db.Ustawa.Find(gr.Ustawa1.ID);
                    gr.Poseł1 = pos;
                    gr.Ustawa1 = ust;
                    db.Głos.Add(gr);
                    db.SaveChanges();
                }
            }
        } // createVote

        //update vote in db
        public void updateVote(Głos gr)
        {
            using (var db = new StorageContext())
            {
                if (gr.Poseł1 == null)
                {
                    //log.Error(gr.Name + " - Create: Name not unique!");
                    throw new NoVotePoseł();
                }
                else if (gr.Ustawa1 == null)
                {
                    //log.Error(gr.Name + " - Create: Name invalid!");
                    throw new NoVoteAct();
                }
                else
                {
                    var original = db.Głos.Find(gr.ID);
                    var pos = db.Poseł.Find(gr.Poseł1.ID);
                    var ust = db.Ustawa.Find(gr.Ustawa1.ID);

                    if (original != null)
                    {
                        original.Głosował = gr.Głosował;
                        original.Poseł1 = pos;
                        original.Ustawa1 = ust;

                        db.Entry(original).State = EntityState.Modified;
                        db.Entry(original).OriginalValues["Stamp"] = gr.Stamp;
                        db.SaveChanges();
                    }
                    else
                    {
                        //log.Error(gr.Name + " - Update: No such group in db!");
                        throw new VoteUpdateError();
                    }
                }
            }
        } // updateAct

        //delete vote from db
        public void deleteVote(Głos gr)
        {
            using (var db = new StorageContext())
            {
                var original = db.Głos.Find(gr.ID);
                if (original != null)
                {
                    db.Entry(original).State = EntityState.Modified;
                    db.Entry(original).OriginalValues["Stamp"] = gr.Stamp;
                    db.Głos.Remove(original);
                    db.SaveChanges();
                }
                else
                {
                    //log.Error(gr.Name + " - Delete: No such group in db!");
                    throw new VoteDeleteError();
                }
            }

        } // deleteAct



    } // Storage



    public class SejmData
    {
        public List<Poseł> Posłowie { get; set; }
        public List<Klub> Kluby { get; set; }
        public List<Ustawa> Ustawy { get; set; }
        public List<Głos> Głosy { get; set; }
    }
}