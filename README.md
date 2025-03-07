# TaskFlow CLI

[![.NET](https://img.shields.io/badge/.NET-8-blue)](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

TaskFlow CLI is a lightweight task manager built using **.NET 8**, [CliFx](https://github.com/Tyrrrz/CliFx) for command-line parsing, [Spectre.Console](https://spectreconsole.net/) for stunning console outputs, and [Dapper](https://github.com/DapperLib/Dapper) with SQLite for data access.

<div align="center">
  <img src="https://github.com/Jpsouza3/TaskFlow/blob/adding-gif-to-readme/readme.gif?raw=true" width="500" />
</div>

## Features

- **Add a task:** Quickly add new tasks.
- **List tasks:** View all your tasks in a neatly formatted table.
- **Remove a task:** Delete a task by its ID.
- **Update a task:** Update the name of an existing task.
- **Complete a task:** Mark a task as complete (removes it from the list).
- **Drop database:** Remove the entire tasks database after confirmation.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)  
- Ensure your system's PATH is correctly set for global tools (the installation process takes care of that).

## Installation

TaskFlow CLI is distributed as a .NET global tool. Once published, you can install it globally with:
https://www.nuget.org/packages/TaskFlowCli
```bash
dotnet tool install --global TaskFlowCli --version 1.0.0
````


# Add a new task

```bash
tk --add "Finish writing README"
````


# List all tasks
```bash
tk --list
````

# Update an existing task (e.g., change task with ID 1)
```bash
tk --update 1 "Finish writing detailed README"
````

# Complete a task (e.g., task with ID 1)
```bash
tk --complete 1
````

# Remove a task (e.g., task with ID 2)
```bash
tk --remove 2
````

# Drop the entire database (with confirmation)
```bash
tk --dropdb
````
