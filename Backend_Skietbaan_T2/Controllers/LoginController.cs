using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend_Skietbaan_T2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Skietbaan_T2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ModelsContext _context;
        public LoginController(ModelsContext db)
        {
            _context = db;
        }

        // GET: api/Login
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        // POST: api/Login
        [HttpPost]
        public async Task<ActionResult<string>> VerifyUser([FromBody] string username , [FromBody] string password)
        {
            List<User> users = _context.Users.ToList<User>();
            for(int i = 0; i < users.Count; i++)
            {
                if(users.ElementAt(i).Username.Equals(username) && users.ElementAt(i).Password.Equals(password))
                {
                    return "access granted";
                }
            }
            return "incorrect login details";
        }
    }
}
