using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Archi_Vite.Forms
{
    public interface ISaveLoadAndDelete
    {
        void SaveText(string filename, string text);
        void SaveData(string fileName, DataXML saveData);
        string LoadText(string filename);
        DataXML LoadData(string fileName);
        void DeleteData(string fileName);

    }
}
