using Microsoft.Extensions.DependencyInjection;
using SalesmenSimulator.Services;

public static class ServiceRegistration
{
  public static IServiceCollection AddGameServices(this IServiceCollection services)
  {
    services.AddSingleton<IGameService, GameService>();
    services.AddSingleton<ISessionFactory, GameStateFactory>();
    services.AddSingleton<ICarCatalog, CarCatalog>();
    services.AddScoped<IRandomProvider, SystemRandomProvider>();
    services.AddScoped<ICarGeneratorService, CarGeneratorService>();
    services.AddScoped<IRestockService, RestockService>();
    services.AddScoped<ICustomerGeneratorService, CustomerGeneratorService>();
    return services;
  }
}