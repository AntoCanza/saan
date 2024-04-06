using Bogus;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SaAn.Application.Interfaces;
using SaAn.Domain.Entities;
using SaAn.Domain.Enums;

namespace SaAn.Infrastructure.Tests;

[TestFixture]
[Category("Unit")]
internal class Test002Database
{
    private readonly ServiceProvider _serviceProvider;
    private List<Category> _categories;
    private List<Manufacturer> _manufacturers;
    private List<Vehicle> _vehicles;
    private List<SparePart> _spareParts;
    private List<VehicleSparePart> _vehicleSpareParts;

    public Test002Database()
    {
        var services = new ServiceCollection();

        services.AddInfrastructure(
            //"Server=195.201.125.202;Port=54322;Database=saan;Username=saan_user;Password='saan';IncludeErrorDetail=true");
            "Server=localhost;Port=54322;Database=saan;Username=saan_user;Password='saan';IncludeErrorDetail=true");

        _serviceProvider = services.BuildServiceProvider(true);

        GenerateData();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _serviceProvider.Dispose();
    }

    [Test]
    public void Test001_ApplyMigrations()
    {
        using IServiceScope serviceScope = _serviceProvider.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetRequiredService<IDbContext>();

        dbContext.Database.Migrate();

        dbContext.Database.CanConnect().Should().BeTrue();
    }

    [Test]
    public void Test001_PlayWithMockedValues()
    {
        using IServiceScope serviceScope = _serviceProvider.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetRequiredService<IDbContext>();

        dbContext.Set<Category>().AddRange(_categories);
        dbContext.SaveChanges();

        dbContext.Set<Manufacturer>().AddRange(_manufacturers);
        dbContext.SaveChanges();

        dbContext.Set<Vehicle>().AddRange(_vehicles);
        dbContext.SaveChanges();

        dbContext.Set<SparePart>().AddRange(_spareParts);
        dbContext.SaveChanges();

        dbContext.Set<VehicleSparePart>().AddRange(_vehicleSpareParts);
        dbContext.SaveChanges();

        dbContext.Set<Category>().Count().Should().BeGreaterThan(1);
        dbContext.Set<Manufacturer>().Count().Should().BeGreaterThan(1);
        dbContext.Set<Vehicle>().Count().Should().BeGreaterThan(1);
        dbContext.Set<SparePart>().Count().Should().BeGreaterThan(1);
        dbContext.Set<VehicleSparePart>().Count().Should().BeGreaterThan(1);
    }

    private void GenerateData()
    {
        // Generate Categories
        var categoryFaker = new Faker<Category>()
            .RuleFor(c => c.Id, f => Guid.NewGuid())
            .RuleFor(c => c.Name, f => f.Commerce.Categories(1)[0]);

        _categories = categoryFaker.Generate(5);

        // Generate Manufacturers
        var manufacturerFaker = new Faker<Manufacturer>()
            .RuleFor(m => m.Id, f => Guid.NewGuid())
            .RuleFor(m => m.Name, f => f.Company.CompanyName())
            .RuleFor(m => m.ContactInfo, f => f.Internet.Email());

        _manufacturers = manufacturerFaker.Generate(5);

        // Generate Vehicles
        var vehicleFaker = new Faker<Vehicle>()
            .RuleFor(v => v.Id, f => Guid.NewGuid())
            .RuleFor(v => v.Model, f => f.Vehicle.Model())
            .RuleFor(v => v.Brand, f => f.Vehicle.Manufacturer())
            .RuleFor(v => v.VehicleType, f => f.PickRandom<VehicleType>());

        _vehicles = vehicleFaker.Generate(25);

        // Generate Spare Parts
        var sparePartFaker = new Faker<SparePart>()
            .RuleFor(sp => sp.Id, f => Guid.NewGuid())
            .RuleFor(sp => sp.Name, f => f.Commerce.ProductName())
            .RuleFor(sp => sp.PartNumber, f => f.Commerce.Ean13())
            .RuleFor(sp => sp.Description, f => f.Lorem.Sentence())
            .RuleFor(sp => sp.CategoryId, f => f.PickRandom(_categories).Id)
            .RuleFor(sp => sp.ManufacturerId, f => f.PickRandom(_manufacturers).Id);

        _spareParts = sparePartFaker.Generate(200);

        // Randomize VehicleSpareParts
        var rnd = new Random();
        _vehicleSpareParts = new List<VehicleSparePart>();
        foreach (var vehicle in _vehicles)
        {
            // Shuffle spare parts and take a random number of them, between 1 and a maximum of the spare parts list size.
            var shuffledSpareParts = _spareParts.OrderBy(x => rnd.Next()).ToList();
            var takeCount = rnd.Next(1, shuffledSpareParts.Count);
            foreach (var sparePart in shuffledSpareParts.Take(takeCount))
            {
                _vehicleSpareParts.Add(new VehicleSparePart
                {
                    VehicleId = vehicle.Id,
                    SparePartId = sparePart.Id
                });
            }
        }
    }
}