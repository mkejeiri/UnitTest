using System;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace VideoClient
{
    public class Video
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string[] Tags { get; set; }
        public string PublishDate { get; set; }
    }

    public interface IHttpClient
    {
        Task<System.IO.Stream> GetStreamAsync(string url);
    }

    public class VideoReader
    {
        private IHttpClient _httpClient;

        public VideoReader(IHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public class VideoHttpClient : IHttpClient
        {
            public async Task<System.IO.Stream> GetStreamAsync(string url)
            {
                var httpClient = new HttpClient();
                //Used by IIS express
                //var stream = await httpClient.GetStreamAsync("http://localhost:64600/api/video");

                //Used by local IIS
                return await httpClient.GetStreamAsync(url);
            }
        }
        public async Task<Video[]> GetVideos()
        {


            ////download the data
            ////Used by IIS express
            ////var stream = await httpClient.GetStreamAsync("http://localhost:64600/api/video");

            ////Used by local IIS
            //var stream = await _httpClient.GetStreamAsync("http://localhost/api/video");

            var stream = await _httpClient.GetStreamAsync("http://localhost/api/video");



            //deserialise json as it returned
            var serializer = new DataContractJsonSerializer(typeof(Video));

            //convert the deserialized json into the video class array
            var videos = (Video[])serializer.ReadObject(stream);
            return videos;
        }

        public async Task<Video[]> GetUpcomingVideos()
        {
            var now = DateTime.UtcNow;
            var stream = await _httpClient.GetStreamAsync("http://localhost/api/video");

            var serializer = new DataContractJsonSerializer(typeof(Video));
            var videos = (Video[])serializer.ReadObject(stream);
            return videos.Where(v => DateTime.Parse(v.PublishDate) > now).ToArray();
        }
    }
}
