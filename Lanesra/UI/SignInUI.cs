using Lanesra.Constants;
using Lanesra.Extensions;
using Lanesra.Models.Users;
using Spectre.Console;

namespace Lanesra.UI;

public class SignInUI
{
    InsideUserUI insideUserUI = new InsideUserUI();
    public async Task Menu()
    {
        var list = FileIO.ReaderAsync<User>(Constatnts.UserPath).Result;
        var haveto = true;
        while (haveto)
        {
            var first = AnsiConsole.Ask<string>("What's your [green]FirstName[/]?");
            var exist = list.FirstOrDefault(u => u.FirstName == first);
            if (exist == null) { AnsiConsole.Write(new Markup(" [red]User is not found[/]\n")); return; }
            var password = AnsiConsole.Ask<string>("What's your [green]Password[/]?");
            if (exist.Password == password)
            {
                AnsiConsole.Write(new Markup("[green]You successfully signed in [/]\nPress Enter to continue?"));
                Console.ReadLine();
                Console.Clear();

                insideUserUI.Menu(exist.Id).Wait();
            }
            else
            {
                AnsiConsole.Write(new Markup("[red]Password is Incorrect[/]\n" +
                    "" +
                    ""));
                var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
            .Title("Do you want to exit?")
            .PageSize(10)
            .MoreChoicesText("[grey](Move up and down to reveal more fruits)[/]")
            .AddChoices(new[] {
                   "Yes","NO"
            }));
                switch (choice)
                {
                    case "Yes": haveto = false; break;
                    case "NO": break;
                }

            }

        }
    }
}
