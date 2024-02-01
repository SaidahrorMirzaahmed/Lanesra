using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lanesra.Models.Metros;

namespace Lanesra.Models.Users
{
    public class UsedMetroHistory
    {
        public MetroStation MetroStation { get; set; }
        public DateTime VisitedAt { get; set; }
    }
}
