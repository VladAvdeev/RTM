using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RTM.Common.Models;
using RTM.Server.Helpers;
using RTM.Server.Models;

namespace RTM.Server.Controllers
{
    [ApiController]
    [Route("Api")]
    public class TestController : ControllerBase
    {
        User User { get; set; }
        public TestController()
        {
             
        }
        private readonly List<TestModel> testModels = new List<TestModel>()
            {
                new TestModel(1, "Model1", new DateTime(1998, 02, 20)),
                new TestModel(2, "Model2", new DateTime(2000, 01, 02)),
                new TestModel(3, "Model3", new DateTime(1995, 03, 23)),
                new TestModel(4, "Model4", new DateTime(1990, 04, 12)),
                new TestModel(5, "Model5", new DateTime(2005, 02, 11)),
                new TestModel(6, "Model6", new DateTime(2002, 02, 04))
            };

        [HttpGet("GetTestModels")]
        public List<TestModel> TestModels()
        {
            User = HttpContext.Request.CheckToken();
            return testModels;
        }
        [HttpGet("GetTheYoungest")]
        public TestModel TheYoungest()
        {
            return testModels.Where(tm => tm.BirthDay == testModels.Max(y => y.BirthDay)).FirstOrDefault();
        }
        [HttpGet("GetTheYoungest2")]
        public TestModel TheYoungest2()
        {
            return testModels.Where(tm => tm.Year == testModels.Min(y => y.Year)).FirstOrDefault();
        }
        [HttpGet("GetTestModel/{Id}")]
        public ActionResult<TestModel> TestModel([FromRoute] int Id)
        {
            TestModel model = testModels.Where(tm => tm.Id == Id).FirstOrDefault();
            if (model != null)
                return model;
            else
                return StatusCode(204);
        }
        /// <summary>
        /// Поэксперементировать с куками
        /// </summary>
        /// <returns></returns>
        [HttpGet("Context")]
        public ActionResult HttpConextInst()
        {
            var cookies = HttpContext.Request.Cookies;
            string ip = HttpContext.Connection.RemoteIpAddress.ToString();

            return StatusCode(200);
        }

    }
}
