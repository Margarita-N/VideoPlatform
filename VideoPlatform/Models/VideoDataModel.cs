using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Newtonsoft.Json;

namespace VideoPlatform.Models
{
    public class VideoDataModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public FormFile VideoPath;

        
        
    }
}
