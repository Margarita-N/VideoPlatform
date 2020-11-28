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
            ViewBag.id = id;
            var currentFile = System.IO.File.ReadAllText("./Data/information.json");
            var resultList = JsonConvert.DeserializeObject<List<ProcessedDataModel>>(currentFile);

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
        public ActionResult Post(ProcessedDataModel response)
        {
            // rujtja json
            //ProcessedDataModel pdm = new ProcessedDataModel(response.Title,response.Description,response.VideoPath.FileName);
            var currentFile = System.IO.File.ReadAllText("./Data/information.json");
            var resultList = JsonConvert.DeserializeObject<List<ProcessedDataModel>>(currentFile);


            resultList.Add(response);
            var convertedJson = JsonConvert.SerializeObject(resultList, Formatting.Indented);
            System.IO.File.WriteAllText("./Data/information.json", convertedJson);

            // rujta e file 
            System.IO.File.Copy(response.VideoPath, "./wwwroot/videos/" + resultList.Count + ".mp4",true);
            InfrastructureMethods.extractThumbnail(resultList.Count);

            return RedirectToAction(nameof(AdminPage), response);
        }
    }
}
