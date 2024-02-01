using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanesra.Models.Metros;

public class MetroStation
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int YearBuilt { get; set; }
    public int OrdinalNumberInItsLine {  get; set; }
    public string LineName { get; set; }
    public int UsedCount { get; set; }
}
