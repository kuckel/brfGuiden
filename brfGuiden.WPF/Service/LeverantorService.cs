using brfGuiden.Models;
using brfGuiden.WPF.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brfGuiden.WPF.Service
{
    public class LeverantorService : ILeverantorService
    {
        private readonly dataContext _context;

        public LeverantorService(dataContext context)
        {
            _context = context;
        }


        public List<Leverantor> GetLeverantorer()
        {
            return _context.Leverantorer.ToList(); 
        }

        public ObservableCollection<Leverantor> GetLeverantorerCollection()
        {
            ObservableCollection<Leverantor> tmpColList = new ObservableCollection<Leverantor>();
            List<Leverantor> tmpList=  _context.Leverantorer.ToList();
            if (tmpList != null && tmpList.Count > 0)
            {
                tmpList.ForEach(x => tmpColList.Add(x));
            }
            return tmpColList;

        }

        public Leverantor GetLeverantor(string id)
        {
           return  _context.Leverantorer.Find(id);   
        }

        public Leverantor AddLeverantor(Leverantor leverantor)
        {
            try
            {
                _context.Leverantorer.Add(leverantor);
                _context.Entry(leverantor).State = EntityState.Added;
                _context.SaveChanges();
                return leverantor;
            }
            catch (Exception)
            {
                //_log.LogCritical(ex.Message + ex.InnerException ?? "");
                return null;
            }
        }


    }
}
