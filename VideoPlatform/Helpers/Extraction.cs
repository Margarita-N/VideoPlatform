using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xabe.FFmpeg;

namespace VideoPlatform.Helpers
{
    public class Extraction
    {
        public static async void extractThumbnailAsync(int id)
        {
            //thumbnails extracted from video at second 0
            //three format of thumbnails generated using ffmpeg
            string dir = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location).Split("bin")[0];

            string videoPath = string.Format("{0}wwwroot\\videos\\{1}.mp4", dir, id.ToString());
            string[] thumbnailFormats = new string[] { "160x90", "240x135", "320x180" };

            for (int i = 0; i < 3; i++)
            {
                string thumbnailPath = string.Format("{0}wwwroot/thumbnails/{1}_{2}.webp", dir, id.ToString(), i.ToString());
                IConversion conversion = await FFmpeg.Conversions.FromSnippet.Snapshot(videoPath, thumbnailPath, TimeSpan.FromSeconds(0));
                IConversionResult result = await conversion.AddParameter(String.Format("-s {0}", thumbnailFormats[i])).Start();
            }
        }

        public static async void extractFrames(int id)
        {
            //frames be extracted from video and put in a seperate folder in the frames folder
            //the folders name will be the same as the videos
            System.IO.Directory.CreateDirectory("C:/Users/HP/source/repos/VideoPlatform/VideoPlatform/wwwroot/frames/" + id.ToString() + "/");

            string videoPath = string.Format("C:/Users/HP/source/repos/VideoPlatform/VideoPlatform/wwwroot/videos/{0}.mp4", id.ToString());
            string framesPath = string.Format("C:/Users/HP/source/repos/VideoPlatform/VideoPlatform/wwwroot/frames/{0}/{1}_%d.png", id.ToString(), id.ToString());
            string arguments = string.Format("-i {0} -vf fps=1 {1}", videoPath, framesPath);

            await FFmpeg.Conversions.New().Start(arguments);

        }
    }
}
