﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaceExplorer.Client
{
    public class PlaceResponse
    {
        public string[] HtmlAttributions { get; set; }
        public Place[] Results { get; set; }
        public string Status { get; set; }
        public string Next_page_token { get; set; }
    }
}
