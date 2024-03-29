﻿using brfGuiden.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brfGuiden.WPF.Interface
{
    public interface IForeningService
    {
        public brfGuiden.Models.Forening GetForening();
        Forening UpdateForening(Forening forening);

        Forening AddForening(Forening forening);
    }
}
