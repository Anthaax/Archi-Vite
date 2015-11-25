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

namespace ITI.Archi_Vite.WebApi.Controllers
{
    public class PrescriptionController : ApiController
    {
        private ArchiViteContext db = new ArchiViteContext();
        DocumentManager _doc;

        // GET: api/Prescription
        public IQueryable<Follower> GetFollower()
        {
            return db.Follower;
        }

        // GET: api/Prescription/?patientId=5&proId=5
        [ResponseType(typeof(DocumentSerializable))]
        public async Task<IHttpActionResult> GetPrescrition(int patientId, int proId)
        {

            DocumentSerializable doc = _doc.SeeDocument(proId, patientId);
            if (doc == null)
            {
                return NotFound();
            }

            return Ok(doc);
        }

        // PUT: api/Prescription/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutFollower(int id, Follower follower)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != follower.PatientId)
            {
                return BadRequest();
            }

            db.Entry(follower).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FollowerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Prescription
        [ResponseType(typeof(Follower))]
        public async Task<IHttpActionResult> PostFollower(Follower follower)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Follower.Add(follower);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FollowerExists(follower.PatientId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = follower.PatientId }, follower);
        }

        // DELETE: api/Prescription/5
        [ResponseType(typeof(Follower))]
        public async Task<IHttpActionResult> DeleteFollower(int id)
        {
            Follower follower = await db.Follower.FindAsync(id);
            if (follower == null)
            {
                return NotFound();
            }

            db.Follower.Remove(follower);
            await db.SaveChangesAsync();

            return Ok(follower);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FollowerExists(int id)
        {
            return db.Follower.Count(e => e.PatientId == id) > 0;
        }
    }
}