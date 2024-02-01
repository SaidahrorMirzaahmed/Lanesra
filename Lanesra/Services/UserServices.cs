using Lanesra.Constants;
using Lanesra.Extensions;
using Lanesra.interfaces;
using Lanesra.Models;
using Lanesra.Models.Metros;
using Lanesra.Models.Users;

namespace Lanesra.Services;

public class UserServices : IUserService
{
    private List<User> users = new List<User>();

    public async ValueTask<Ticket> BuyTicket(int id, MetroStation metroStation)
    {
        users = await FileIO.ReaderAsync<User>(Constatnts.UserPath);
        var res1 = users.FirstOrDefault(x => x.Id == id);
        if (res1 == null)
        {
            throw new Exception("User is not found");
        }
        res1.Balance = res1.Balance - 2000;
        var history = new UsedMetroHistory()
        {
            MetroStation = metroStation,
            VisitedAt = DateTime.UtcNow,
        };
        if (res1.UsedMetroHistory is null) res1.UsedMetroHistory = new List<UsedMetroHistory>();
        res1.UsedMetroHistory.Add(history);
        Ticket ticket = new Ticket()
        {
            BoughtTime = DateTime.UtcNow,
            metroStation = metroStation,
            UserViewModel = res1.ToMap(),
            ValidTill = DateTime.UtcNow.AddMinutes(5)
        };
        FileIO.WriterAsync(Constatnts.UserPath, users).Wait();
        return ticket;
    }

    public async ValueTask<UserViewModel> CreateAsync(UserCreation userCreation)
    {
        users = FileIO.ReaderAsync<User>(Constatnts.UserPath).Result;
        var created = userCreation.ToMap();
        if (users is null)users=new List<User>();
         created.Id = users.Count==0 ? 1 : users.Last().Id + 1;
        users.Add(created);
        FileIO.WriterAsync(Constatnts.UserPath, users).Wait();
        return created.ToMap();
    }

    public async ValueTask<bool> DeleteAsync(int id)
    {
        users = await FileIO.ReaderAsync<User>(Constatnts.UserPath);
        var res1 = users.FirstOrDefault(x => x.Id == id);
        if (res1 == null)
        {
            throw new Exception("User is not found");
        }
        var res = users.Remove(res1);
        await FileIO.WriterAsync(Constatnts.UserPath, users);
        return res;
    }

   

    public async ValueTask<bool> DepositAsync(int id, decimal Amount)
    {
        users = await FileIO.ReaderAsync<User>(Constatnts.UserPath);
        var res = users.FirstOrDefault(x => x.Id == id);
        if (res == null)
        {
            throw new Exception("User is not found");
        }
        res.Balance = res.Balance + Amount;
        await FileIO.WriterAsync(Constatnts.UserPath, users);
        return true;
    }

    public async ValueTask<List<UserViewModel>> GetAllUsersAsync()
    {
        users = await FileIO.ReaderAsync<User>(Constatnts.UserPath);
        var res = new List<UserViewModel>();
        foreach (var user in users)
        {
            res.Add(user.ToMap());
        }
        return res;
    }

    public async ValueTask<UserViewModel> GetByIdAsync(int id)
    {
        users = await FileIO.ReaderAsync<User>(Constatnts.UserPath);
        var res = users.FirstOrDefault(u => u.Id == id);
        if (res == null)
        {
            throw new Exception("User is not found");
        }
        return res.ToMap();
    }

    public async ValueTask<UserViewModel> UpdateAsync(int id, UserCreation userCreation)
    {
        users = await FileIO.ReaderAsync<User>(Constatnts.UserPath);
        var res = users.FirstOrDefault(x => x.Id == id);
        if (res == null)
        {
            throw new Exception("User is not found");
        }
        res.FirstName = userCreation.FirstName;
        res.LastName = userCreation.LastName;
        res.Email = userCreation.Email;
        res.Password = userCreation.Password;
        res.Id = id;
        res.UpdatedAt = DateTime.UtcNow;
        await FileIO.WriterAsync(Constatnts.UserPath, users);
        return res.ToMap();
    }
}
