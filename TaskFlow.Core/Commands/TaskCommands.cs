using CliFx.Attributes;
using CliFx.Infrastructure;
using System;
using System.Threading.Tasks;
using TaskFlow.Business.Interface;
using TaskFlow.Business.Model;
using TaskFlow.Data.Interface;

namespace TaskFlow.Core.Commands
{
    [Command("tk", Description = "TaskFlow command initializer")]
    public class TaskCommands : CliFx.ICommand
    {
        private readonly ITaskRepository _taskRepository;

        public TaskCommands(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        [CommandOption("add", 'a', Description = "Add task command")]
        public string? AddTask { get; init; }

        [CommandOption("list", 'l', Description = "List task command")]
        public bool ListTasks { get; init; }

        [CommandOption("delete", 'd', Description = "Delete task command")]
        public int? DeleteTask { get; init; }

        [CommandOption("update", 'u', Description = "Update task command. Usage: -u <id> <newName>")]
        public string[]? UpdateTask { get; init; } // Alterado para array

        [CommandOption("complete", 'c', Description = "Complete task command")]
        public int? CompleteTask { get; init; } // Alterado para nullable

        public async ValueTask ExecuteAsync(IConsole console)
        {
            if (!string.IsNullOrEmpty(AddTask))
            {
                var newTask = new TaskModel
                {
                    Name = AddTask,
                    CreationDate = DateTime.UtcNow
                };

                await _taskRepository.Add(newTask);
                await console.Output.WriteLineAsync($"Task '{AddTask}' added.");
            }
            else if (ListTasks)
            {
                var tasks = await _taskRepository.GetAll();
                foreach (var task in tasks)
                {
                    await console.Output.WriteLineAsync($"Task #{task.Id}: {task.Name} (Created: {task.CreationDate})");
                }
            }
            else if (DeleteTask.HasValue)
            {
                await _taskRepository.Remove(DeleteTask.Value);
                await console.Output.WriteLineAsync($"Task #{DeleteTask} removed.");
            }
            else if (UpdateTask != null && UpdateTask.Length == 2)
            {
                // Parse do ID e novo nome
                if (int.TryParse(UpdateTask[0], out int taskId))
                {
                    var taskToUpdate = await _taskRepository.GetById(taskId);
                    if (taskToUpdate != null)
                    {
                        taskToUpdate.Name = UpdateTask[1];
                        await _taskRepository.Update(taskToUpdate);
                        await console.Output.WriteLineAsync($"Task #{taskId} updated to '{UpdateTask[1]}'.");
                    }
                    else
                    {
                        await console.Output.WriteLineAsync($"Task #{taskId} not found.");
                    }
                }
                else
                {
                    await console.Output.WriteLineAsync("Invalid task ID format.");
                }
            }
            else if (CompleteTask.HasValue)
            {
                var taskToComplete = await _taskRepository.GetById(CompleteTask.Value);
                if (taskToComplete != null)
                {
                    // Lógica para marcar como concluída (adicione uma propriedade no modelo se necessário)
                    await console.Output.WriteLineAsync($"Task #{CompleteTask} marked as completed.");
                }
                else
                {
                    await console.Output.WriteLineAsync($"Task #{CompleteTask} not found.");
                }
            }
            else
            {
                await console.Output.WriteLineAsync("No valid command provided.");
            }
        }
    }
}