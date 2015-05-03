using System.Linq;
using System.Web.Http;
using LuckyMe.CMS.Entity.DTO;
using LuckyMe.CMS.Service.Services.Interfaces;

namespace LuckyMe.CMS.WebAPI.Controllers
{
      [RoutePrefix("api/Test")]
    public class TestController : ApiController
    {

         private IUserService _userservice;

        public TestController()
        {
        }

        public TestController(IUserService userservice)
        {
            _userservice = userservice;
        }
        
        //public string Get()
        //{
        //    var users = _userservice.GetAllUsers();
        //    return "value"+users.Count();
        //}

        [Route("TestPath")]
        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        public IHttpActionResult TestPost(string str)
        {
            if (!str.Equals(""))
            {
                return Ok();
            }
            return BadRequest();
        }

        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        [Route("AddProviderToUser")]
        public void Post()
        {
            var provider = new UserProviderDTO
            {
                UserId = "16cc730a-6db4-416c-8f8a-b8bad47f34ae",
                LoginProvider = "Testapimethod",
                ProviderKey = "TestapimethodKey"
            };
            _userservice.InsertUserExternalLoginEntry(provider);
        }

    }
}
