using brfGuiden.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brfGuiden.WPF.Interface
{
    public interface IMedlemService
    {

        List<Medlem> GetMedlemmar();

         Medlem GetMedlem(string id);
    }
}
                                                     