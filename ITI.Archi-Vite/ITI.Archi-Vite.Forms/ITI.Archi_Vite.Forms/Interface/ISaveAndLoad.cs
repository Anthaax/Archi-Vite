using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Archi_Vite.Forms
{
    public interface ISaveAndLoad
    {
        void SaveText(string filename, string text);
        void SaveData(string fileName, DataJson saveData);
        string LoadText(string filename);
        DataJson LoadData(string fileName);

    }
}
