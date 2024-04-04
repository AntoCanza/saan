using System.Reflection;
using AutoMapper;

namespace SaAn.Infrastructure.Tests;

[TestFixture]
[Category("Unit")]
internal class Test001MappingProfiles
{
    [Test]
    public void AssertConfigurationIsValid()
    {
        var configuration = new MapperConfiguration(opt => { opt.AddProfiles(MappingProfilesFromAssembly()); });

        configuration.AssertConfigurationIsValid();
    }

    private static IEnumerable<Profile> MappingProfilesFromAssembly()
    {
        Assembly assembly = typeof(ApplicationDbContext).Assembly;
        IEnumerable<TypeInfo> mappingTypes = assembly.DefinedTypes
            .Where(x => typeof(Profile).IsAssignableFrom(x) && x is { IsInterface: false, IsAbstract: false });
        return mappingTypes.Select(Activator.CreateInstance).Cast<Profile>();
    }
}