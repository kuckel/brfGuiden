using brfGuiden.Models;
using brfGuiden.WPF.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brfGuiden.WPF.Service
{
    public class ForeningService: IForeningService
    {
        //private readonly dataContext _context;
        //public ForeningService(dataContext datacontext)
        //{
        //    _context = datacontext;
        //}

        public ForeningService()
        {
            
        }



        public Forening GetForening()
        {
            return new Forening { ForeningId = "sdfsdfsf" };  //_context.Forening.FirstOrDefault() ?? new Forening { ForeningId= "01e31a52-2d2a-4ba0-bd1a-80f14c7d04a2" } ;   
        }



    }
}
