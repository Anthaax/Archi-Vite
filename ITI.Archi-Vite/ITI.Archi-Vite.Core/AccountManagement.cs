using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Archi_Vite.Core
{
    public class PatientManagement
    {
        public void CreatePatient(string FirstName, string LastName, int userId)
        {
            string folderName = @"C:\ArchiFile";
            string pathString = Path.Combine(folderName, FirstName + LastName + userId);
            bool exists = Directory.Exists(pathString);
            if (!exists) Directory.CreateDirectory(pathString);
            else throw new ArgumentException("This file path is already taken!");
        }
        public void DeletePatient(string FirstName, string LastName, int userId)
        {
            string folderName = @"C:\ArchiFile";
            string pathString = Path.Combine(folderName, FirstName + LastName + userId);
            if (Directory.Exists(pathString)) Directory.Delete(pathString);
        }
    }

    
}
