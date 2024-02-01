using Lanesra.Models.Metros;
using Lanesra.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanesra.Models;

public class Ticket
{
    public UserViewModel UserViewModel { get; set; }
    public MetroStation metroStation { get; set; }
    public DateTime BoughtTime { get; set; }
    public DateTime ValidTill { get; set; }
}
