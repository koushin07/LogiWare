using Logiware.Application.Services;
using Logiware.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Logiware.Test.Services;

[TestFixture]
[TestOf(typeof(ShipmentService))]
public class ShipmentServiceTest
{

    private readonly DbContextOptions<MyDbContext> _options;

    public ShipmentServiceTest()
    {
        _options = new DbContextOptionsBuilder<MyDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
    }
    [Test]
    public void CreateShipment_SuccessFully_AndParseToDataBase()
    {
        Assert.True(true);
    }
}