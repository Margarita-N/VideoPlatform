using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using VideoPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;
using System.Reflection;
using Xabe.FFmpeg;

namespace VideoPlatform.Helpers
{
    public class InfrastructureMethods
    {
        public static List<int> GetVideoIDs()
        {
            var currentFile = System.IO.File.ReadAllText("./Data/information.json");
            var resultList = JsonConvert.DeserializeObject<List<ProcessedDataModel>>(currentFile);

            List<int> videoIDs = new List<int>();

            for(int i = 0; i < resultList.Count; i++)
            {
                videoIDs.Add(Convert.ToInt32(resultList[i].id));
            }

            return videoIDs;
        }

        public static int GetVideoDuration(string id)
        {
            var currentFile = System.IO.File.ReadAllText("./Data/information.json");
            var resultList = JsonConvert.DeserializeObject<List<ProcessedDataModel>>(currentFile);

            for(int i = 0; i < resultList.Count; i++)
            {
                if (resultList[i].id.Equals(id))
                {
                    return resultList[i].Duration;
                }
            }
            return -1;
        }
    }
}
