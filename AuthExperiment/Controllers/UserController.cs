using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MongoDB.Bson;
using MongoDB.Driver;
using AuthExperiment.Managers;
using System.Threading.Tasks;

namespace AuthExperiment.Controllers
{
    public class UserController : ApiController
    {
        MongoDBManager _userManager;
        public UserController()
        {
            _userManager = new MongoDBManager();
        }


        public async Task<IHttpActionResult> GetUsers() {
            var result = await _userManager.GetUsers();
            return Ok(result);
            
        }

    }
}
