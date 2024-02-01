using Lanesra.Constants;
using Lanesra.Models.Metros;
using Newtonsoft.Json;
using Spectre.Console;

namespace Lanesra.UI;

public class AllStationsUI
{
    public async Task Menu()
    {
        List<string> strings = new List<string>() { "Exit" };

        var str = File.ReadAllText(Constatnts.MetroPath);
        var metros = JsonConvert.DeserializeObject<List<MetroStation>>(str);

        foreach (var metro in metros)
        {

            strings.Add(metro.Name);
        }
        var haveto = true;
        while (haveto)
        {
            var selectedMetro = AnsiConsole.Prompt(
             new SelectionPrompt<string>()
            .Title("Select a metro station:")
            .PageSize(35)
            .MoreChoicesText("[grey](Move up and down to reveal more stations)[/]")
            .AddChoices(strings)
            );
            if (selectedMetro == "Exit") { haveto = false; return; }
            var existing = metros.FirstOrDefault(m => m.Name == selectedMetro);
            Display(existing);
            AnsiConsole.Write("[green]Press[Enter] to continue[/]?");
            Console.ReadLine();
            Console.Clear();
        }
    }
    private void Display(MetroStation metroStation)
    {
        Console.Clear();
        var table = new Table();

        table.AddColumn("Name");
        table.AddColumn(new TableColumn("Line").Centered());
        table.AddColumn(new TableColumn("Year of building").Centered());

        table.AddRow($"[blue]{metroStation.Name}[/]", $"[green]{metroStation.LineName}[/]", $"[yellow1]{metroStation.YearBuilt}[/]");

        AnsiConsole.Write(table);
    }

}
