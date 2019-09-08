using System;
using System.Threading.Tasks;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VideoClient;

namespace VideoTest
{
    [TestClass]
    public class VideoUnitTest
    {
        [TestMethod]
        [DeploymentItem("videos.txt")]
        public async Task test_if_there_is_videos()
        {
            //var realHttpClient = new VideoReader.VideoHttpClient();
            var fakeHttpClient = new VideoClient.Fakes.StubIHttpClient();
            fakeHttpClient.GetStreamAsyncString =
                async (url) => { return System.IO.File.OpenRead("videos.txt"); };

            var reader = new VideoReader(fakeHttpClient);
            var videos = await reader.GetVideos();
            if (videos.Length == 0)
            {
                Assert.Fail("No Videos found");
            }
        }

        [TestMethod]
        [DeploymentItem("videos.txt")]
        public async Task test_if_there_is_upcoming_videos()
        {
            var fakeHttpClient = new VideoClient.Fakes.StubIHttpClient();
            fakeHttpClient.GetStreamAsyncString =
                async (url) => { return System.IO.File.OpenRead("videos.txt"); };

            var reader = new VideoReader(fakeHttpClient);

            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.UtcNowGet = () => DateTime.Parse("01/01/2019");
                var videos = await reader.GetUpcomingVideos();
                if (videos.Length == 0)
                {
                    Assert.Fail("No Videos found");
                }
            }
        }
    }
}
