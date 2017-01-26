using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SejmMVC.Models
{
    public class ActVoteViewModel
    {
        public string filterUstawaByClub { get; set; }
        public string filterUstawaByName { get; set; }
        public string filterVoteByName { get; set; }

        public string UstawaErrMsg { get; set; }
        public string VoteErrMsg { get; set; }

        public Ustawa EditedUstawa { get; set; }
        public Głos EditedGłos { get; set; }

        public string editedUstawaKlub { get; set; }
        public string editedUstawaData { get; set; }

        public string editedVotePoseł { get; set; }
        public string editedVoteUstawa { get; set; }
        public string editedVoteGłos { get; set; }

        public SejmData data { get; set; }

        public List<string> voteOpts;

        public ActVoteViewModel()
        {
            EditedUstawa = new Ustawa();
            EditedUstawa.Nazwa = "";
            EditedUstawa.Data = new DateTime();

            EditedGłos = new Głos();
            EditedGłos.Głosował = null;

            voteOpts = new List<string>();
            voteOpts.Add("Za");
            voteOpts.Add("Przeciw");
            voteOpts.Add("Wstrzymał się");
        }
    }
}