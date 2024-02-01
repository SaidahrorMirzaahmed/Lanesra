using Lanesra.Models.Metros;
using Lanesra.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lanesra.Extensions
{
    public static class Extension
    {
        public static MetroStationView ToMap(this MetroStation metroStation)
        {
            var view = new MetroStationView();
            view.Name = metroStation.Name;
            view.Line = metroStation.LineName;
            return view;
        }

        public static UserViewModel ToMap(this User user)
        {
            var viewModel = new UserViewModel();
            viewModel.Id = user.Id;
            viewModel.FirstName = user.FirstName;
            viewModel.LastName = user.LastName;
            viewModel.Email = user.Email;
            viewModel.Balance = user.Balance;
            return viewModel;
        }
        public static User ToMap(this UserCreation userCreation)
        {
            var viewModel = new User()
            {
                FirstName = userCreation.FirstName,
                LastName = userCreation.LastName,
                Email = userCreation.Email,
                CreatedAt = DateTime.UtcNow,
                Password = userCreation.Password
            };
            return viewModel;
        }

    }
}
