using brfGuiden.Models;
using brfGuiden.WPF.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityState = Microsoft.EntityFrameworkCore.EntityState;

namespace brfGuiden.WPF.Service
{
    public class ForeningService: IForeningService
    {
        private readonly dataContext _context;


        public ForeningService(dataContext context)
        {
            _context = context;
        }



        public brfGuiden.Models.Forening GetForening()
        {
            return _context.Forening.FirstOrDefault() ?? null;   
        }



        public Forening UpdateForening(Forening forening)
        {
            try
            {

                _context.Entry(forening).State = EntityState.Modified;
                _context.Forening.Update(forening);
                _context.SaveChanges();
                return forening;
            }
            catch (Exception )
            {
                //_log.LogCritical(ex.Message + ex.InnerException ?? "");
                return null;
            }
        }



        public Forening AddForening(Forening forening)
        {
            try
            {
                _context.Forening.Add(forening); 
                _context.Entry(forening).State = EntityState.Added;
                _context.SaveChanges();
                return forening;
            }
            catch (Exception)
            {
                //_log.LogCritical(ex.Message + ex.InnerException ?? "");
                return null;
            }
        }




    }
}
