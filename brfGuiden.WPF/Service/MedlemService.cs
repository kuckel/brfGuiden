using brfGuiden.Models;
using brfGuiden.WPF.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brfGuiden.WPF.Service                               
{
    public partial class MedlemService : IMedlemService
    {
        private readonly dataContext _context;

        public MedlemService(dataContext context)
        {
            _context = context;
        }


        public List<Medlem> GetMedlemmar()
        {
           return  _context.Medlemmar.ToList(); 
        }
        public Medlem GetMedlem(string id)
        {
            return _context.Medlemmar.Find(id);    
        }




    }
}
