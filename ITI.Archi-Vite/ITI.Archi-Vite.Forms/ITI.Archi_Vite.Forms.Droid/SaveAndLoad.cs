using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.IO;
using Xamarin.Forms;
using ITI.Archi_Vite.Forms.Droid;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using Polenter.Serialization;

[assembly: Dependency(typeof(SaveAndLoad))]
namespace ITI.Archi_Vite.Forms.Droid
{
    public class SaveAndLoad : ISaveLoadAndDelete
    {
        public void SaveText(string filename, string text)
        {

            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, filename);
            System.IO.File.WriteAllText(filePath, text);
        }
        public string LoadText(string filename)
        {
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, filename);
            return System.IO.File.ReadAllText(filePath);
        }
        public DataJson LoadData(string filename)
        {
            DataJson data;
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, filename);
            var serializer = new SharpSerializer();
            Stream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            try
            {
                data = (DataJson)serializer.Deserialize(fileStream);
            }
			catch (Exception e)
            {
				return null;
            }
            finally
            {
                fileStream.Close();
            }
            return data;
        }

        public void SaveData(string filename, DataJson saveData)
        {
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, filename);
            var serializer = new SharpSerializer();
            Stream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                serializer.Serialize(saveData, fileStream);
            }
			catch (Exception e)
            {
                Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fileStream.Close();
            }
        }

        public void DeleteData(string fileName)
        {
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, fileName);
            System.IO.File.Delete(filePath);
        }
    }
}
