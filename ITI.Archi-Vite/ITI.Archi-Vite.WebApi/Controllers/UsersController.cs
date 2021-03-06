﻿using System;
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
using ITI.Archi_Vite.Core;
using System.Xml.Serialization;
using System.IO;

namespace ITI.Archi_Vite.WebApi.Controllers
{
    public class UsersController : ApiController
    {
        private ArchiViteContext _db = new ArchiViteContext();
        DocumentManager _doc;
        ToXml _tXML = new ToXml();
        UserService Do = new UserService();

        // GET: api/Users
        public IQueryable<User> GetUsers()
        {
            return _db.User;
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> GetUser(int id)
        {
            User swag = Do.getUser(id);
            if (swag == null)
            {
                return NotFound();
            }

            return Ok(swag);
        }

        [ResponseType(typeof(DataXML))]
        public async Task<IHttpActionResult> GetUser(string pseudo, string password)
        {
            Data data = Do.getUser(pseudo, password);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(_tXML.ToXML(data));
        }

        // POST: api/Users
        [ResponseType(typeof(Follower))]
        public async Task<IHttpActionResult> PostUser(UserXML user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            FromXml xml = new FromXml();
            User u = xml.CreateUser(user);
            try
            {
                Do.PostUser(u);
            }
            catch (DbUpdateException)
            {
                if (_db.SelectRequest.SelectUser(user.UserId) == null)
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = user.UserId }, user);
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