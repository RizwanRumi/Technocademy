using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace TechnocademyWebSolution.Models
{
    public class VideoUpload
    {
        public int VideoUploadId { get; set; }
        public string VideoName { get; set; }
        public int CategoryId { get; set; }
        public string Label { get; set; }
        public int Ratings { get; set; }
        public string Lecturer { get; set; }
        public DateTime PublishDate { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
    }
}