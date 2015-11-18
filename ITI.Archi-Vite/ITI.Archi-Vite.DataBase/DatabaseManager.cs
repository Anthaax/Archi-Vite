using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Archi_Vite.DataBase
{
    public class DatabaseManager
    {
        readonly ArchiViteContext _context;

        public DatabaseManager()
        {
            _context = new ArchiViteContext();
        }
        public ArchiViteContext Context
        {
            get
            {
                return _context;
            }
        }
    }
}
