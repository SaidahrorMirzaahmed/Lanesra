using Lanesra.Models.Metros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanesra.interfaces
{
    public interface IMetroServices
    {
        ValueTask<List<MetroStationView>> GetByNameAsync(string name);
        ValueTask<List<MetroStationView>> GetAllAsync();
        ValueTask<string> GuideAsync(MetroStation a, MetroStation b);
    }
}
