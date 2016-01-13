using System.IO;
using System.Drawing;
using Xamarin.Forms;
using ITI.Archi_Vite.Forms.Droid;

[assembly: Dependency(typeof(BytesSaveAndLoad))]

namespace ITI.Archi_Vite.Forms.Droid
{
    public class BytesSaveAndLoad : IBytesSaveAndLoad
    {
        public void SaveByteArray(byte[] byteArray, string name)
        {
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, name);
            if (!File.Exists(filePath))
            {
                using (var stream = new FileStream(filePath, FileMode.CreateNew))
                {
                    stream.Write(byteArray, 0, byteArray.Length);
                }
            }
        }
        public byte[] LoadByteArray( string name )
        {
            byte[] b;
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, name);
            FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            try
            {
                b = File.ReadAllBytes(filePath);
            }
            catch (System.Exception)
            {
                throw;
            }
            return b;
        }
    }
}