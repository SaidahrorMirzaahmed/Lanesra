using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanesra.Models.Users;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public decimal Balance { get; set; } = 8000;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public  List<UsedMetroHistory> UsedMetroHistory { get; set; }
}
