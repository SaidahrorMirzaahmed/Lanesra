using Lanesra.Extensions;
using Lanesra.Models.Users;
using Lanesra.Services;
using Spectre.Console;
using System.Net;
using System.Net.Mail;

namespace Lanesra.UI;

public class RegistrationUI
{
        UserServices UserServices=new UserServices();
    public async Task Register()
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
                var code=AnsiConsole.Ask<int>("What's your [green]Code[/]?");
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
        var final = await UserServices.CreateAsync(userCreation);
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

}
