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

            if(Convert.ToInt32(id) > resultList.Count || Convert.ToInt32(id) <= 0)
            {
                //there is no such video
                return View("NotFound");
            }
            else
            {
                ViewBag.Title = resultList[Convert.ToInt32(id) - 1].Title;
                ViewBag.Description = resultList[Convert.ToInt32(id) - 1].Description;
            }
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Post(VideoDataModel response)
        {
            // rujtja json
            //ProcessedDataModel pdm = new ProcessedDataModel(response.Title,response.Description,response.VideoPath.FileName);
            var currentFile = System.IO.File.ReadAllText("./Data/information.json");
            var resultList = JsonConvert.DeserializeObject<List<ProcessedDataModel>>(currentFile);
            int size = resultList.Count+1;
            

            var filePath = String.Format("./wwwroot/videos/{0}.mp4", size);
            using (var stream = System.IO.File.Create(filePath))
            {
                await response.VideoPath.CopyToAsync(stream);
            }

            ProcessedDataModel pdm = new ProcessedDataModel(response.Title, response.Description, filePath);
            resultList.Add(pdm);
            var convertedJson = JsonConvert.SerializeObject(resultList, Formatting.Indented);
            System.IO.File.WriteAllText("./Data/information.json", convertedJson);

            // rujta e file 
            //System.IO.File.Copy(response.VideoPath, "./wwwroot/videos/" + resultList.Count + ".mp4",true);
            InfrastructureMethods.extractThumbnail(size);
            InfrastructureMethods.extractFrames(size);

            return RedirectToAction(nameof(AdminPage), response);
        }
    }

}