using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Archi_Vite.Forms
{
    public class PrescriptionXML : DocumentsXML
    {
        public string DocPath { get; set; }
        public byte[] Doc { get; set; }
    }
}