using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Archi_Vite.Core
{
    public class ImageManager
    {
        public byte[] ImageCoverter(Image image)
        {
            MemoryStream ms = new MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.ToArray(); 
        }
        public Image BytesArrayConverter( byte[] bytesArrayImage)
        {
            MemoryStream ms = new MemoryStream(bytesArrayImage);
            Image image = Image.FromStream(ms);
            return image;
        }
        public void SaveImage(Image image, string fileName)
        {
            string folderName = @"C:\ArchiFile\Photo";
            if (!Directory.Exists(folderName))
                Directory.CreateDirectory(folderName);
            string pathString = Path.Combine(folderName, fileName);
            if (!File.Exists(pathString))
            {
                image.Save(pathString, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }
        public Image LoadImage(string fileName)
        {
            string folderName = @"C:\ArchiFile\Photo";
            string pathString = Path.Combine(folderName, fileName);
            if (File.Exists(pathString))
            {
                try
                {
                    Image image = Image.FromFile(pathString);
                    return image;
                }
                catch (Exception)
                {
                    return null;
                    throw;
                }
            }
            return null;
        }
    }
}
