using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VideoPlatform.Models
{
    public class ProcessedDataModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string VideoPath { get; set; }

        //public ProcessedDataModel(string title,string description,string videoFilePath)
        //{
        //    this.Title = title;
        //    this.Description = description;
        //    this.VideoFilePath = videoFilePath;
        //}
    }
}
