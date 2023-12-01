using brfGuiden.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brfGuiden.WPF.Interface
{
    public interface ILeverantorService
    {
        List<Leverantor> GetLeverantorer();
        Leverantor GetLeverantor(string id);
        Leverantor AddLeverantor(Leverantor leverantor);
        ObservableCollection<Leverantor> GetLeverantorerCollection();
    }

}
