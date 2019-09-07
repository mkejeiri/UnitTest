using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VideoTutorial.Models;

namespace VideoTutorial.Controllers
{
    public class VideoController : ApiController
    {
        //// GET api/Video/
        public IHttpActionResult Get()
        {
            return  Json(new List<Video>
            {
                new Video() {Title="C# Advanced", Author ="Mohamed Kejeiri", PublishDate = "01/01/20019"},
                new Video() {Title="C# Unit Testing", Author ="Mohamed Kejeiri", PublishDate = "01/01/2020"},
                new Video() {Title="C# Data Modeling", Author ="Mohamed Kejeiri", PublishDate = "01/01/2021"},
                new Video() {Title="Domain Driven Design", Author ="Mohamed Kejeiri", PublishDate = "01/01/2022"}
            });
        }

        //// GET api/values/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/values
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/values/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //public void Delete(int id)
        //{
        //}
    }

   
}
