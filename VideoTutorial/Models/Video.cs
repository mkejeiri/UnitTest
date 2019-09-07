using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideoTutorial.Models
{
    public class Video
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string[] Tags { get; set; }
        public string PublishDate { get; set; }
    }
}