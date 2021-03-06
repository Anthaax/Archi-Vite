﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Archi_Vite.Forms
{
    public interface IBytesSaveAndLoad
    {
        void SaveByteArray(byte[] byteArray, string name);
        byte[] LoadByteArray(string name);
        byte[] GetBytes(string str);
        string GetString(byte[] bytes);
    }
}
