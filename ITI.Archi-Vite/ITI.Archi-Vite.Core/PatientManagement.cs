﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITI.Archi_Vite.DataBase;

namespace ITI.Archi_Vite.Core
{
    public class PatientManagement : UserManager
    {
        public void CreatePatient(Patient Patient)
        {
            string folderName = @"C:\ArchiFile";
            string pathString = Path.Combine(folderName, Patient.FirstName + Patient.LastName + Patient.ID);
            bool exists = Directory.Exists(pathString);
            if (!exists) Directory.CreateDirectory(pathString);
            else throw new ArgumentException("This file path is already taken!");
            using (ArchiViteContexts context = new ArchiViteContexts())
            {
                context.User.Add(UserCreator(Patient));
                context.PatientFile.Add(InsertPatient(pathString, Patient));
                foreach(var follow in InsertFollower(InsertPatient(pathString, Patient), Patient))
                {
                    context.Follower.Add(follow);
                }
                
                context.SaveChanges();
            }
        }
        public void DeletePatient(string FirstName, string LastName, int userId)
        {
            string folderName = @"C:\ArchiFile";
            string pathString = Path.Combine(folderName, FirstName + LastName + userId);
            if (Directory.Exists(pathString)) Directory.Delete(pathString);
        }

        private PatientFile InsertPatient(string PathFile, Patient Patient )
        {
            PatientFile p = new PatientFile()
            {
                PathFiles = PathFile,
                Referent = Patient.Referent.ID,
                UserId = Patient.ID,
            };
            return p;
        }
        private List<Follower> InsertFollower(PatientFile File, Patient Patient)
        {
            List<Follower> Followers = new List<Follower>();
            foreach (var follow in Patient.Follow)
            {
                Follower f = new Follower()
                {
                    UserId = follow.ID,
                    PatientFile = File
                };
                Followers.Add(f);
            }
            return Followers;
        }
    }

    
}