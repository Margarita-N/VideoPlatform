using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VideoPlatform.Models
{
    public class ProcessedDataModel
    {
        public string id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string VideoPath { get; set; }
        public int Duration { get; set; }

        public ProcessedDataModel(int id,string title,string description,string category,string videoPath,int duration)
        {
            this.id = id.ToString();
            this.Title = title;
            this.Description = description;
            this.Category = category;
            this.VideoPath = videoPath;
            this.Duration = duration;
        }
    }
}
