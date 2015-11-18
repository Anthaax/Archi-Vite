using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Archi_Vite.DataBase
{
    public class ContextManager
    {
        readonly ArchiViteContext _context;

        public ContextManager()
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
