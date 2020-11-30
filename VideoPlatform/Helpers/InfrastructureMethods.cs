using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using VideoPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;

namespace VideoPlatform.Helpers
{
    public class InfrastructureMethods
    {
        public static void extractThumbnail(int id)
        {
            //thumbnails extracted from video at 4th second
            //three format of thumbnails generated using ffmpeg

            string videoPath = string.Format("C:/Users/HP/source/repos/VideoPlatform/VideoPlatform/wwwroot/videos/{0}.mp4", id.ToString());
            string[] thumbnailFormats = new string[] { "160x90", "240x135", "320x180" };
            for(int i = 0; i < 3; i++)
            {
                string thumbnailPath = string.Format("C:/Users/HP/source/repos/VideoPlatform/VideoPlatform/wwwroot/thumbnails/{0}_{1}.webp", id.ToString(), i.ToString());
                string arguments = string.Format("-ss 4 -i {0} -s {1} -frames:v 1 {2}", videoPath,thumbnailFormats[i],thumbnailPath);
                ProcessStartInfo psi = new ProcessStartInfo("C:/Users/HP/source/repos/VideoPlatform/VideoPlatform/Helpers/ffmpeg.exe", arguments);
                Process.Start(psi);
            }
             
        }

        public static void extractFrames(int id)
        {
            //frames be extracted from video and put in a seperate folder in the frames folder
            //the folders name will be the same as the videos
            System.IO.Directory.CreateDirectory("C:/Users/HP/source/repos/VideoPlatform/VideoPlatform/wwwroot/frames/" + id.ToString()+"/");

            string videoPath = string.Format("C:/Users/HP/source/repos/VideoPlatform/VideoPlatform/wwwroot/videos/{0}.mp4", id.ToString());
            string framesPath = string.Format("C:/Users/HP/source/repos/VideoPlatform/VideoPlatform/wwwroot/frames/{0}/{1}_%d.png",id.ToString(), id.ToString());
            string arguments = string.Format("-i {0} -vf fps=1 {1}", videoPath, framesPath);

            ProcessStartInfo psi = new ProcessStartInfo("C:/Users/HP/FFMPEG/ffmpeg.exe", arguments);
            Process.Start(psi);

        }

        private static TimeSpan GetVideoDuration(string filePath) 
        { 
            using (var shell = ShellObject.FromParsingName(filePath)) 
            { 
                IShellProperty prop = shell.Properties.System.Media.Duration; 
                var t = (ulong)prop.ValueAsObject; 
                return TimeSpan.FromTicks((long)t); 
            } 
        }

    }
}
