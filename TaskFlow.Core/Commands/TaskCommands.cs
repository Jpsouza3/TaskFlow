using CliFx.Attributes;
using CliFx.Infrastructure;
using System;
using System.Threading.Tasks;
using TaskFlow.Business.Interface;
using TaskFlow.Business.Model;
using TaskFlow.Core.SpectreOutputs;
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

        [CommandOption("add", Description = "Add task command")]
        public string? AddTask { get; init; }

        [CommandOption("list", Description = "List task command")]
        public bool ListTasks { get; init; }

        [CommandOption("remove", Description = "Remove task command")]
        public int? DeleteTask { get; init; }

        [CommandOption("update", Description = "Update task command. Usage: -u <id> <newName>")]
        public string[]? UpdateTask { get; init; }

        [CommandOption("complete", Description = "Complete task command")]
        public int? CompleteTask { get; init; }

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
                DefaultOutputs.PrintTaskAdded(newTask);
            }
            else if (ListTasks)
            {
                IEnumerable<TaskModel> tasks = await _taskRepository.GetAll();
                DefaultOutputs.PrintTaskTable(tasks);
            }
            else if (DeleteTask.HasValue)
            {
                if (await _taskRepository.GetById(DeleteTask.Value) == null)
                {
                    DefaultOutputs.printTaskNotFound(DeleteTask.Value);
                }
                else
                {
                    await _taskRepository.Remove(DeleteTask.Value);
                    DefaultOutputs.PrintRemoveTask(DeleteTask.Value);
                }
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
                        DefaultOutputs.PrintUpdateTask(taskId, UpdateTask[1]);
                    }
                    else
                    {
                        DefaultOutputs.printTaskNotFound(taskId);
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
                    await _taskRepository.Remove(CompleteTask.Value);
                    DefaultOutputs.PrintCompleteTask(CompleteTask.Value);
                }
                else
                {
                    DefaultOutputs.printTaskNotFound(CompleteTask.Value);
                }
            }
            else
            {
                await console.Output.WriteLineAsync("No valid command provided.");
            }
        }
    }
}