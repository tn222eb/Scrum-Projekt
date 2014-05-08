using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tommy.Model
{
    public class Image
    {
        public int imageid { get; set; }
        public string imagename { get; set; }
        public string userid { get; set; }
        public int imagecategoryid { get; set; }
        public string imagetitle { get; set; }
        public DateTime createddate { get; set; }
    }
}