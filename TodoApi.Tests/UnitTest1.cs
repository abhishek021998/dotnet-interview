using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using TodoApi.Controllers;
using TodoApi.Models;
using TodoApi.Services;
using Xunit;

namespace TodoApi.Tests;

public class UnitTest1
{
    private readonly TodoService service;
    private readonly string _connectionString = "Data Source=todo.db";

    public UnitTest1()
    {
 
        var settings = new Dictionary<string, string> {
            {"ConnectionStrings:DefaultConnection", _connectionString}
        };

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(settings)
            .Build();

        InitializeDatabase(_connectionString);

        service = new TodoService(configuration);
    }

    private void InitializeDatabase(string connectionString)
    {
        using var connection = new SqliteConnection(connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Todos (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Title TEXT NOT NULL,
                Description TEXT,
                IsCompleted INTEGER,
                CreatedAt TEXT
            );
        ";
        command.ExecuteNonQuery();
    }
    [Fact]
    public void Test1()
    {
        var settings = new Dictionary<string, string> {
        {"ConnectionStrings:DefaultConnection", "Data Source=todo.db"}
    };

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(settings)
            .Build();

        var service = new TodoService(configuration);

    }

    [Fact]
    public void TestCreateTodo()
    {
        var todo = new Todo
        {
            Title = "Test",
            Description = "Test Description",
            IsCompleted = false
        };

        var result = service.CreateTodo(todo);

        Assert.NotNull(result);
        Assert.True(result.Id > 0);
    }

    [Fact]
    public void TestGetTodo()
    {
        service.CreateTodo(new Todo { Title = "Test" });

        var todos = service.GetAllTodos();

        Assert.True(todos.Count > 0);
    }

    [Fact]
    public void UpdateTest()
    {
        var created = service.CreateTodo(new Todo { Title = "Old" });

        var updated = service.UpdateTodo(created.Id, new Todo
        {
            Title = "Updated",
            Description = "Updated Description",
            IsCompleted = true
        });

        Assert.NotNull(updated);
        Assert.Equal("Updated", updated.Title);
    }

    [Fact]
    public void DeleteWorks()
    {
        var created = service.CreateTodo(new Todo { Title = "ToDelete" });

        var result = service.DeleteTodo(created.Id);

        Assert.True(result);
    }

    [Fact]
    public void ControllerTest()
    {
        var controller = new TodoController(service);

        var todo = new Todo { Title = "Test", Description = "Desc" };

        var result = controller.CreateTodo(todo);

        Assert.NotNull(result);
    }

    [Fact]
    public void TestEverything()
    {
        var todo1 = service.CreateTodo(new Todo { Title = "1", Description = "D1" });
        var todo2 = service.CreateTodo(new Todo { Title = "2", Description = "D2" });

        var all = service.GetAllTodos();

        service.UpdateTodo(todo1.Id, new Todo { Title = "Updated", Description = "D1" });

        service.DeleteTodo(todo2.Id);

        Assert.True(all.Count >= 2);
    }
}