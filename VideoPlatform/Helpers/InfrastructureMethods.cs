using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using VideoPlatform.Models;
using Microsoft.AspNetCore.Mvc;

namespace VideoPlatform.Helpers
{
    public class InfrastructureMethods
    {
        public static void extractThumbnail(int id)
        {
            //frames be extracted from video and put in a seperate folder in the frames folder
            //the folders name will be the same as the videos

            string videoPath = string.Format("C:/Users/HP/source/repos/VideoPlatform/VideoPlatform/wwwroot/videos/{0}.mp4",id.ToString());
            string[] thumbnailFormats = new string[] { "160x90", "240x135", "320x180" };
            for(int i = 0; i < 3; i++)
            {
                string thumbnailPath = string.Format("C:/Users/HP/source/repos/VideoPlatform/VideoPlatform/wwwroot/thumbnails/{0}_{1}.webp", id.ToString(), i.ToString());
                string arguments = string.Format("-ss 4 -i {0} -s {1} -frames:v 1 {2}",videoPath,thumbnailFormats[i],thumbnailPath);
                ProcessStartInfo psi = new ProcessStartInfo("C:/Users/HP/FFMPEG/ffmpeg.exe", arguments);
                Process.Start(psi);
            }
             
        }

    }
}
