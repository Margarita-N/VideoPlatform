using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VideoPlatform.Models;
using System.IO;
using System.Web;
using VideoPlatform.Helpers;
using Xabe.FFmpeg;
using System.Reflection;

namespace VideoPlatform.Controllers
{
    public class VideoController : Controller
    {
        public IActionResult AdminPage()
        {
            return View();
        }

        [HttpGet]
        public IActionResult PlayVideo(string id)
        {
            var currentFile = System.IO.File.ReadAllText("./Data/information.json");
            var resultList = JsonConvert.DeserializeObject<List<ProcessedDataModel>>(currentFile);
            
            ViewBag.id = id;
            ViewBag.nrOfVideos = resultList.Count;

            int index = 0;
            bool isPresent=false;
            for(int i = 0; i < resultList.Count; i++)
            {
                if (id.Equals(resultList[i].id))
                {
                    index = i;
                    isPresent = true;
                    break;
                }
            }

            if(!isPresent)
            {
                //there is no such video
                return View("NotFound");
            }
            else
            {
                ViewBag.Title = resultList[index].Title;
                ViewBag.Description = resultList[index].Description;
                ViewBag.Duration = resultList[index].Duration;
            }
            
            return View();
        }

        public IActionResult Videos()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Post(VideoDataModel response)
        {
            // rujtja json
            //ProcessedDataModel pdm = new ProcessedDataModel(response.Title,response.Description,response.VideoPath.FileName);
            var currentFile = System.IO.File.ReadAllText("./Data/information.json");
            var resultList = JsonConvert.DeserializeObject<List<ProcessedDataModel>>(currentFile);

            int id;
            if (resultList.Count != 0)
                id = Convert.ToInt32(resultList[resultList.Count - 1].id) + 1;
            else
            {
                id = 1;
            }

            var filePath = String.Format("./wwwroot/videos/{0}.mp4", id);
            using (var stream = System.IO.File.Create(filePath))
            {
                await response.VideoPath.CopyToAsync(stream);
            }

            string dir = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location).Split("bin")[0];
            string videoPath = string.Format("{0}wwwroot\\videos\\{1}.mp4", dir, id.ToString());

            IMediaInfo mediaInfo = await FFmpeg.GetMediaInfo(videoPath);
            int videoDuration = (int)Math.Floor(mediaInfo.Duration.TotalSeconds);

            ProcessedDataModel pdm = new ProcessedDataModel(id,response.Title, response.Description, response.Category,filePath,videoDuration);
            resultList.Add(pdm);
            var convertedJson = JsonConvert.SerializeObject(resultList, Formatting.Indented);
            System.IO.File.WriteAllText("./Data/information.json", convertedJson);

            // rujta e file 
            //System.IO.File.Copy(response.VideoPath, "./wwwroot/videos/" + resultList.Count + ".mp4",true);
            Extraction.extractThumbnailAsync(id);
            Extraction.extractFrames(id);

            return RedirectToAction(nameof(AdminPage), response);
        }

        [HttpGet]
        public string GetImg(ImageDataModel img)
        {
            return String.Format("~/wwwroot/frames/{0}/{1}_{2}.png", img.Id, img.Id, img.Second);
        }
    }

}