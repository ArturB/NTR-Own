using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SejmMVC.Models
{
    public class PosełClubViewModel
    {
        public string filterPosełByClub { get; set; }
        public string filterPosełByName { get; set; }
        public string filterClubByName { get; set; }

        public string PosełErrMsg { get; set; }
        public string KlubErrMsg { get; set; }

        public Poseł EditedPoseł { get; set; }
        public Klub EditedKlub { get; set; }

        public string editedPosełKlub { get; set; }

        public SejmData data { get; set; }

        public PosełClubViewModel()
        {
            EditedPoseł = new Poseł();
            EditedPoseł.Imie = "";
            EditedPoseł.Nazwisko = "";

            EditedKlub = new Klub();
            EditedKlub.Nazwa = "";
            EditedKlub.Skrót = "";
        }
    }
}