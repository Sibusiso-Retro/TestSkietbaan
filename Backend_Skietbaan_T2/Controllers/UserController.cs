
using Backend_Skietbaan_T2.Controllers;
using Backend_Skietbaan_T2.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Backend_Skietbaan_T2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private ModelsContext _context;
        public UserController(ModelsContext db)
        {
            _context = db;
        }

        // GET: api/User
        [HttpGet]
        public  async Task<ActionResult<IEnumerable<User>>> Get()
        {
            List<User> users =  _context.Users.ToList<User>();
            return users;
        }

        // GET: api/User/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<User>> Get(int id)
        {
            User user =  _context.Users.Find(id);
            if(user == null)
            {
                List<User> users = _context.Users.ToList();
                for(int i = 0; i < users.Count; i++)
                {
                    if(users.ElementAt(i).Id == id)
                    {
                        return users.ElementAt(i);
                    }
                }
            }
            return user;
        }

        // POST: api/User
        [HttpPost]
        public async void Post([FromBody] User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        // PUT: api/User/5
        [HttpPut]
        public async void Put([FromBody] User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete]
        public async void Delete(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}
