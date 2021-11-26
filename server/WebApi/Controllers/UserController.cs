using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private string uid = "0x0e90709f60fdacea987fcbba0927e0da0be870d9";

        // GET: api/<UserController>
        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            //var firestore = FirestoreDb
            var auth = FirebaseAuth.DefaultInstance;
            try
            {
                var user = await auth.GetUserAsync(uid);
                return new string[] { "user found", user.Uid };
            }
            catch (Exception ex)
            {
                var newUser = await auth.CreateUserAsync(new UserRecordArgs
                {
                    Uid = uid,
                });
                return new string[] { "user created", newUser.Uid };
            }

        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
