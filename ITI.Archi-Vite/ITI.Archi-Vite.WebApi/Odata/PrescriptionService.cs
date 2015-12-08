﻿using ITI.Archi_Vite.Core;
using ITI.Archi_Vite.DataBase;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ITI.Archi_Vite.WebApi.Controllers
{
    public class PrescriptionService
    {
        private ArchiViteContext _db = new ArchiViteContext();
        DocumentManager _doc;
        
        public DocumentSerializable getPrescription(int patientId, int proId)
        {
            _doc = new DocumentManager(_db);
            DocumentSerializable doc = _doc.SeeDocument(proId, patientId);
            return doc;
        }

        public void putPrescription(int id, Follower follower)
        {
            _db.Entry(follower).State = EntityState.Modified;
        }

        public void postFollower(Follower follower)
        {
            _db.Follower.Add(follower);
        }

        public async System.Threading.Tasks.Task<Follower> deleteFollower(int id)
        {
            Follower follower = await _db.Follower.FindAsync(id);
            _db.Follower.Remove(follower);
            await _db.SaveChangesAsync();

            return follower;
        }


    }
}