﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SejmMVC.Models
{
    public class SearchViewModel
    {
        public string filterPosełByClub { get; set; }
        public string filterPosełByName { get; set; }
        public string ErrMsg { get; set; }

        public SejmData data { get; set; }

    }
}