using brfGuiden.Models;
using System.Collections.ObjectModel;

namespace brfGuiden.Core.Interface
{
    public interface IStyrelseService
    {

        ObservableCollection<StyrelseMedlem> GetStyrelsen();
        bool AuthenticateUser(string email, string password);

    }
}
