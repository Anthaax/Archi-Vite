using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ITI.Archi_Vite.DataBase;

namespace ITI.Archi_Vite.WebApi.Controllers
{
    public class UsersController : ApiController
    {
        private ArchiViteContext _db = new ArchiViteContext();

        // GET: api/Users
        public IQueryable<User> GetUsers()
        {
            return _db.User;
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> GetUser(int id)
        {
            User user = await _db.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> GetUser(UserConnection user)
        {
            User User = await _db.User.FindAsync(user.Pseudo, user.Password);
            if (User == null)
            {
                return NotFound();
            }

            return Ok(User);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return _db.User.Count(e => e.UserId == id) > 0;
        }
    }
}