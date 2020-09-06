using Newtonsoft.Json;
using System.Net.Http;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Image_Gallery_Demo
{
    class DataFetcher
    {
        async Task<string> GetDatafromService(string searchstring)
        {
            string readText = null;
            // using try block to catch any exception such as the web app at azure server is not found
            try
            {
                var azure = @"https://imagefetcher20200529182038.azurewebsites.net";
                string url = azure + @"/api/fetch_images?query=" + searchstring + "&max_count=5";
                using (HttpClient c = new HttpClient())
                {
                    readText = await c.GetStringAsync(url);
                }
            }
            catch
            {
                // assigning the sample images of groot to readText if any exception occurs
                readText = File.ReadAllText(@"Data/sampleData.json");
            }
            return readText;
        }
        public async Task<List<ImageItem>> GetImageData(string search)
        {
            string data = await GetDatafromService(search);

            // deserializing the json file and typecasting to ImageItem class and returning the result
            return JsonConvert.DeserializeObject<List<ImageItem>>(data);
        }
    }
}
