using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Models
{
    public class ProjectDetailsViewModel
    {
        public int Id { get; set; }
        public string Titel { get; set; }
        public string Beschrijving { get; set; }
        public byte[] ImageFile { get; set; }
        public string Status { get; set; }
        public List<string> Tags { get; set; }

        public string Image
        {
            get
            {
                string mimeType = "png";
                string base64 = Convert.ToBase64String(ImageFile);
                return string.Format("data:{0};base64,{1}", mimeType, base64);
            }
        }

    }
}
