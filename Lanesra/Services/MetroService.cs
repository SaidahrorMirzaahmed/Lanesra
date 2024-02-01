using Lanesra.Constants;
using Lanesra.Extensions;
using Lanesra.interfaces;
using Lanesra.Models.Metros;

namespace Lanesra.Services;
public class MetroService : IMetroServices
{
    private List<MetroStation> MetroStations;
    public MetroService()
    {
        MetroStations = new List<MetroStation>();
    }
    public async ValueTask<List<MetroStationView>> GetAllAsync()
    {
        MetroStations = await FileIO.ReaderAsync<MetroStation>(Constatnts.MetroPath);
        var res = new List<MetroStationView>();
        foreach (var m in MetroStations)
        {
            res.Add(m.ToMap());
        }
        await FileIO.WriterAsync(Constatnts.MetroPath, MetroStations);
        return res;
    }

    public async ValueTask<List<MetroStationView>> GetByNameAsync(string name)
    {
        MetroStations = await FileIO.ReaderAsync<MetroStation>(Constatnts.MetroPath);
        var res= new List<MetroStationView>();
        var exist = MetroStations.Where(m => m.Name.Contains(name)).ToList();
        if (!exist.Any())
        {
            throw new Exception("No metro stations found");
        }
        foreach (var m in exist)
        {
            res.Add(m.ToMap());
        }
        return res;
    }

    public async ValueTask<string> GuideAsync(MetroStation a, MetroStation b)
    {
        MetroStations = await FileIO.ReaderAsync<MetroStation>(Constatnts.MetroPath);
        var res = new List<MetroStationView>();
        if (a.LineName == b.LineName)
        {
            return $"Your destination is in the same line {Math.Abs(b.OrdinalNumberInItsLine - a.OrdinalNumberInItsLine)} stops";
        }
        else
        {
            var list = ConstantCrossings.crossings;
            foreach (var c in list)
            {
                if (c.metroStation.LineName == a.LineName && c.metroStation2.LineName == b.LineName)
                    return $"Go to  {c.metroStation.Name.ToUpper()}  which is after {Math.Abs(c.metroStation.OrdinalNumberInItsLine - a.OrdinalNumberInItsLine)} stops transfer to  {c.metroStation2.Name.ToUpper()} and {Math.Abs(c.metroStation2.OrdinalNumberInItsLine - b.OrdinalNumberInItsLine)} stops will be left to your Destination";
                else if (c.metroStation.LineName == b.LineName && c.metroStation2.LineName == a.LineName)
                    return $"Go to {c.metroStation2.Name} which is after{Math.Abs(c.metroStation2.OrdinalNumberInItsLine - a.OrdinalNumberInItsLine)} stops transfer to{c.metroStation.Name} and {Math.Abs(c.metroStation.OrdinalNumberInItsLine - b.OrdinalNumberInItsLine)} stops will be left to your Destination";

            }
        }
        return "";
    }
}
