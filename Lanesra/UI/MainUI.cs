using Spectre.Console;

namespace Lanesra.UI;

public class MainUI
{
    RegistrationUI registrationUI = new RegistrationUI();
    SignInUI signInUI = new SignInUI();
    public async Task Menu()
    {
        var haveto = true;
        while (haveto)
        {
            var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("[green]Metro of Tashkent[/]?")
            .PageSize(10)
            .MoreChoicesText("[grey](Move up and down to reveal more fruits)[/]")
            .AddChoices(new[] {
            "Register", "Sign In","Exit"
            }));

            switch (choice)
            {
                case "Register": await registrationUI.Register(); break;
                case "Sign In": await signInUI.Menu();  break;
                case "Exit": haveto = false; break;
            }
            AnsiConsole.Write(new Markup("[green]Press {Enter} to continue[/]"));
            Console.ReadLine();
            Console.Clear();
        }


    }
    
}
