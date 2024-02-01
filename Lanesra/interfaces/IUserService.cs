using Lanesra.Models;
using Lanesra.Models.Metros;
using Lanesra.Models.Users;

namespace Lanesra.interfaces
{
    public interface IUserService
    {
        ValueTask<UserViewModel> CreateAsync(UserCreation userCreation);
        ValueTask<UserViewModel> UpdateAsync(int id, UserCreation userCreation);
        ValueTask<bool> DeleteAsync(int id);
        ValueTask<List<UserViewModel>> GetAllUsersAsync();
        ValueTask<UserViewModel> GetByIdAsync(int id);
        ValueTask<bool> DepositAsync(int id, decimal Amount);
        ValueTask<Ticket> BuyTicket(int id, MetroStation metroStation);
    }
}
