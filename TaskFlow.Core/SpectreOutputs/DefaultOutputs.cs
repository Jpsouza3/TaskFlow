using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;
using TaskFlow.Business.Model;

namespace TaskFlow.Core.SpectreOutputs
{
    public static class DefaultOutputs
    {

        public static void PrintTask(TaskModel t)
        {

            AnsiConsole.Markup(
                "[bold #8787ff]› [/][bold #d7d7ff]" + t.Id + "[/]\n" +
                "[#8787af]│ [/][italic #afafff]" + t.Name + "[/][grey42](" + t.CreationDate + ")[/]"
            );
        }

        public static void PrintTaskAdded(TaskModel t)
        {
            AnsiConsole.Markup(
                $"[bold grey62]Task [#afafff]'{t.Name}'[/] added[/]"
            );
        }

        public static void PrintRemoveTask(int id)
        {
            AnsiConsole.Markup(
                $"[bold grey62]task #{id}[/] [bold red]removed[/]"
                );
        }

        public static void PrintCompleteTask(int id)
        {
            AnsiConsole.Markup(
                $"[bold grey62]task #{id} marked as [/][bold green]completed[/]"
                );
        }

        public static void PrintUpdateTask(int id, string name)
        {
            AnsiConsole.Markup(
                $"[bold grey62]Task [#afafff]#{id}[/] updated to [#afafff]'{name}'[/][/]"
                );
        }

        public static void printTaskNotFound(int id)
        {
            AnsiConsole.MarkupLine(
                $"[white on red3_1]Task not found[/]"
            );
        }


        public static void PrintTaskTable(IEnumerable<TaskModel> tasks)
        {
            var markups = new List<IEnumerable<Markup>>();

            // Preenche a lista dinamicamente
            foreach (var task in tasks)
            {
                var markup = new List<Markup>
                {
                    new Markup($"[bold #8787ff]{task.Id}[/]"), // ID da task
                    new Markup($"[bold #afafff]{task.Name}[/]"), // Descrição da task
                    new Markup($"[italic #d7d7ff]({task.CreationDate:yyyy-MM-dd})[/]") // Data formatada
                };

                markups.Add(markup); // Adiciona à lista principal
            }

            var table = new Table();
            table.AddColumn(new TableColumn(new Markup("[#8787ff]Id[/]")));
            table.AddColumn(new TableColumn("[#afafff]Name[/]"));
            table.AddColumn(new TableColumn("[italic #d7d7ff]CreationDate[/]"));

            foreach (var row in markups)
            {
                table.AddRow(new TableRow(row));
            }

            table.Border = TableBorder.MinimalHeavyHead;

            AnsiConsole.Write(table);

        }

        internal static void PrintDropDatabase()
        {
            AnsiConsole.MarkupLine(
                "[white on red3_1]Database dropped[/]"
            );
        }

        internal static void PrintDropDatabaseCanceled()
        {
            AnsiConsole.MarkupLine(
                "[bold grey62]Drop database canceled[/]"
            );
        }

        public static void PrintDecision()
        {
            AnsiConsole.MarkupLine(
                "[white on red3_1]WARNING[/] [bold grey62]Are you sure you want to drop the database? (y/n)[/]"
            );
        }
    }
}
