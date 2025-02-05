using Microsoft.EntityFrameworkCore;
using MotorcycleWorkshop.Infrastructure;

namespace MotorcycleWorkshop;

public class WorkshopDBContextTests : IDisposable
{
    private readonly Microsoft.Data.Sqlite.SqliteConnection _connection;
    private readonly WorkshopDBContext _context;

    public WorkshopDBContextTests()
    {
        _connection = new Microsoft.Data.Sqlite.SqliteConnection("Filename=:memory:");
        _connection.Open();

        var options = new DbContextOptionsBuilder<WorkshopDBContext>()
            .UseSqlite(_connection)
            .Options;

        _context = new WorkshopDBContext(options);
        _context.Database.Migrate(); // Datenbank migrate
    }

    [Fact]
    public void Database_ShouldSeedDataCorrectly()
    {
        var customers = _context.Customers.ToList();
        var mechanics = _context.Mechanics.ToList();
        var repairs = _context.Repairs.ToList();

        Assert.NotEmpty(customers);
        Assert.NotEmpty(mechanics);
        Assert.NotEmpty(repairs);
    }

    public void Dispose()
    {
        _context.Dispose();
        _connection.Close();
        _connection.Dispose();
    }
}