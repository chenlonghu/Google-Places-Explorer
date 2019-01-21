using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaceExplorer.Client
{
    public class PlaceDetailResponse
    {
        public string[] HtmlAttributions { get; set; }
        public PlaceDetail Result { get; set; }
        public string Status { get; set; }
    }
}
