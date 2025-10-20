#nullable enable
using System;
using Xunit;

namespace Logger.Tests;

public class StorageTests
{

    [Fact]
    public void Add_Entities_ShouldBeStored()
    {
        
        // Arrange
        var storage = new Storage();
        var entities = new IEntity[]
        {
            new Student { FullName = new ("Inigo", "Montoya") },
            new Employee { FullName = new ("Fezzik", "The Great"), Department = "Security" },
            new Book { Title = "The Princess Bride" }
        };

        // Act
        foreach (var e in entities)
        {
            storage.Add(e);
        }

        // Assert
        Assert.All(entities, e => Assert.True(storage.Contains(e)));
    }

    [Fact]
    public void Add_SameId_NoDuplicates()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        var storage = new Storage();
        var student1 = new Student { Id = id, FullName = new("Bob", "Smith") };
        var student2 = new Student { Id = id, FullName = new("Robert", "Garcia") };

        // Act
        storage.Add(student1);
        storage.Add(student2);
        var retrieved = storage.Get(id);

        // Assert
        Assert.Same(student1, retrieved);
        Assert.NotSame(student2, retrieved);
    }

    [Fact]
    public void Remove_Entity_RemovesFromStorage()
    {
        // Arrange
        var storage = new Storage();
        var book = new Book { Title = "1984" };

        // Act
        storage.Add(book);
        storage.Remove(book);

        // Assert
        Assert.False(storage.Contains(book));
    }

    [Fact]
    public void Contains_Entity_EntityExists()
    {
        // Arrange
        var storage = new Storage();
        var book = new Book { Title = "Fahrenheit 451" };

        // Act
        storage.Add(book);

        // Assert
        Assert.True(storage.Contains(book));
    }

    [Fact]
    public void Get_ById_ReturnsEntity()
    {
        // Arrange
        var storage = new Storage();
        var employee = new Employee { FullName = new("John", "Dober"), Department = "CS" };

        // Act
        storage.Add(employee);
        var retrieved = storage.Get(employee.Id);

        // Assert
        Assert.Same(employee, retrieved);
    }

    [Fact]
    public void Get_ById_ReturnsNullIfMissing()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        var storage = new Storage();

        // Act
        var retrieved = storage.Get(id);
        
        // Assert
        Assert.Null(retrieved);
    }
}