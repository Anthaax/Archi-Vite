using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Archi_Vite.Core
{
    public class AccountManagement
    {
        public void CreateFileIfNotExist(string FirstName, string LastName, int userId)
        {
            string folderName = @"C:\ArchiFile";
            string pathString = System.IO.Path.Combine(folderName, FirstName+LastName+userId);
            bool exists = System.IO.Directory.Exists(pathString);
            if (!exists) System.IO.Directory.CreateDirectory(pathString);
            else throw new ArgumentException("This file path is already taken!");
        }
    }

    
}
