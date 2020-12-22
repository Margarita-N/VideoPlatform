using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Newtonsoft.Json;

namespace VideoPlatform.Models
{
    public class VideoDataModel
    {
        [Required]
        [StringLength(50,MinimumLength =3)]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public string Category { get; set; }
        [Required]
        [Display(Name = "File")]
        public IFormFile VideoPath { get; set; }
        
    }
}
