using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tommy.Model
{
    public class Video
    {
        public int videoid { get; set; }
        public string videoname { get; set; }
        public string userid { get; set; }
        public int videocategoryid { get; set; }
        public string videotitle { get; set; }
        public DateTime createddate { get; set; }
      
    }
}