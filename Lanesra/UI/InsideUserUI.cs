using Lanesra.Constants;
using Lanesra.Extensions;
using Lanesra.Models;
using Lanesra.Models.Metros;
using Lanesra.Models.Users;
using Lanesra.Services;
using Newtonsoft.Json;
using Spectre.Console;
using System.Net;
using System.Net.Mail;

namespace Lanesra.UI;

public class InsideUserUI
{
    UserServices UserServices = new UserServices();
    AllStationsUI AllStationsUI = new AllStationsUI();
    MetroService MetroService = new MetroService();
    public async Task Menu(int userId)
    {
        var haveto = true;
        while (true)
        {
            var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("[yellow1]What do u want to do[/]?")
            .PageSize(10)
            .MoreChoicesText("[grey](Move up and down to reveal more fruits)[/]")
            .AddChoices(new[] {
            "Buy ticket(Reccomended)","Deposit","Profile","Guide(Reccomended)","Get All Metro Stations and Info(Reccomended)","Get Metro by Name","History of Tickets","Update","Delete","Exit"
            }));
            switch (choice)
            {
                case "Buy ticket(Reccomended)": await BuyTicket(userId); break;
                case "Deposit": await Desposit(userId); break;
                case "Update": await Update(userId); break;
                case "Delete": await AskDelete(userId); break;
                case "Guide(Reccomended)": Guide(); break;
                case "Get All Metro Stations and Info(Reccomended)": AllStationsUI.Menu().Wait(); break;
                case "Get Metro by Name": await getbyName(); break;
                case "Profile": var res=await UserServices.GetByIdAsync(userId); DisplayViewUser(res); break;
                case "History of Tickets": await GetHistory(userId); break;
                case "Exit": haveto = false; break;
            }
            AnsiConsole.Write(new Markup("[green]Press {Enter} to continue[/]"));
            Console.ReadLine();
            Console.Clear();
        }
    }

    public async Task Update(int userId)
    {
        Random random = new Random();
        UserCreation userCreation = new UserCreation();
        userCreation.FirstName = AnsiConsole.Ask<string>("What's your [green]FirstName[/]?");
        userCreation.LastName = AnsiConsole.Ask<string>("What's your [green]LastName[/]?");
        userCreation.Password = AnsiConsole.Prompt(
        new TextPrompt<string>("Enter [green]password[/]?")
        .PromptStyle("red")
        .Secret());
        string recipientEmail = "";
        var boool = true;
        while (boool)
        {
            recipientEmail = AnsiConsole.Ask<string>("What's your [green]Email[/]?\nCode will be send to to your e-mail");
            string senderEmail = "saidahror03189@gmail.com";
            string appPassword = "qnovhrqojuxhkadh";

            string subject = "Test Email";
            int body = random.Next(1000, 9999);

            MailMessage mailMessage = new MailMessage(senderEmail, recipientEmail, subject, body.ToString());

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.Port = 587;
            smtpClient.Credentials = new NetworkCredential(senderEmail, appPassword);
            smtpClient.EnableSsl = true;

            try
            {
                smtpClient.Send(mailMessage);
                AnsiConsole.Write(new Markup("[bold yellow]E-mail[/] [blue]Sent successfully![/]"));
                var code = AnsiConsole.Ask<int>("What's your [green]Code[/]?");
                if (code == body) userCreation.Email = recipientEmail;
                else
                    throw new Exception("wrong code");

                boool = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
            }
        }
        var final = await UserServices.UpdateAsync(userId, userCreation);
        DisplayViewUser(final);
    }
    private void DisplayViewUser(UserViewModel userViewModel)
    {
        var table = new Table();


        table.AddColumn("Id");
        table.AddColumn(new TableColumn("FirstName").Centered());
        table.AddColumn(new TableColumn("LastName").Centered());
        table.AddColumn(new TableColumn("Balance").Centered());
        table.AddColumn(new TableColumn("E-mail").Centered());



        table.AddRow(new Markup($"[blue]{userViewModel.Id}[/]"), new Markup($"[lime]{userViewModel.FirstName}[/]"), new Markup($"[lime]{userViewModel.LastName}[/]"), new Markup($"[yellow1]{userViewModel.Balance}[/]"), new Markup($"[white]{userViewModel.Email}[/]"));


        AnsiConsole.Write(table);
    }
    public async Task<bool> AskDelete(int userId)
    {
        var choice = AnsiConsole.Prompt(
                   new SelectionPrompt<string>()
               .Title("Are u sure u want to Delete?")
               .PageSize(10)
               .MoreChoicesText("[grey](Move up and down to reveal more fruits)[/]")
               .AddChoices(new[] {
                   "Yes","NO"
               }));
        switch (choice)
        {
            case "Yes":
                try { await UserServices.DeleteAsync(userId); AnsiConsole.Write("[blue]Success"); break; }
                catch
                {
                    AnsiConsole.Write("[red]This may never be printed[/]?"); break;

                }
            case "NO": break;
        }
        return true;
    }
    public void DisplayListMetro(List<MetroStationView> metroStationViews)
    {
        var table = new Table();

        // Add some columns
        table.AddColumn("Name");
        table.AddColumn(new TableColumn("Line").Centered());

        foreach (var m in metroStationViews)
        {
            table.AddRow(new Markup($"[blue]{m.Name}[/]"), new Markup($"[green]{m.Line}[/]"));
        }
        AnsiConsole.Write(table);
    }
    private async Task getbyName()
    {
        var name = AnsiConsole.Ask<string>("[blue]Name?: [/]");
        var list = MetroService.GetByNameAsync(name).Result;
        DisplayListMetro(list);
    }
    async Task Desposit(int id)
    {
        var amount = AnsiConsole.Ask<decimal>("How much?: ");
        await UserServices.DepositAsync(id, amount);
        AnsiConsole.Write("[green]Success[/]?");
    }
    void Guide()
    {
        List<string> strings = new List<string>();

        var str = File.ReadAllText(Constatnts.MetroPath);
        var metros = JsonConvert.DeserializeObject<List<MetroStation>>(str);

        foreach (var metro in metros)
        {

            strings.Add(metro.Name);
        }

        var selectedMetro = AnsiConsole.Prompt(
         new SelectionPrompt<string>()
        .Title("Select FIRST metro station:")
        .PageSize(35)
        .MoreChoicesText("[grey](Move up and down to reveal more stations)[/]")
        .AddChoices(strings)
        );
        var first = metros.FirstOrDefault(m => m.Name == selectedMetro);
        var selectedMetro2 = AnsiConsole.Prompt(
         new SelectionPrompt<string>()
        .Title("Select SECOND metro station:")
        .PageSize(35)
        .MoreChoicesText("[grey](Move up and down to reveal more stations)[/]")
        .AddChoices(strings)
        );
        var second = metros.FirstOrDefault(m => m.Name == selectedMetro2);
        var rst = MetroService.GuideAsync(first, second).Result;
        AnsiConsole.Write(new Markup($"[darkslategray1] {rst} [/]?"));
    }
    async Task BuyTicket(int userId)
    {
        List<string> strings = new List<string>();

        var str = File.ReadAllText(Constatnts.MetroPath);
        var metros = JsonConvert.DeserializeObject<List<MetroStation>>(str);

        foreach (var metro in metros)
        {

            strings.Add(metro.Name);
        }
        var selectedMetro = AnsiConsole.Prompt(
         new SelectionPrompt<string>()
        .Title("Select FIRST metro station:")
        .PageSize(35)
        .MoreChoicesText("[grey](Move up and down to reveal more stations)[/]")
        .AddChoices(strings)
        );
        var first = metros.FirstOrDefault(m => m.Name == selectedMetro);
        var res = UserServices.BuyTicket(userId, first);

        var image = new CanvasImage(@"..\..\..\..\Lanesra\qrcode.png");

        image.MaxWidth(16);

        // Render the image to the console
        AnsiConsole.Write(image);
        displayTicket(await res);
    }
    void displayTicket(Ticket ticket)
    {

        var table = new Table();

        table.AddColumn("User");
        table.AddColumn(new TableColumn("Metro Station").Centered());
        table.AddColumn(new TableColumn("Bought At").Centered());
        table.AddColumn(new TableColumn("Valid Till").Centered());


        table.AddRow($"[blue]{ticket.UserViewModel.FirstName}[/]", $"[green]{ticket.metroStation}[/]", $"[blue]{ticket.BoughtTime}[/]", $"[blue]{ticket.ValidTill}[/]");

        // Render the table to the console
        AnsiConsole.Write(table);
    }
    public async Task GetHistory(int userId)
    {
        var users= await FileIO.ReaderAsync<User>(Constatnts.UserPath);
        var res = users.FirstOrDefault(u => u.Id == userId);
        var result = res.UsedMetroHistory;
        if (result == null)
        {
            AnsiConsole.Write(new Markup("[blue]No tickets are purchased yet[/]"));
            return;
        }
        DisplayHistory(result);
    }
    private void DisplayHistory(List<UsedMetroHistory> usedMetroHistorys)
    {
        var table = new Table();

        table.AddColumn("Metro station Name");
        table.AddColumn(new TableColumn("MetroStation Line").Centered());
        table.AddColumn(new TableColumn("Visited At").Centered());
        foreach (var u in usedMetroHistorys) 
        {
            table.AddRow($"[yellow]{u.MetroStation.Name}[/]", $"[blue]{u.MetroStation.LineName}[/]", $"[olive]{u.VisitedAt}[/]");
        }
       
        AnsiConsole.Write(table);
    }

}

