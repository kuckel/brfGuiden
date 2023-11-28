using brfGuiden.Core.Interface;

using brfGuiden.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brfGuiden.Core.Service
{
    public class StyrelseService : IStyrelseService
    {

        private readonly dataContext _context;

        public StyrelseService(dataContext datacontext) 
        {
            _context= datacontext;
        }



        public ObservableCollection<StyrelseMedlem> GetStyrelsen()
        {
            ObservableCollection<StyrelseMedlem> tmpColList = new ObservableCollection<StyrelseMedlem>();
            List<StyrelseMedlem> tmpStyrelseMedelmmar=  _context.StyrelseMedlemmar.ToList();
            if (tmpStyrelseMedelmmar != null && tmpStyrelseMedelmmar.Count > 0)
            {
                tmpStyrelseMedelmmar.ForEach(x => tmpColList.Add(x));
            }
            return tmpColList;
        }



        public bool AuthenticateUser(string email, string password)
        {

            StyrelseMedlem? styrelse =  _context.StyrelseMedlemmar.Where(x => x.Epost == email ).FirstOrDefault();
            if(styrelse!=null && styrelse.Epost == email)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public StyrelseMedlem GetCurrentStyrelseMedlem()
        {
            return null;
        }


    }
}
