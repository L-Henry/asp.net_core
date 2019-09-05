using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ContactenLijst.Models
{
    public class ContactAvatarUpdateViewModel
    {
        public IFormFile NewAvatar { get; set; }

        public byte[] Avatar { get; set; }

        public string AvaterImage
        {
            get
            {
                if (Avatar != null)
                {
                    string mimeType = "png";
                    string base64 = Convert.ToBase64String(Avatar);
                    return string.Format("data:{0};base64,{1}", mimeType, base64);
                }
                else
                {
                    return null;
                }
            }
        }

    }
}
